namespace AdventOfCode2023.Day3;

public static class Day3
{
    public static int SumGearNumbers(IEnumerable<string> input)
    {
        var grid = input.Select(l => l.ToCharArray()).ToArray();

        var gearCoords = new List<(int x, int y)>();
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] == '*')
                {
                    gearCoords.Add((row, col));
                }
            }
        }

        return 0;
    }
    
    public static int SumPartNumbers(IEnumerable<string> input)
    {
        var grid = input.Select(l => l.ToCharArray()).ToArray();

        var partNumbers = new List<int>();
        for (var row = 0; row < grid.Length; row++)
        {
            var inNum = false;
            var numStart = 0;
            var numEnd = 0;
            var numStr = "";
            for (var col = 0; col < grid[0].Length; col++)
            {
                var isDigit = char.IsDigit(grid[row][col]);
                if (isDigit && inNum)
                {
                    numEnd = col;
                    numStr += grid[row][col].ToString();
                }

                if (isDigit & !inNum)
                {
                    inNum = true;
                    numStart = col;
                    numEnd = col;
                    numStr += grid[row][col].ToString();
                }

                if (!isDigit)
                {
                    inNum = false;

                    if (numStr.Length == 0)
                    {
                        continue;
                    }

                    if (IsPartNumber(row, numStart, numEnd, grid))
                    {
                        partNumbers.Add(int.Parse(numStr));
                    }

                    numStr = "";
                }
            }
            
            if (numStr.Length > 0)
            {
                if (IsPartNumber(row, numStart, numEnd, grid))
                {
                    partNumbers.Add(int.Parse(numStr));
                }
            }
        }
        
        return partNumbers.Sum();
    }

    private static bool IsPartNumber(int row, int numStart, int numEnd, char[][] grid)
    {
        for (var c = numStart - 1; c <= numEnd + 1; c++)
        {
            for (var r = row - 1; r <= row + 1; r++)
            {
                var inEngine = r >= 0 && c >= 0 && r < grid.Length && c < grid[0].Length;
                if (inEngine)
                {
                    var isSymbol = grid[r][c] != '.' && !char.IsDigit(grid[r][c]);
                    if (isSymbol)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
