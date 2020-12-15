using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace AdventOfCode
{
    public static class Day15
    {
        public static int FindNumber(string input, int target)
        {
            var startingNumbers = input.Split(",").Select(x => Convert.ToInt32(x)).ToList();

            var history = new Dictionary<int, List<int>>();
            var turn = 1;
            foreach (var num in startingNumbers)
            {
                history[num] = new List<int> {turn};
                turn++;
            }

            var lastNumberSpoken = history.Last().Key;
            while (turn <= target)
            {
                history.TryGetValue(lastNumberSpoken, out var lastNumberTurnsSeen);
                var lastNumberWasNew = lastNumberTurnsSeen!.Count == 1;
                if (lastNumberWasNew)
                {
                    const int nextNumberSpoken = 0;
                    UpdateHistory(history, nextNumberSpoken, turn);
                    lastNumberSpoken = nextNumberSpoken;
                }
                else
                {
                    var nextNumberSpoken = lastNumberTurnsSeen![1] - lastNumberTurnsSeen[0];
                    UpdateHistory(history, nextNumberSpoken, turn);
                    lastNumberSpoken = nextNumberSpoken;
                }

                turn++;
            }

            return lastNumberSpoken;
        }

        private static void UpdateHistory(Dictionary<int, List<int>> history, int nextNumberSpoken, int turn)
        {
            history.TryGetValue(nextNumberSpoken, out var nextNumberTurnsSeen);
            if (nextNumberTurnsSeen == null)
            {
                history[nextNumberSpoken] = new List<int> {turn};
            }
            else if (nextNumberTurnsSeen.Count == 1)
            {
                history[nextNumberSpoken] = nextNumberTurnsSeen.Append(turn).ToList();
            }
            else
            {
                history[nextNumberSpoken] = new List<int> {nextNumberTurnsSeen[1], turn};
            }
        }
    }
}