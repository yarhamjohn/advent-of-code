using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode2023.Day18;

public static class Day18
{
    public static long CalculateLagoonSize(string[] input)
    {
        var parsedInput = ParseInput(input);
        
        // assume it starts at (0, 0)
        var positions = new List<(int row, int col)>();
        var currentPosition = (row: 0, col: 0);
        foreach (var x in parsedInput)
        {
            for (var i = 0; i < x.count; i++)
            {
                switch (x.direction)
                {
                    case "R":
                    {
                        currentPosition = (currentPosition.row, currentPosition.col + 1);
                        positions.Add(currentPosition);
                        break;
                    }
                    case "L":
                    {
                        currentPosition = (currentPosition.row, currentPosition.col - 1);
                        positions.Add(currentPosition);
                        break;
                    }
                    case "U":
                    {
                        currentPosition = (currentPosition.row - 1, currentPosition.col);
                        positions.Add(currentPosition);
                        break;
                    }
                    case "D":
                    {
                        currentPosition = (currentPosition.row + 1, currentPosition.col);
                        positions.Add(currentPosition);
                        break;
                    }
                }
            }
        }

        // Console.WriteLine(string.Join(",", positions));

        var orderedPositions = positions.Order().ToArray();

        Print(orderedPositions);
        PrintFull(orderedPositions);
        
        // Console.WriteLine(string.Join(",", orderedPositions));

        var num = 0;
        var groups = orderedPositions.GroupBy(x => x.row);
        foreach (var group in groups)
        {
            var cols = group.Select(x => x.col);

            var inGrp = false;
            for (var i = cols.Min(); i <= cols.Max(); i++)
            {
                if (cols.Contains(i) && inGrp == false)
                {
                    inGrp = true;
                    num++;
                }
                else if (inGrp)
                {
                    num++;
                }
                else if (!cols.Contains(i + 1) && inGrp == true)
                {
                    inGrp = false;
                }
            }
        }

        return num;
        
        //90719 too high
    }

    private static void Print((int row, int col)[] orderedPositions)
    {
        var min = (orderedPositions.Min(x => x.row), orderedPositions.Min(x => x.col));
        var max = (orderedPositions.Max(x => x.row), orderedPositions.Max(x => x.col));

        var inGrp = false;
        for (var i = min.Item1; i <= max.Item1; i++)
        {
            for (var j = min.Item2; j <= max.Item2; j++)
            {
                if (orderedPositions.Contains((i, j)))
                {
                    Console.Write('#');
                }
                else
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static void PrintFull((int row, int col)[] orderedPositions)
    {
        var min = (orderedPositions.Min(x => x.row), orderedPositions.Min(x => x.col));
        var max = (orderedPositions.Max(x => x.row), orderedPositions.Max(x => x.col));

        var inGrp = false;
        for (var i = min.Item1; i <= max.Item1; i++)
        {
            inGrp = false;
            for (var j = min.Item2; j <= max.Item2; j++)
            {
                if (orderedPositions.Contains((i, j)) || inGrp)
                {
                    Console.Write('#');
                    inGrp = true;
                }
                
                if (!orderedPositions.Contains((i, j)))
                {
                }

                Console.Write('.');
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static IEnumerable<(string direction, int count, string colour)> ParseInput(string[] input)
    {
        return input
            .Select(line => line.Split(" "))
            .Select(splitLine => (
                splitLine[0], 
                int.Parse(splitLine[1]),
                splitLine[2].Replace(")", "").Replace("(", "")));
    }
}