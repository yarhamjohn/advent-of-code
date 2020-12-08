using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day8
    {
        public static int GetNumBeforeInfiniteLoop(List<string> input)
        {
            return GetIndexesVisited(input)
                .Select(x => GetInstruction(input[x]))
                .Where(y => y.Item1 == "acc")
                .Sum(z => z.Item2);
        }

        public static int GetNumAfterBreakingInfiniteLoop(List<string> input)
        {
            var indexesVisited = GetIndexesVisited(input).ToList();
            indexesVisited.Sort();

            for (var i = indexesVisited.Count - 1; i >= 0; i--)
            {
                var (instruction, value) = GetInstruction(input[indexesVisited[i]]);
                if (instruction == "acc")
                {
                    continue;
                }

                var newInput = ModifyInput(input, indexesVisited[i], instruction, value);

                var visited = GetIndexesVisited(newInput);
                if (visited.Max() < input.Count)
                {
                    continue;
                }
                
                visited.Remove(visited.Max());
                return visited.Select(x => GetInstruction(input[x]))
                    .Where(y => y.Item1 == "acc")
                    .Sum(z => z.Item2);
            }

            throw new InvalidOperationException("Something went wrong.");
        }

        private static List<string> ModifyInput(List<string> input, int index, string instruction, int value)
        {
            var newInput = input.ToList();
            newInput[index] = instruction switch
            {
                "jmp" => $"nop {(value < 0 ? value : $"+{value}")}",
                "nop" => $"jmp {(value < 0 ? value : $"+{value}")}",
                _ => newInput[index]
            };
            return newInput;
        }

        private static HashSet<int> GetIndexesVisited(List<string> input)
        {
            var visitedIndexes = new HashSet<int>();
            var currentIndex = 0;
            while (true)
            {
                var added = visitedIndexes.Add(currentIndex);
                if (!added || currentIndex >= input.Count)
                {
                    return visitedIndexes;
                }

                var (instruction, value) = GetInstruction(input[currentIndex]);
                switch (instruction)
                {
                    case "acc":
                        currentIndex++;
                        break;
                    case "jmp":
                        currentIndex += value;
                        break;
                    case "nop":
                        currentIndex++;
                        break;
                    default: throw new InvalidOperationException($"Not a valid instruction: {instruction}");
                }
            }
        }

        private static (string, int) GetInstruction(string line)
        {
            var instruction = line.Split(" ");
            return (instruction[0], Convert.ToInt32(instruction[1]));
        }
    }
}