using System.Text;

namespace AdventOfCode2023.Day3;

public static class Day3
{
    public static int SumPartNumbers(IEnumerable<string> input)
    {
        var grid = input.Select(l => l.ToCharArray()).ToArray();
        
        return GetSymbolCoords(grid)
            .Select(symbol => GetNeighbouringNums(symbol, GetNumCoords(grid)))
            .Where(symbolNums => symbolNums.Count >= 1)
            .SelectMany(symbolNums => symbolNums)
            .Select(x => GetNum(x, grid))
            .Sum();
    }
    
    public static int SumGearNumbers(IEnumerable<string> input)
    {
        var grid = input.Select(l => l.ToCharArray()).ToArray();

        return GetGearCoords(grid)
            .Select(gear => GetNeighbouringNums(gear, GetNumCoords(grid)))
            .Where(gearNums => gearNums.Count == 2)
            .Sum(gearNums => gearNums
                .Select(x => GetNum(x, grid))
                .Aggregate((a, b) => a * b));
    }
    
    private static int GetNum(List<(int x, int y)> numCoords, char[][] grid)
    {
        var builder = new StringBuilder();
        foreach (var coord in numCoords)
        {
            builder.Append(grid[coord.x][coord.y]);
        }
        return int.Parse(builder.ToString());
    }

    private static List<List<(int x, int y)>> GetNeighbouringNums((int x, int y) target, List<List<(int x, int y)>> numCoords)
    {
        return numCoords
            .Where(num => num
                .Any(coord =>
                    coord.x >= target.x - 1 &&
                    coord.x <= target.x + 1 &&
                    coord.y >= target.y - 1 &&
                    coord.y <= target.y + 1))
            .ToList();
    }

    private static List<List<(int x, int y)>> GetNumCoords(char[][] grid)
    {
        var numCoords = new List<List<(int x, int y)>>();
        for (var row = 0; row < grid.Length; row++)
        {
            var coords = new List<(int x, int y)>();
            
            for (var col = 0; col < grid[0].Length; col++)
            {
                var isDigit = char.IsDigit(grid[row][col]);
                if (isDigit)
                {
                    coords.Add((row, col));
                }
                else
                {
                    if (coords.Count > 0)
                    {
                        numCoords.Add(coords);
                    }

                    coords = [];
                }
            }
            
            if (coords.Count > 0)
            {
                numCoords.Add(coords);
            }
        }

        return numCoords;
    }

    private static List<(int x, int y)> GetGearCoords(char[][] grid)
    {
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

        return gearCoords;
    }

    private static List<(int x, int y)> GetSymbolCoords(char[][] grid)
    {
        var symbolCoords = new List<(int x, int y)>();
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[0].Length; col++)
            {
                if (grid[row][col] != '.' && !char.IsDigit(grid[row][col]))
                {
                    symbolCoords.Add((row, col));
                }
            }
        }

        return symbolCoords;
    }
}
