namespace AdventOfCode2021.Day15;

public static class Day15
{
    public static long CalculatePath(string[] input)
    {
        // Create risk grid from input
        var grid = CreateGrid(input);
        
        // Create initial cost grid (default = Int.MaxValue)
        var costGrid = CreateCostGrid(grid);
        
        // Set one known cost, which is the starting point
        costGrid[0, 0] = grid[0, 0];

        CalculateFullCostGrid(grid, costGrid);

        return costGrid[costGrid.GetLength(0) - 1, costGrid.GetLength(1) - 1] - 1;
    }
    
    public static long CalculatePathBig(string[] input)
    {
        // Create risk grid from input
        var grid = CreateGridBig(input);
        
        // Create initial cost grid (default = Int.MaxValue)
        var costGrid = CreateCostGrid(grid);
        
        // Set one known cost, which is the starting point
        costGrid[0, 0] = grid[0, 0];

        CalculateFullCostGrid(grid, costGrid);

        return costGrid[costGrid.GetLength(0) - 1, costGrid.GetLength(1) - 1] - 1;
    }

    private static void CalculateFullCostGrid(int[,] grid, int[,] costGrid)
    {
        // Create possible movement arrays
        var dx = new[] { 1, 0, -1, 0 };
        var dy = new[] { 0, 1, 0, -1 };

        // Initialise a queue with the source cell in it
        var queue = new Queue<Cell>();
        queue.Enqueue(new(0, 0, grid[0, 0]));

        while (queue.Count > 0)
        {
            var currentCell = queue.Dequeue();

            for (var neighbours = 0; neighbours < 4; neighbours++)
            {
                var row = currentCell.X + dx[neighbours];
                var col = currentCell.Y + dy[neighbours];

                if (IsInGrid(grid, row, col))
                {
                    var currentNeighbourCost = costGrid[row, col];
                    var potentialNeighbourCost = grid[row, col] + costGrid[currentCell.X, currentCell.Y];

                    if (currentNeighbourCost > potentialNeighbourCost)
                    {
                        costGrid[row, col] = potentialNeighbourCost;
                        queue.Enqueue(new(row, col, potentialNeighbourCost));
                    }
                }
            }
        }
    }

    private static int[,] CreateCostGrid(int[,] grid)
    {
        var costGrid = new int[grid.GetLength(0), grid.GetLength(1)];

        for (var i = 0; i < costGrid.GetLength(0); i++)
        {
            for (var j = 0; j < costGrid.GetLength(1); j++)
            {
                costGrid[i, j] = int.MaxValue;
            }
        }
        
        return costGrid;
    }

    private static bool IsInGrid(int[,] grid, int x, int y)
    {
        return x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1);
    }

    private static int[,] CreateGrid(string[] input)
    {
        var grid = new int[input.Length, input.Length];
        for (var i = 0; i < input.Length; i++)
        {
            var row = input[i];
            for (var j = 0; j < row.Length; j++)
            {
                grid[i, j] = row[j] - '0';
            }
        }

        return grid;
    }   
    
    private static int[,] CreateGridBig(string[] input)
    {
        var initialGrid = CreateGrid(input);

        var bigGrid = new int[initialGrid.GetLength(0) * 5, initialGrid.GetLength(1) * 5];
        
        for (var row = 0; row < 5; row++)
        {
            for (var col = 0; col < 5; col++)
            {
                var dx = row * initialGrid.GetLength(0);
                var dy = col * initialGrid.GetLength(1);

                var incrementer = row + col;

                for (var i = 0; i < initialGrid.GetLength(0); i++)
                {
                    for (var j = 0; j < initialGrid.GetLength(1); j++)
                    {
                        var newCost = initialGrid[i, j] + incrementer;
                        bigGrid[i + dx, j + dy] = newCost > 9 ? newCost - 9 : newCost;
                    }
                }
            }    
        }

        return bigGrid;
    }

    private record Cell(int X, int Y, int Cost);
}