namespace AdventOfCode2022.Day17;

public static class Day17
{
    // Rolling window from last complete row
    
    
    public static long CalculateHeight(string input)
    {
        var grid = Enumerable.Range(0, GetMaxHeightOfGrid()).Select(_ => Enumerable.Range(0, 7).Select(_ => ".").ToList()).ToList();
        
        var jetIndex = 0;
        
        for (var i = 0; i < 2022; i++)
        {
            var rockPositions = AddNewRockToGrid(grid, i);

            jetIndex = HandleJetBlast(input, jetIndex, grid, ref rockPositions);

            while (true)
            {
                if (CanMoveDown(grid, rockPositions))
                {
                    rockPositions = MoveDown(grid, rockPositions);
                }
                else
                {
                    foreach (var rock in rockPositions)
                    {
                        FixRockPosition(grid, rock);
                    }

                    break;
                }

                jetIndex = HandleJetBlast(input, jetIndex, grid, ref rockPositions);
            }
        }
        
        return GetStartRow(grid) - 3;
    }

    private static int HandleJetBlast(string input, int jetIndex, List<List<string>> grid, ref List<(int row, int col)> rockPositions)
    {
        if (CanMoveDirection(input[jetIndex], grid, rockPositions))
        {
            rockPositions = MoveDirection(input[jetIndex], grid, rockPositions);
        }

        return ++jetIndex >= input.Length ? 0 : jetIndex;
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

    private static bool CanMoveDown(List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        return rockPositions.All(x => x.row != 0 && grid[x.row - 1][x.col] != "#");
    }
    
    private static bool CanMoveDirection(char c, List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        return c switch
        {
            '>' when rockPositions.All(x => x.col < 6) => rockPositions.All(x =>
                grid[x.row][x.col + 1] == "." || grid[x.row][x.col + 1] == "@"),
            '<' when rockPositions.All(x => x.col > 0) => rockPositions.All(x =>
                grid[x.row][x.col - 1] == "." || grid[x.row][x.col - 1] == "@"),
            _ => false
        };
    }

    private static List<(int row, int col)> MoveDown(List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        var orderedPositions = rockPositions.OrderBy(x => x.row).ToArray();
        for (var i = 0; i < orderedPositions.Length; i++)
        {
            SetPositionToEmpty(grid, orderedPositions[i]);
            SetPositionToRock(grid, (orderedPositions[i].row - 1, orderedPositions[i].col));
        }

        return rockPositions.Select(x => (x.row - 1, x.col)).ToList();
    }

    private static List<(int row, int col)> MoveDirection(char c, List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        if (c == '>')
        {
            var orderedPositions = rockPositions.OrderByDescending(x => x.col).ToArray();
            for (var i = 0; i < orderedPositions.Length; i++)
            {
                SetPositionToEmpty(grid, orderedPositions[i]);
                SetPositionToRock(grid, (orderedPositions[i].row, orderedPositions[i].col + 1));
            }
            
            return rockPositions.Select(x => (x.row, x.col + 1)).ToList();
        }

        var orderedPositionsTwo = rockPositions.OrderBy(x => x.col).ToArray();
        for (var i = 0; i < orderedPositionsTwo.Length; i++)
        {
            SetPositionToEmpty(grid, orderedPositionsTwo[i]);
            SetPositionToRock(grid, (orderedPositionsTwo[i].row, orderedPositionsTwo[i].col - 1));
        }
            
        return rockPositions.Select(x => (x.row, x.col - 1)).ToList();
    }

    private static List<(int row, int col)> GetRockStartingPositions(List<List<string>> grid, long iteration)
    {
        const int startCol = 2;
        var row = GetStartRow(grid);

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

    private static int GetStartRow(List<List<string>> grid)
    {
        return grid.Select((x, i) => x.Any(y => y != ".") ? i + 4 : 3).Max();
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