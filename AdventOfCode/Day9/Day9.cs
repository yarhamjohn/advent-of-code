using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day9
    {
        public static long GetInvalidNum(List<string> input, int preamble)
        {
            for (var i = preamble; i < input.Count; i++)
            {
                var num = Convert.ToInt32(input[i]);
                var previousNums = input.GetRange(i - preamble, preamble).Select(x => Convert.ToInt64(x)).ToList();
                var validPairs = GetAllNumPairs(previousNums).Any(x => IsValidNum(x, num));
                if (!validPairs)
                {
                    return num;
                }
            }

            throw new InvalidOperationException("No invalid number was found.");
        }

        public static long GetWeakness(List<string> input, int preamble)
        {
            var invalidNum = GetInvalidNum(input, preamble);
            var nums = input.Select(x => Convert.ToInt64(x)).ToList();
            var index = nums.IndexOf(invalidNum);
            var previousNums = input.GetRange(0, index).Select(x => Convert.ToInt64(x)).ToList();
            for (var i = 0; i < previousNums.Count; i++)
            {
                var numsIncluded = new List<long>();
                var currentIndex = i;
                while (numsIncluded.Sum() < invalidNum)
                {
                    numsIncluded.Add(previousNums[currentIndex]);
                    currentIndex++;
                }

                if (numsIncluded.Sum() == invalidNum)
                {
                    return numsIncluded.Min() + numsIncluded.Max();
                }
            }

            throw new InvalidOperationException("Something went wrong");
        }

        private static List<(long numOne, long numTwo)> GetAllNumPairs(IReadOnlyCollection<long> nums)
        {
            var pairs = new List<(long, long)>();
            var numsDone = new List<long>();
            foreach (var num in nums)
            {
                pairs.AddRange(nums.Where(x => x != num && !numsDone.Contains(x)).Select(otherNum => (num, otherNum)));
                numsDone.Add(num);
            }

            return pairs;
        }

        private static bool IsValidNum((long numOne, long numTwo) nums, long targetNum) => nums.numOne + nums.numTwo == targetNum;
    }
}