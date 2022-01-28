namespace AdventOfCode2021.Day13;

public static class Day13
{
    public static int CalculateFirstFold(string[] input)
    {
        var grid = GetGrid(input.Where(line => line.Contains(',')));
        var (direction, position) = GetFolds(input.Where(line => line.Contains('='))).First();

        grid = direction == "y" ? FoldHorizontally(grid, position) : FoldVertically(grid, position);
        
        return grid.Cast<char>().Count(x => x == '#');
    }    
    
    public static int CalculateAllFolds(string[] input)
    {
        var grid = GetGrid(input.Where(line => line.Contains(',')));
        var folds = GetFolds(input.Where(line => line.Contains('=')));

        foreach (var (direction, position) in folds)
        {
            grid = direction == "y" ? FoldHorizontally(grid, position) : FoldVertically(grid, position);
        }

        PrintResultingLetters(grid);

        return grid.Cast<char>().Count(x => x == '#');
    }

    private static void PrintResultingLetters(char[,] grid)
    {
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                Console.Write(grid[i, j] == '#' ? grid[i, j] : '.');
            }

            Console.WriteLine();
        }
    }

    private static char[,] FoldVertically(char[,] grid, int column)
    {
        var leftGrid = new char[grid.GetLength(0), column];
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < column; j++)
            {
                leftGrid[i, j] = grid[i, j];
            }
        }

        var numColumnsIgnored = grid.GetLength(1) - column;
        var rightGrid = new char[grid.GetLength(0), numColumnsIgnored - 1];
        for (var i = 0; i < grid.GetLength(0); i++)
        {
            for (var j = column + 1; j < grid.GetLength(1); j++)
            {
                rightGrid[i, j - numColumnsIgnored] = grid[i, j];
            }
        }

        for (var y = 0; y < rightGrid.GetLength(1); y++)
        {
            for (var x = 0; x < rightGrid.GetLength(0); x++)
            {
                var targetColumn = leftGrid.GetLength(1) - 1 - y;
                if (leftGrid[x, targetColumn] == '#' || rightGrid[x, y] == '#')
                {
                    leftGrid[x, targetColumn] = '#';
                }
            }
        }

        return leftGrid;
    }

    private static char[,] FoldHorizontally(char[,] grid, int row)
    {
        var topGrid = new char[row, grid.GetLength(1)];
        for (var i = 0; i < row; i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                topGrid[i, j] = grid[i, j];
            }
        }

        var numRowsIgnored = grid.GetLength(0) - row;
        var bottomGrid = new char[numRowsIgnored - 1, grid.GetLength(1)];
        for (var i = row + 1; i < grid.GetLength(0); i++)
        {
            for (var j = 0; j < grid.GetLength(1); j++)
            {
                bottomGrid[i - numRowsIgnored, j] = grid[i, j];
            }
        }

        for (var x = 0; x < bottomGrid.GetLength(0); x++)
        {
            for (var y = 0; y < bottomGrid.GetLength(1); y++)
            {
                var targetRow = topGrid.GetLength(0) - 1 - x;
                if (topGrid[targetRow, y] == '#' || bottomGrid[x, y] == '#')
                {
                    topGrid[targetRow, y] = '#';
                }
            }
        }

        return topGrid;
    }

    private static IEnumerable<(string direction, int position)> GetFolds(IEnumerable<string> input) =>
        input
            .Select(line => line
                .Split("fold along ")
                .Last()
                .Split("=")
                .ToArray())
            .Select(pair => (direction: pair[0], position: Convert.ToInt32(pair[1])));

    private static char[,] GetGrid(IEnumerable<string> input)
    {
        var coordinates = input.Select(line =>
        {
            var elements = line.Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            return (col: elements[0], row: elements[1]);
        }).ToArray();

        var grid = new char[coordinates.Max(c => c.row) + 1, coordinates.Max(c => c.col) + 1];

        foreach (var (col, row) in coordinates)
        {
            grid[row, col] = '#';
        }

        return grid;
    }
}