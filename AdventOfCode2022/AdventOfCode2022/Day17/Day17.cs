namespace AdventOfCode2022.Day17;

public static class Day17
{
    public static long CalculateHeight(string input)
    {
        var grid = Enumerable.Range(0, GetMaxHeightOfGrid()).Select(_ => Enumerable.Range(0, 7).Select(_ => ".").ToList()).ToList();
        
        var jetIndex = 0;
        
        for (var i = 0; i < 2022; i++)
        {
            // PrintGrid(grid);

            var rockPositions = AddNewRockToGrid(grid, i);

            // PrintGrid(grid);

            if (CanMove(input[jetIndex], grid, rockPositions))
            {
                rockPositions = Move(input[jetIndex], grid, rockPositions);
            }

            jetIndex = ++jetIndex >= input.Length ? 0 : jetIndex;

            while (true)
            {
                // PrintGrid(grid);

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

                // PrintGrid(grid);

                if (CanMove(input[jetIndex], grid, rockPositions))
                {
                    rockPositions = Move(input[jetIndex], grid, rockPositions);
                }

                jetIndex = ++jetIndex >= input.Length ? 0 : jetIndex;
            }
        }
        
        PrintGrid(grid);

        return GetHighestRow(grid) + 1; // 0-based index
    }

    public static long CalculateHeightHuge(string input)
    {
        var grid = Enumerable.Range(0, 10).Select(_ => Enumerable.Range(0, 7).Select(_ => ".").ToList()).ToList();
        
        var jetIndex = 0;

        // Rolling window from last complete row
        var height = 0L;

        for (var i = 0L; i < 1000000000000; i++)
        {
            if (i is 0 or 10 or 100 or 1000 or 10000 or 100000 or 1000000 or 10000000 or 100000000 or 1000000000 or 10000000000 or 100000000000)
            {
                Console.WriteLine(height);
                Console.WriteLine(grid.Count);
                PrintGrid(grid);
            }

            var newBottomRow = GetNewBottomRow(grid);
            grid = TruncateGrid(grid, newBottomRow);
            height += newBottomRow;

            var spaceNeeded = GetNewRockEntryRow(grid) + 4; // Include space for the new rock
            if (spaceNeeded >= grid.Count)
            {
                grid = grid.Concat(Enumerable.Range(0, spaceNeeded - grid.Count).Select(_ => Enumerable.Range(0, 7).Select(_ => ".").ToList()))
                    .ToList();
            }

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

        Console.WriteLine(height);
        Console.WriteLine(grid.Count);
        PrintGrid(grid);
        
        height += GetHighestRow(grid) + 1; // 0-based index

        return height;
    }

    private static List<List<string>> TruncateGrid(List<List<string>> grid, int newBottomRow)
    {
        return grid.Where((_, i) => i >= newBottomRow).ToList();
    }

    private static int GetNewBottomRow(List<List<string>> grid)
    {
        var positionsFilled = new Dictionary<int, bool> { { 0, false }, { 1, false }, { 2, false }, { 3, false }, {4, false}, {5, false}, {6, false} };

        var row = grid.Count;
        while (!positionsFilled.All(x => x.Value))
        {
            row--;
            if (row == 0)
            {
                break;
            }
            
            for (var i = 0; i < 7; i++)
            {
                if (grid[row][i] == "#")
                {
                    positionsFilled[i] = true;
                }
            }
        }

        return row;
    }

    private static int GetMaxHeightOfGrid()
    {
        const int maxHeightOfOneSetOfRocks = 13;
        const int maxHeightOfAllRocks = 2022 / 5 * maxHeightOfOneSetOfRocks + 4; // Because 2022 % 5 is 2, and the first 2 rocks have a possible height of 4
        return maxHeightOfAllRocks + 3 + 4; // New rocks appear 3 lines above and tallest rock is 4.
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
            Console.WriteLine($"|{string.Join("", grid[row])}|");

            if (row == 0)
            {
                Console.WriteLine("+-------+");
            }
        }

        Console.WriteLine();
    }
}