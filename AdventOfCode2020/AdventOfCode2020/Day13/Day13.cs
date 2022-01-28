using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day13
{
    public static class Day13
    {
        public static int CalculateBusId(List<string> input)
        {
            return input[1]
                .Split(",")
                .Where(id => id != "x")
                .Select(id => Convert.ToInt32(id))
                .Select(id => (id, id - Convert.ToInt32(input[0]) % id))
                .OrderBy(calc => calc.Item2)
                .Select(calc => calc.Item1 * calc.Item2)
                .First();
        }

        public static long CalculateTimestamp(List<string> input)
        {
            var nums = input[1]
                .Split(",")
                .Select(x => x == "x" ? 0 : Convert.ToInt64(x))
                .Select((x, i) => (x - i, x))
                .Where(x => x.Item2 != 0)
                .ToList();
            
            var N = nums.Select(x => x.Item2).Aggregate((a, b) => a * b);
            
            return nums
                .Select(x => (x.Item1, N / x.Item2, CalcX(N / x.Item2, x.Item2)))
                .Select(x => x.Item1 * x.Item2 * x.Item3)
                .Sum() % N;
        }

        private static long CalcX(long n, long mod)
        {
            var incrementor = 1;
            while (true)
            {
                var currentN = n % mod * incrementor;
                if (currentN % mod == 1)
                {
                    return incrementor;
                }

                incrementor++;
            }
        }
    }
}