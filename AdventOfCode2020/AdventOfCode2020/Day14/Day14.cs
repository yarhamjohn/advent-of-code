using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day14
{
    public static class Day14
    {
        public static long SumInMemoryValues(List<string> input)
        {
            var memory = new Dictionary<int, long>();
            var mask = string.Concat(Enumerable.Repeat("0", 36));
            foreach (var row in input)
            {
                if (row.Substring(0, 4) == "mask")
                {
                    mask = row.Substring(7, 36);
                }
                else
                {
                    var (location, value) = ParseMemoryUpdate(row);
                    memory[location] = GetMaskedValue(mask, value);
                }
                
            }
            return memory.Sum(x => x.Value);
        }
        
        public static long SumInMemoryValuesVersion2(List<string> input)
        {
            var memory = new Dictionary<long, long>();
            var mask = string.Concat(Enumerable.Repeat("0", 36));
            foreach (var row in input)
            {
                if (row.Substring(0, 4) == "mask")
                {
                    mask = row.Substring(7, 36);
                }
                else
                {
                    var (location, value) = ParseMemoryUpdate(row);
                    var locations = GetMaskedLocations(mask, location);
                    foreach (var loc in locations)
                    {
                        memory[loc] = value;
                    }
                }
                
            }
            return memory.Sum(x => x.Value);
        }

        private static IEnumerable<long> GetMaskedLocations(string mask, int location)
        {
            var binaryLocation = Convert.ToString(location, 2).PadLeft(36, '0');
            var partialMaskedLocation = mask.Select((x, i) => x == '0' ? binaryLocation[i] : x).ToArray();
            var combinations = GetCombinations(partialMaskedLocation);
            foreach (var combination in combinations)
            {
                var indexesToReplace = partialMaskedLocation.Select((x, i) => (x, i)).Where(x => x.Item1 == 'X')
                    .Select(x => x.Item2).ToList();
                var realLocation = partialMaskedLocation.Select((x, i) => x != 'X' ? x : combination[indexesToReplace.IndexOf(i)]).ToArray();

                yield return Convert.ToInt64(new string(realLocation), 2);
            }
        }

        private static IEnumerable<string> GetCombinations(char[] partialMaskedLocation)
        {
            var numCombinations = Math.Pow(2, partialMaskedLocation.Count(x => x == 'X'));
            var length = Convert.ToString(Convert.ToInt64(numCombinations) - 1, 2).Length;
            for (var i = 0; i < numCombinations; i++)
            {
                yield return Convert.ToString(i, 2).PadLeft(length, '0');
            }
        }

        private static (int, long) ParseMemoryUpdate(string row)
        {
            var memRegex = new Regex("^mem\\[(?<location>[0-9]+)\\] = (?<value>[0-9]+)$");
            var match = memRegex.Match(row);
            var location = Convert.ToInt32(match.Groups["location"].Value);
            var value = Convert.ToInt64(match.Groups["value"].Value);
            return (location, value);
        }

        private static long GetMaskedValue(string mask, long value)
        {
            var binaryValue = Convert.ToString(value, 2).PadLeft(36, '0');
            var maskedValue = mask.Select((x, i) => x == 'X' ? binaryValue[i] : x).ToArray();
            return Convert.ToInt64(new string(maskedValue), 2);
        }
    }
}