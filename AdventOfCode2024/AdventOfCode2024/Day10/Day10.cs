namespace AdventOfCode2024.Day10;

public static class Day10
{
    public static long Part1(string[] input)
    {
        var map = ParseInput(input);

        var trailEnds = GetLocations(map, 9);

        return trailEnds.SelectMany(x => FindTrailHeads(map, x)).Count();
    }

    private static List<(int row, int col)> FindTrailHeads(List<int[]> map, (int row, int col) location)
    {
        if (map[location.row][location.col] == 0)
        {
            return [location];
        }

        return GetNextLocations(map, location)
            .SelectMany(y => FindTrailHeads(map, y))
            .Distinct()
            .ToList();
    }

    private static List<(int row, int col)> GetNextLocations(List<int[]> map, (int row, int col) location)
    {
        var allLocations = new List<(int row, int col)> {
            (location.row - 1, location.col), 
            (location.row + 1, location.col), 
            (location.row, location.col - 1), 
            (location.row, location.col + 1)};
        
        return allLocations
            .Where(x => x.row >= 0 && x.row < map.Count && x.col >= 0 && x.col < map.First().Length)
            .Where(x => map[x.row][x.col] == map[location.row][location.col] - 1)
            .ToList();
    }
            
    private static List<(int row, int col)> GetLocations(List<int[]> map, int height)
    {
        var locations = new List<(int row, int col)>();

        for (var row = 0; row < map.Count; row++)
        {
            for (var col = 0; col < map.First().Length; col++)
            {
                if (map[row][col] == height)
                {
                    locations.Add((row, col));
                }
            }
        }

        return locations;
    }

    public static long Part2(string[] input)
    {
        return 0;
    }

    private static List<int[]> ParseInput(string[] input)
    {
        var result = new List<int[]>();

        foreach (var row in input)
        {
            var newRow = new int[input.First().Length];
            
            for (var col = 0; col < input.First().Length; col++)
            {
                newRow[col] = row[col] - '0';
            }
            
            result.Add(newRow);
        }

        return result;
    }
}