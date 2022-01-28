using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day3
    {
        public static int CountTreesHit(List<string> input, int right, int down)
        {
            var width = input[0].Length;
            var height = input.Count;

            var requiredWidth = right * height;
            var repetitions = Convert.ToInt32(Math.Ceiling(requiredWidth / (decimal) width));

            var extendedInput = GetExtendedInput(input, repetitions);
            var currentColumn = 0;
            var treeCount = 0;
            for (var i = 0; i < height; i += down)
            {
                if (extendedInput[i].Substring(currentColumn, 1) == "#")
                {
                    treeCount++;
                }
                currentColumn += right;
            }
            
            return treeCount;
        }

        private static List<string> GetExtendedInput(List<string> input, int repetitions)
        {
            var extendedInput = input.Select(x =>
            {
                var stringBuilder = new StringBuilder();
                for (var i = 0; i < repetitions; i++)
                {
                    stringBuilder.Append(x);
                }

                return stringBuilder.ToString();
            });

            return extendedInput.ToList();
        }
    }
}