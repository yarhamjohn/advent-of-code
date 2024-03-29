﻿using System.Security.Cryptography;

namespace AdventOfCode2016.Day14;

public static class Day14
{
    public static long Get64thKeyIndexStretched(string salt)
    {
        var index = 0;
        var matchingIndexes = new List<int>();

        var next1000 = new Dictionary<int, string>();
        
        while (matchingIndexes.Count < 64)
        {
            var encodedString = GetCandidateStretchedKey(salt, index);

            var firstTrebleChar = GetFirstTrebleChar(encodedString);

            if (firstTrebleChar is not null)
            {
                // Remove from dictionary anything not in required range
                foreach (var x in next1000.Keys.Where(x => x <= index || x > index + 1000))
                {
                    next1000.Remove(x);
                }
                
                // add required elements to dictionary
                for (var i = 0; i < 1000; i++)
                {
                    var targetIndex = index + 1 + i;
                    if (!next1000.ContainsKey(targetIndex))
                    {
                        next1000[targetIndex] = GetCandidateStretchedKey(salt, targetIndex);
                    }
                }

                if (next1000.Any(x =>
                        x.Value.Contains(
                            $"{firstTrebleChar}{firstTrebleChar}{firstTrebleChar}{firstTrebleChar}{firstTrebleChar}")))
                {
                    matchingIndexes.Add(index);
                }
            }
            
            index++;
        }
        
        return matchingIndexes.Last();
    }

    public static long Get64thKeyIndex(string salt)
    {
        var index = 0;
        var matchingIndexes = new List<int>();

        var next1000 = new Dictionary<int, string>();
        
        while (matchingIndexes.Count < 64)
        {
            var encodedString = GetCandidateKey(salt, index);

            var firstTrebleChar = GetFirstTrebleChar(encodedString);

            if (firstTrebleChar is not null)
            {
                // Remove from dictionary anything not in required range
                foreach (var x in next1000.Keys.Where(x => x <= index || x > index + 1000))
                {
                    next1000.Remove(x);
                }
                
                // add required elements to dictionary
                for (var i = 0; i < 1000; i++)
                {
                    var targetIndex = index + 1 + i;
                    if (!next1000.ContainsKey(targetIndex))
                    {
                        next1000[targetIndex] = GetCandidateKey(salt, targetIndex);
                    }
                }

                if (next1000.Any(x =>
                        x.Value.Contains(
                            $"{firstTrebleChar}{firstTrebleChar}{firstTrebleChar}{firstTrebleChar}{firstTrebleChar}")))
                {
                    matchingIndexes.Add(index);
                }
            }
            
            index++;
        }
        
        return matchingIndexes.Last();
    }

    private static string GetCandidateKey(string salt, int index)
    {
        var md5 = MD5.HashData(System.Text.Encoding.ASCII.GetBytes($"{salt}{index}"));

        return BitConverter.ToString(md5).Replace("-", "");
    }

    private static string GetCandidateStretchedKey(string salt, int index)
    {
        var hash = GetCandidateKey(salt, index);
        for (var i = 0; i < 2016; i++)
        {
            var md5 = MD5.HashData(System.Text.Encoding.ASCII.GetBytes($"{hash.ToLower()}"));
            hash = BitConverter.ToString(md5).Replace("-", "");
        }

        return hash;
    }

    private static char? GetFirstTrebleChar(string encodedString)
    {
        var count = 0;
        char? currentChar = null;
        
        foreach (var ch in encodedString)
        {
            if (ch == currentChar)
            {
                count++;
            }
            else
            {
                currentChar = ch;
                count = 1;
            }

            if (count == 3)
            {
                break;
            }
        }

        return count != 3 ? null : currentChar;
    }
}