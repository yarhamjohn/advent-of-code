using System.ComponentModel.DataAnnotations;

namespace AdventOfCode2023.Day14;

public static class Day14
{
    public static long SumRockLoad(string[] input)
    {
        var platform = ParseInput(input);
        TiltNorth(platform);
        return CalculateLoad(platform);
    }

    public static long SumRockLoadCycles(string[] input)
    {
        var platform = ParseInput(input);
        var platform2 = ParseInput(input);
        // PrintPlatform(platform);

        for (var i = 0; i < 1000000000; i++)
        {
            if (i % 100000 == 0)
            {
                Console.WriteLine("Reached " + i + " cycles");
            }
            
            TiltNorth(platform);
            TiltWest(platform);
            TiltSouth(platform);
            TiltEast(platform);

            if (MatchesStart(platform, platform2))
            {
                Console.WriteLine("It matched after " + i + 1 + " cycles");
                break;
            }

            // PrintPlatform(platform);
        }
        
        return CalculateLoad(platform);
    }

    private static bool MatchesStart(List<string[]> platform, List<string[]> platform2)
    {
        for (var row = 0; row < platform.Count; row++)
        {
            for (var col = 0; col < platform[0].Length; col++)
            {
                if (platform[row][col] != platform2[row][col])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static void PrintPlatform(List<string[]> platform)
    {
        foreach (var row in platform)
        {
            for (var col = 0; col < platform[0].Length; col++)
            {
                Console.Write(row[col]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private static long CalculateLoad(IReadOnlyList<string[]> newPlatform)
    {
        var result = 0L;
        for (var row = 0; row < newPlatform.Count; row++)
        {
            result += (newPlatform.Count - row) * newPlatform[row].Count(x => x == "O");
        }

        return result;
    }

    private static void TiltNorth(IReadOnlyList<string[]> platform)
    {
        for (var row = 0; row < platform.Count; row++)
        {
            for (var col = 0; col < platform[0].Length; col++)
            {
                if (platform[row][col] != "O")
                {
                    continue;
                }

                var currentRow = row;
                while (currentRow > 0)
                {
                    var nextLocation = platform[currentRow - 1][col];
                    if (nextLocation == ".")
                    {
                        currentRow--;
                    }
                    else
                    {
                        break;
                    }
                }

                platform[row][col] = ".";
                platform[currentRow][col] = "O";
            }
        }
    }
    
    private static void TiltWest(IReadOnlyList<string[]> platform)
    {
        for (var row = 0; row < platform.Count; row++)
        {
            for (var col = 0; col < platform[0].Length; col++)
            {
                if (platform[row][col] != "O")
                {
                    continue;
                }

                var currentCol = col;
                while (currentCol > 0)
                {
                    var nextLocation = platform[row][currentCol - 1];
                    if (nextLocation == ".")
                    {
                        currentCol--;
                    }
                    else
                    {
                        break;
                    }
                }

                platform[row][col] = ".";
                platform[row][currentCol] = "O";
            }
        }
    }
    
    private static void TiltSouth(IReadOnlyList<string[]> platform)
    {
        for (var row = platform.Count - 1; row >= 0; row--)
        {
            for (var col = 0; col < platform[0].Length; col++)
            {
                if (platform[row][col] != "O")
                {
                    continue;
                }

                var currentRow = row;
                while (currentRow < platform.Count - 1)
                {
                    var nextLocation = platform[currentRow + 1][col];
                    if (nextLocation == ".")
                    {
                        currentRow++;
                    }
                    else
                    {
                        break;
                    }
                }

                platform[row][col] = ".";
                platform[currentRow][col] = "O";
            }
        }
    }

    private static void TiltEast(IReadOnlyList<string[]> platform)
    {
        for (var row = 0; row < platform.Count; row++)
        {
            for (var col = platform[0].Length - 1; col >= 0; col--)
            {
                if (platform[row][col] != "O")
                {
                    continue;
                }

                var currentCol = col;
                while (currentCol < platform[0].Length - 1)
                {
                    var nextLocation = platform[row][currentCol + 1];
                    if (nextLocation == ".")
                    {
                        currentCol++;
                    }
                    else
                    {
                        break;
                    }
                }

                platform[row][col] = ".";
                platform[row][currentCol] = "O";
            }
        }
    }
    
    private static List<string[]> ParseInput(IEnumerable<string> input)
    {
        return input.Select(line => line.ToCharArray().Select(x => x.ToString()).ToArray()).ToList();
    }
}