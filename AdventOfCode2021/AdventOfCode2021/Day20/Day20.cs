namespace AdventOfCode2021.Day20;

public static class Day20
{
    private const int InfiniteScalarEmulator = 50; // must be bigger than 50 for some reason
    public static long CalculateLitPixels(string[] input, int turns)
    {
        var algorithm = input.First().ToArray();
        var startGrid = GetGrid(input.Skip(2).ToArray());
        var finalGrid = CreateFinalGrid(turns, startGrid);

        while (turns > 0)
        {
            finalGrid = UpdateImage(finalGrid, algorithm);
            turns--;
        }

        var result = TrimToResultGrid(finalGrid);

        return result.Cast<char>().Count(x => x == '#');
    }

    private static char[,] TrimToResultGrid(char[,] finalGrid)
    {
        var result = new char[finalGrid.GetLength(0) - InfiniteScalarEmulator * 2,
            finalGrid.GetLength(1) - InfiniteScalarEmulator * 2];
        for (var i = InfiniteScalarEmulator; i < finalGrid.GetLength(0) - InfiniteScalarEmulator; i++)
        {
            for (var j = InfiniteScalarEmulator; j < finalGrid.GetLength(1) - InfiniteScalarEmulator; j++)
            {
                result[i - InfiniteScalarEmulator, j - InfiniteScalarEmulator] = finalGrid[i, j];
            }
        }

        return result;
    }

    private static char[,] CreateFinalGrid(int turns, char[,] grid)
    {
        var finalGrid = new char[grid.GetLength(0) + turns * 2 + InfiniteScalarEmulator * 2, grid.GetLength(1) + turns * 2 + InfiniteScalarEmulator * 2];
        
        PopulateEmptyGrid(finalGrid);
        PopulateFinalGrid(turns, grid, finalGrid);

        return finalGrid;
    }

    private static void PopulateEmptyGrid(char[,] finalGrid)
    {
        for (var i = 0; i < finalGrid.GetLength(0); i++)
        {
            for (var j = 0; j < finalGrid.GetLength(1); j++)
            {
                finalGrid[i, j] = '.';
            }
        }
    }

    private static void PopulateFinalGrid(int turns, char[,] startGrid, char[,] finalGrid)
    {
        for (var i = 0; i < startGrid.GetLength(0); i++)
        {
            for (var j = 0; j < startGrid.GetLength(1); j++)
            {
                if (startGrid[i, j] == '#')
                {
                    finalGrid[i + turns + InfiniteScalarEmulator, j + turns + InfiniteScalarEmulator] = '#';
                }
            }
        }
    }

    private static void PrintGrid(char[,] grid)
    {
        Console.WriteLine();
        
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j]);
            }

            Console.WriteLine();
        }
    }

    private static char[,] UpdateImage(char[,] grid, IReadOnlyList<char> algorithm)
    {
        var nextGrid = new char[grid.GetLength(0), grid.GetLength(1)];
        PopulateEmptyGrid(nextGrid);

        for (var row = 1; row < grid.GetLength(0) - 1; row++)
        {
            for (var col = 1; col < grid.GetLength(1) - 1; col++)
            {
                var pixels = GetPixels(grid, row, col);
                var binaryString = string.Join("", pixels.Select(x => x == '.' ? "0" : "1"));
                nextGrid[row, col] = algorithm[Convert.ToInt32(binaryString, 2)];
            }
        }
        
        return nextGrid;
    }

    private static IEnumerable<char> GetPixels(char[,] grid, int row, int col)
    {
        var pixels = new List<char>();
        for (var i = row - 1; i <= row + 1; i++)
        {
            for (var j = col - 1; j <= col + 1; j++)
            {
                pixels.Add(grid[i, j]);
            }
        }

        return pixels;
    }

    private static char[,] GetGrid(IReadOnlyList<string> input)
    {
        var grid = new char[input.Count, input.Count];
        for (var row = 0; row < input.Count; row++)
        {
            var line = input[row];
            for (var col = 0; col < line.Length; col++)
            {
                grid[row, col] = line[col];
            }
        }

        return grid;
    }
}