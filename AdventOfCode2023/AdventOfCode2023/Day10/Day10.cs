namespace AdventOfCode2023.Day10;

public static class Day10
{
    public static long CountSteps(string[] input)
    {
        var grid = ParseInput(input);
        PrintGrid(grid);
        Console.WriteLine();

        var start = GetStartPosition(grid);
        Console.WriteLine($"Start position: {start}");
        Console.WriteLine();
        
        var scoredGrid = ScoreGrid(grid, start);
        PrintGrid(scoredGrid);
        Console.WriteLine();
        
        return GetMaxScore(scoredGrid);
    }

    private static int[][] ScoreGrid(char[][] grid, (int row, int col) start)
    {
        var scoredGrid = new int[grid.GetLongLength(0)][];
        for (var row = 0; row < grid.GetLongLength(0); row++)
        {
            scoredGrid[row] = new int[grid.GetLongLength(1)];
        }
        
        var nextLocations = GetNextLocations(grid, start);
        while (nextLocations.Count != 0)
        {
            var locationsToAdd = new List<(int, int)>();
            foreach (var location in nextLocations)
            {
                scoredGrid[location.row][location.col]++;
                locationsToAdd.AddRange(GetNextLocations(grid, location));
            }
            
            nextLocations = locationsToAdd;
        }

        return scoredGrid;
    }

    private static List<(int row, int col)> GetNextLocations(char[][] grid, (int row, int col) location)
    {
        // TODO: don't return the location we came from
        throw new NotImplementedException();
    }

    private static long GetMaxScore(int[][] scoredGrid)
    {
        var maxScore = 0;
        for (var row = 0; row < scoredGrid.GetLongLength(0); row++)
        {
            for (var col = 0; col < scoredGrid.GetLongLength(1); col++)
            {
                if (scoredGrid[row][col] > maxScore)
                {
                    maxScore = scoredGrid[row][col];
                }
            }
        }

        return maxScore;
    }

    private static (int row, int col) GetStartPosition(char[][] grid)
    {
        for (var row = 0; row < grid.GetLongLength(0); row++)
        {
            for (var col = 0; col < grid.GetLongLength(1); col++)
            {
                if (grid[row][col] == 'S')
                {
                    return (row, col);
                }
            }
        }

        Console.WriteLine("Something went wrong: ");
        Console.WriteLine();
        PrintGrid(grid);
        Console.WriteLine();
        throw new ArgumentException("Grid had no start position");
    }

    private static char[][] ParseInput(string[] input)
    {
        return input.Select(x => x.ToCharArray()).ToArray();
    }
    
    private static void PrintGrid(char[][] grid)
    {
        foreach (var row in grid)
        {
            foreach (var col in row)
            {
                Console.Write(col);
            }
            Console.WriteLine();
        }
    }
    
    private static void PrintGrid(int[][] grid)
    {
        foreach (var row in grid)
        {
            foreach (var col in row)
            {
                Console.Write(col);
            }
            Console.WriteLine();
        }
    }
}