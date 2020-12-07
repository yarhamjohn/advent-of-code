using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day6
    {
        public static int CalculateQuestionSumAllQuestions(List<string> input)
        {
            var total = 0;
            var runningTotal = new HashSet<char>(26);
            foreach (var line in input)
            {
                if (line.Trim() == "")
                {
                    total += runningTotal.Count;
                    runningTotal.Clear();
                    continue;
                }

                foreach (var letter in line)
                {
                    runningTotal.Add(letter);
                }
            }

            total += runningTotal.Count;
            return total;
        }
        
        public static int CalculateQuestionSumSameQuestions(List<string> input)
        {
            var total = 0;
            var runningTotal = new List<HashSet<char>>();
            foreach (var line in input)
            {
                if (line.Trim() == "")
                {
                    runningTotal = TopUpRunningTotal(runningTotal);

                    total += runningTotal[0].Count;
                    runningTotal.Clear();
                    continue;
                }

                var hashSet = new HashSet<char>(26);
                foreach (var letter in line)
                {
                    hashSet.Add(letter);
                }
                runningTotal.Add(hashSet);
            }

            runningTotal = TopUpRunningTotal(runningTotal);
            total += runningTotal[0].Count;
            return total;
        }

        private static List<HashSet<char>> TopUpRunningTotal(List<HashSet<char>> runningTotal)
        {
            while (runningTotal.Count > 1)
            {
                var remainder = runningTotal.Count > 2
                    ? runningTotal.GetRange(2, runningTotal.Count - 2)
                    : new List<HashSet<char>>();
                runningTotal = new List<HashSet<char>> {runningTotal[0].Intersect(runningTotal[1]).ToHashSet()};
                runningTotal.AddRange(remainder);
            }

            return runningTotal;
        }
    }
}