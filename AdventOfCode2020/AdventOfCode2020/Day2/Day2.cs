using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day2
{
    public static class Day2
    {
        public static int CountValidPasswordsWrongPolicy(IEnumerable<string> input)
        {
            var validPasswords =  0;
            foreach (var row in input)
            {
                var (min, max, target, letters) = GetInputSegments(row);
                var count = letters.Count(x => x == target);
                if (count >= min && count <= max)
                {
                    validPasswords++;
                }
            }

            return validPasswords;
        }

        public static int CountValidPasswordsCorrectPolicy(IEnumerable<string> input)
        {
            var validPasswords =  0;
            foreach (var row in input)
            {
                var (pos1, pos2, target, letters) = GetInputSegments(row);
                
                // 1 index-based not 0 index-based
                var pos1Matches = letters[pos1 - 1] == target;
                var pos2Matches = letters[pos2 - 1] == target;
                if (pos1Matches != pos2Matches)
                {
                    validPasswords++;
                }
            }

            return validPasswords;
        }

        private static (int, int, char, string) GetInputSegments(string row)
        {
            var segments = row.Split(" ");
            var range = segments[0].Split("-").Select(x => Convert.ToInt32(x)).ToList();
            var target = segments[1].Split(":")[0].ToCharArray()[0];
            return (range[0], range[1], target, segments[2]);
        }
    }
}