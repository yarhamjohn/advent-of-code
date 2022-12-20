namespace AdventOfCode2022.Day17;

public static class Day17
{
    public static long CalculateHeight(string input)
    {
        var maxHeightOfRocks = 2022 / 5 * 13 + 4; // Because 2022 % 5 is 2, and the first 2 rocks have a possible height of 4
        var maxHeightOfGrid = maxHeightOfRocks + 3 + 4; // New rocks appear 3 lines above and tallest rock is 4.

        // 0 being just off the floor
        var grid = Enumerable.Range(0, maxHeightOfGrid).Select(_ => Enumerable.Range(0, 7).Select(_ => ".").ToList()).ToList();
        
        // PrintGrid(grid);

        var jetIndex = 0;
        
        for (var i = 0; i < 2022; i++)
        {
            var rockPositions = GetRockPositions(grid, i);
            AddToGrid(grid, rockPositions);
            // PrintGrid(grid);

            if (CanMoveDirection(input[jetIndex], grid, rockPositions))
            {
                rockPositions = MoveDirection(input[jetIndex], grid, rockPositions);
                // PrintGrid(grid);

                jetIndex++;

                if (jetIndex >= input.Length)
                {
                    jetIndex = 0;
                }
            }
            else
            {
                // PrintGrid(grid);
            }

            while (true)
            {
                if (CanMoveDown(grid, rockPositions))
                {
                    rockPositions = MoveDown(grid, rockPositions);
                    // PrintGrid(grid);
                }
                else
                {
                    UpdateGrid(grid, rockPositions);
                    // PrintGrid(grid);
                    break;
                }

                if (CanMoveDirection(input[jetIndex], grid, rockPositions))
                {
                    rockPositions = MoveDirection(input[jetIndex], grid, rockPositions);
                }

                // PrintGrid(grid);

                jetIndex++;

                if (jetIndex >= input.Length)
                {
                    jetIndex = 0;
                }
            }
        }
        
        return GetStartRow(grid) - 3;
    }

    private static void AddToGrid(List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        foreach (var rock in rockPositions)
        {
            grid[rock.row][rock.col] = "@";
        }
    }

    private static List<(int row, int col)> MoveDirection(char c, List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        if (c == '>')
        {
            var orderedPositions = rockPositions.OrderByDescending(x => x.col).ToArray();
            for (var i = 0; i < orderedPositions.Length; i++)
            {
                grid[orderedPositions[i].row][orderedPositions[i].col] = ".";
                grid[orderedPositions[i].row][orderedPositions[i].col + 1] = "@";
            }
            
            return rockPositions.Select(x => (x.row, x.col + 1)).ToList();
        }

        var orderedPositionsTwo = rockPositions.OrderBy(x => x.col).ToArray();
        for (var i = 0; i < orderedPositionsTwo.Length; i++)
        {
            grid[orderedPositionsTwo[i].row][orderedPositionsTwo[i].col] = ".";
            grid[orderedPositionsTwo[i].row][orderedPositionsTwo[i].col - 1] = "@";
        }
            
        return rockPositions.Select(x => (x.row, x.col - 1)).ToList();
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

    private static void UpdateGrid(List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        foreach (var rock in rockPositions)
        {
            grid[rock.row][rock.col] = "#";
        }
    }

    private static List<(int row, int col)> MoveDown(List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        var orderedPositions = rockPositions.OrderBy(x => x.row).ToArray();
        for (var i = 0; i < orderedPositions.Length; i++)
        {
            grid[orderedPositions[i].row][orderedPositions[i].col] = ".";
            grid[orderedPositions[i].row - 1][orderedPositions[i].col] = "@";
        }

        return rockPositions.Select(x => (x.row - 1, x.col)).ToList();
    }

    private static bool CanMoveDown(List<List<string>> grid, List<(int row, int col)> rockPositions)
    {
        if (rockPositions.Any(x => x.row == 0))
        {
            return false;
        }
        
        return rockPositions.All(x => grid[x.row - 1][x.col] == "." || grid[x.row - 1][x.col] == "@");
    }

    private static List<(int row, int col)> GetRockPositions(List<List<string>> grid, int iteration)
    {
        var startCol = 2;
        var row = GetStartRow(grid);

        switch (iteration % 5)
        {
            case 0:
                // ####
                return new List<(int row, int col)> { (row, startCol), (row, startCol + 1), (row, startCol + 2), (row, startCol + 3) };
            case 1: 
                /*
                 * .#.
                 * ###
                 * .#.
                 */
                return new List<(int row, int col)> { (row + 1, startCol), (row, startCol + 1), (row + 1, startCol + 1), (row + 2, startCol + 1), (row + 1, startCol + 2) };
            case 2: 
                /*
                 * ..#
                 * ..#
                 * ###
                 */
                return new List<(int row, int col)> { (row, startCol), (row, startCol + 1), (row, startCol + 2), (row + 1, startCol + 2), (row + 2, startCol + 2) };
            case 3: 
                /*
                 * #
                 * #
                 * #
                 * #
                 */
                return new List<(int row, int col)> { (row, startCol), (row + 1, startCol), (row + 2, startCol), (row + 3, startCol) };
            case 4: 
                /*
                 * ##
                 * ##
                 */
                return new List<(int row, int col)> { (row, startCol), (row + 1, startCol), (row, startCol + 1), (row + 1, startCol + 1) };
            default:
                throw new InvalidOperationException("Not a valid rock selection");
        }
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