namespace AdventOfCode2016.Day8;

public static class Day8
{
    public static void PrintCode(string[] input, int row, int col)
    {
        PrintGrid(GetLitPixels(input, row, col));
    }

    private static void PrintGrid(bool[,] grid)
    {
        for (var row = 0; row < grid.GetLength(0); row++)
        {
            var rowStr = new List<bool>();
            for (var col = 0; col < grid.GetLength(1); col++)
            {
                rowStr.Add(grid[row, col]);
            }

            Console.WriteLine(string.Join("", rowStr.Select(x => x ? "#" : ".")));
        }
    }

    public static long CountLitPixels(string[] input, int row, int col)
    {
        return GetLitPixels(input, row, col).Cast<bool>().Count(x => x);
    }

    private static bool[,] GetLitPixels(string[] input, int row, int col)
    {
        var instructions = ParseInstructions(input);

        var grid = new bool[row, col];

        foreach (var instruction in instructions)
        {
            UpdateGrid(grid, instruction);
        }

        return grid;
    }

    private static void UpdateGrid(bool[,] grid, Instruction instruction)
    {
        switch (instruction)
        {
            case Rect rect:
            {
                UpdateGridWithRect(grid, rect);
                break;
            }
            case RotateColumn rotateColumn:
            {
                UpdateGridWithRotateColumn(grid, rotateColumn);
                break;
            }
            case RotateRow rotateRow:
            {
                UpdateGridWithRotateRow(grid, rotateRow);
                break;
            }
        }
    }

    private static void UpdateGridWithRotateRow(bool[,] grid, RotateRow rotateRow)
    {
        var numCols = grid.GetLength(1);
        var distanceToMove = rotateRow.Distance % numCols;
        var tempStorage = new bool[numCols * 2];
        
        // Update tempStorage from grid
        for (var col = 0; col < numCols; col++)
        {
            tempStorage[col + distanceToMove] = grid[rotateRow.TargetRow, col];
        }

        var movers = tempStorage[numCols..(numCols + distanceToMove)];
        
        // Rotate 'overhangs' to the front
        for (var i = 0; i < movers.Length; i++)
        {
            tempStorage[i] = movers[i];
        }

        // Update grid from tempStorage
        for (var i = 0; i < numCols; i++)
        {
            grid[rotateRow.TargetRow, i] = tempStorage[i];
        }
    }

    private static void UpdateGridWithRotateColumn(bool[,] grid, RotateColumn rotateColumn)
    {
        var numRows = grid.GetLength(0);
        var distanceToMove = rotateColumn.Distance % numRows;
        var tempStorage = new bool[numRows * 2];

        // Update tempStorage from grid
        for (var row = 0; row < numRows; row++)
        {
            tempStorage[row + distanceToMove] = grid[row, rotateColumn.TargetColumn];
        }

        var movers = tempStorage[numRows..(numRows + distanceToMove)];
        
        // Rotate 'overhangs' to the front
        for (var i = 0; i < movers.Length; i++)
        {
            tempStorage[i] = movers[i];
        }

        // Update grid from tempStorage
        for (var i = 0; i < numRows; i++)
        {
            grid[i, rotateColumn.TargetColumn] = tempStorage[i];
        }
    }

    private static void UpdateGridWithRect(bool[,] grid, Rect rect)
    {
        for (var row = 0; row < rect.Rows; row++)
        {
            for (var col = 0; col < rect.Cols; col++)
            {
                grid[row, col] = true;
            }
        }
    }

    private static IEnumerable<Instruction> ParseInstructions(string[] input)
    {
        foreach (var instruction in input)
        {
            if (instruction[..4] == "rect")
            {
                var size = instruction
                    .Split(" ")[1]
                    .Split("x")
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                
                yield return new Rect(size[1], size[0]);
            }
            else
            {
                var target = instruction
                    .Split("=")[1]
                    .Split(" by ")
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
                
                if (instruction[..15] == "rotate column x")
                {
                    yield return new RotateColumn(target[0], target[1]);
                }
                else
                {
                    yield return new RotateRow(target[0], target[1]);
                }
            }
        }
    }

    private record Instruction;

    private record Rect(int Rows, int Cols) : Instruction;

    private record RotateColumn(int TargetColumn, int Distance) : Instruction;

    private record RotateRow(int TargetRow, int Distance) : Instruction;
}