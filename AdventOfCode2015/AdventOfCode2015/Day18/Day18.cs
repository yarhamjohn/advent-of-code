namespace AdventOfCode2015.Day18;

public static class Day18
{
    public static long CountLights(IEnumerable<string> input, int steps)
    {
        var grid = ParseInput(input);

        Console.WriteLine("Initial state:");
        PrintGrid(grid);
        Console.WriteLine();

        grid = IterateSteps(steps, grid);

        return CountOnLights(grid);
    }
    
    public static long CountLightsWithStuckOn(IEnumerable<string> input, int steps)
    {
        var grid = ParseInput(input);

        SetCornersOn(grid);

        Console.WriteLine("Initial state:");
        PrintGrid(grid);
        Console.WriteLine();

        grid = IterateSteps(steps, grid, true);

        return CountOnLights(grid);
    }

    private static void SetCornersOn(char[][] grid)
    {
        var maxCoord = grid.GetLength(0) - 1;
        
        grid[0][0] = '#';
        grid[maxCoord][maxCoord] = '#';
        grid[0][maxCoord] = '#';
        grid[maxCoord][0] = '#';
    }

    private static char[][] IterateSteps(int steps, char[][] grid, bool stickCornersOn = false)
    {
        for (var step = 1; step <= steps; step++)
        {
            grid = UpdateGrid(grid);

            if (stickCornersOn)
            {
                SetCornersOn(grid);
            }

            Console.WriteLine($"After step {step}:");
            PrintGrid(grid);
            Console.WriteLine();
        }

        return grid;
    }

    private static int CountOnLights(char[][] grid) =>
        grid.SelectMany(x => x).Count(y => y == '#');

    private static char[][] UpdateGrid(char[][] grid)
    {
        var newGrid = grid.Select(x => x.ToArray()).ToArray();

        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(0); j++)
            {
                var neighbourCount = CountNeighbours(grid, i, j);
                if (TurnOffLight(grid[i][j], neighbourCount))
                {
                    newGrid[i][j] = '.';
                }

                if (TurnLightOn(grid[i][j], neighbourCount))
                {
                    newGrid[i][j] = '#';
                }
            }
        }

        return newGrid;
    }

    private static bool TurnLightOn(char currentValue, int neighbourCount) =>
        currentValue == '.' && neighbourCount == 3;

    private static bool TurnOffLight(char currentValue, int neighbourCount) =>
        currentValue == '#' && neighbourCount < 2 || neighbourCount > 3;

    private static int CountNeighbours(char[][] grid, int row, int col)
    {
        var count = 0;

        for (var x = row - 1; x <= row + 1; x++)
        {
            for (var y = col - 1; y <= col + 1; y++)
            {
                if (x == row && y == col || IsNotInGrid(grid, x, y))
                {
                    continue;
                }

                count += grid[x][y] == '#' ? 1 : 0;
            }
        }

        return count;
    }

    private static bool IsNotInGrid(char[][] grid, int x, int y) =>
        x < 0
        || y < 0
        || x >= grid.GetLength(0)
        || y >= grid.GetLength(0);

    private static void PrintGrid(char[][] grid)
    {
        foreach (var row in grid)
        {
            Console.WriteLine(string.Join("", row));
        }
    }

    private static char[][] ParseInput(IEnumerable<string> input)
        => input.Select(x => x.Select(y => y).ToArray()).ToArray();
}