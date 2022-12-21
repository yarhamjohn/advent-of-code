namespace AdventOfCode2022.Day17;

public static class Day17
{
    public static long CalculateHeight(string input)
    {
        return Calculate(input, 2022);
    }

    public static long CalculateHeightHuge(string input)
    {
        // Find repeating pattern or rocks and jets
        // Calculate height of repeating pattern
        // Calculate height of starting section (before repeating pattern)
        // Calculate height of remaining section (after last complete pattern)
        
        /*
         * A total of 277 rocks were placed before the first repeating section with a height of 417
         * Each repeating section is 1755 rocks adding a height of 2768
         *
         * The first repeat adds 2767 because the first row overlaps with the last of the non-repeating section
         * The non-repeating section height can therefore be adjusted to 416 to keep all repeating sections constant
         *
         * Total rocks in complete repeating sections: (1000000000000 - 277) / 1755 = 569800569
         * Total height of complete repeating sections: 569800569 * 2768 = 1577207974992
         *
         * Total rocks in remaining incomplete section: (1000000000000 - 277) % 1755 = 1128
         * Total height of rocks in remaining incomplete section: 1778
         *
         * Total height: 416 + 1577207974992 + 1778 = 1577207977186
         */

        return Calculate(input, 1000000000000);
    }

    private static long Calculate(string input, long size)
    {
        var grid = Enumerable.Range(0, 10).Select(_ => Enumerable.Range(0, 7).Select(_ => ".").ToList()).ToList();

        var jetIndex = 0;

        for (var i = 0L; i < size; i++)
        {
            grid = ExtendGrid(grid);

            var rockPositions = AddNewRockToGrid(grid, i);

            if (CanMove(input[jetIndex], grid, rockPositions))
            {
                rockPositions = Move(input[jetIndex], grid, rockPositions);
            }

            jetIndex = ++jetIndex >= input.Length ? 0 : jetIndex;

            while (true)
            {
                if (CanMove('v', grid, rockPositions))
                {
                    rockPositions = Move('v', grid, rockPositions);
                }
                else
                {
                    foreach (var rock in rockPositions)
                    {
                        FixRockPosition(grid, rock);
                    }

                    break;
                }

                if (CanMove(input[jetIndex], grid, rockPositions))
                {
                    rockPositions = Move(input[jetIndex], grid, rockPositions);
                }

                jetIndex = ++jetIndex >= input.Length ? 0 : jetIndex;
            }
        }

        return GetHighestRow(grid) + 1; // 0-based index
    }

    private static List<List<string>> ExtendGrid(List<List<string>> grid)
    {
        var spaceNeeded = GetNewRockEntryRow(grid) + 4; // Include space for the new rock
        if (spaceNeeded >= grid.Count)
        {
            grid = grid.Concat(Enumerable.Range(0, spaceNeeded - grid.Count)
                    .Select(_ => Enumerable.Range(0, 7).Select(_ => ".").ToList()))
                .ToList();
        }

        return grid;
    }

    private static List<(int row, int col)> AddNewRockToGrid(List<List<string>> grid, long iteration)
    {
        var rockPositions = GetRockStartingPositions(grid, iteration);
        
        foreach (var rock in rockPositions)
        {
            SetPositionToRock(grid, rock);
        }

        return rockPositions;
    }

    private static void SetPositionToRock(List<List<string>> grid, (int row, int col) rock)
    {
        grid[rock.row][rock.col] = "@";
    }
    
    private static void FixRockPosition(List<List<string>> grid, (int row, int col) rock)
    {
        grid[rock.row][rock.col] = "#";
    }
    
    private static void SetPositionToEmpty(List<List<string>> grid, (int row, int col) rock)
    {
        grid[rock.row][rock.col] = ".";
    }

    private static bool CanMove(char direction, List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        return direction switch
        {
            '>' => rockPositions.All(x => x.col < 6 && grid[x.row][x.col + 1] != "#"),
            '<' => rockPositions.All(x => x.col > 0 && grid[x.row][x.col - 1] != "#"),
            _ => rockPositions.All(x => x.row != 0 && grid[x.row - 1][x.col] != "#")
        };
    }

    private static List<(int row, int col)> Move(char direction, List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        foreach (var position in rockPositions)
        {
            SetPositionToEmpty(grid, position);
        }

        var newPositions = GetNewPositions(direction, rockPositions);
        foreach (var position in newPositions)
        {
            SetPositionToRock(grid, position);
        }

        return newPositions;
    }
    
    private static List<(int row, int col)> GetNewPositions(char direction, List<(int row, int col)> rockPositions)
    {
        return direction switch
        {
            '>' => rockPositions.Select(x => (x.row, x.col + 1)).ToList(),
            '<' => rockPositions.Select(x => (x.row, x.col - 1)).ToList(),
            _ => rockPositions.Select(x => (x.row - 1, x.col)).ToList()
        };
    }

    private static List<(int row, int col)> GetRockStartingPositions(List<List<string>> grid, long iteration)
    {
        const int startCol = 2;
        var row = GetNewRockEntryRow(grid);

        return (iteration % 5) switch
        {
            0 =>
                /*
                 * ####
                 */
                new List<(int row, int col)>
                {
                    (row, startCol), (row, startCol + 1), (row, startCol + 2), (row, startCol + 3)
                },
            1 =>
                /*
                 * .#.
                 * ###
                 * .#.
                 */
                new List<(int row, int col)>
                {
                    (row + 1, startCol),
                    (row, startCol + 1),
                    (row + 1, startCol + 1),
                    (row + 2, startCol + 1),
                    (row + 1, startCol + 2)
                },
            2 =>
                /*
                 * ..#
                 * ..#
                 * ###
                 */
                new List<(int row, int col)>
                {
                    (row, startCol),
                    (row, startCol + 1),
                    (row, startCol + 2),
                    (row + 1, startCol + 2),
                    (row + 2, startCol + 2)
                },
            3 =>
                /*
                 * #
                 * #
                 * #
                 * #
                 */
                new List<(int row, int col)>
                {
                    (row, startCol), (row + 1, startCol), (row + 2, startCol), (row + 3, startCol)
                },
            4 =>
                /*
                 * ##
                 * ##
                 */
                new List<(int row, int col)>
                {
                    (row, startCol), (row + 1, startCol), (row, startCol + 1), (row + 1, startCol + 1)
                },
            _ => throw new InvalidOperationException("Not a valid rock selection")
        };
    }

    private static int GetNewRockEntryRow(List<List<string>> grid)
    {
        // Ensure there is the necessary space above the current highest row
        var highestRow = GetHighestRow(grid);
        return highestRow == -1 ? 3 : highestRow + 4;
    }

    private static int GetHighestRow(List<List<string>> grid)
    {
        for (var i = grid.Count - 1; i >= 0; i--)
        {
            if (grid[i].Any(x => x == "#"))
            {
                return i;
            }
        }

        return -1; // Floor
    }

    private static void PrintGrid(List<List<string>> grid)
    {
        var rowsToInclude = grid.Select((x, i) => x.Any(y => y != ".") ? i : 3).Max();
        for (var row = rowsToInclude; row >= 0; row--)
        {
            // File.AppendAllLines("test.txt", new[] {$"|{string.Join("", grid[row])}|"});
            Console.WriteLine($"|{string.Join("", grid[row])}|");

            if (row == 0)
            {
                // File.AppendAllLines("test.txt", new [] {$"+-------+"});
                Console.WriteLine("+-------+");
            }
        }

        Console.WriteLine();
    }
}