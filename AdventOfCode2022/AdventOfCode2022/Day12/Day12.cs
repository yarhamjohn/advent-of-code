namespace AdventOfCode2022.Day12;

public static class Day12
{
    public static long CountFewestSteps(string[] input)
    {
        var grid = ParseGrid(input);
        var distances = grid.Select(x => x.Select(_ => 0).ToArray()).ToArray();
        
        var (startX, startY) = GetPosition(grid, 'S');
        var end = GetPosition(grid, 'E');

        grid[startX][startY] = 'a';
        CalculateDistances(grid, distances, end, 0, 'z');
        
        return distances[startX][startY];
    }

    private static void CalculateDistances(char[][] grid, int[][] distances, (int x, int y) target, int distance, char height)
    {
        distances[target.x][target.y] = distance;
        
        distance++;

        var nextPositions = GetValidNextPositions(grid, distances, target, distance, height);

        if (!nextPositions.Any())
        {
            return;
        }

        foreach (var position in nextPositions)
        {
            CalculateDistances(grid, distances, position, distance, grid[position.x][position.y]);
        }
    }

    private static List<(int x, int y)> GetValidNextPositions(char[][] grid, int[][] distances, (int x, int y) target, int distance, char previousHeight)
    {
        // valid if on grid
        // valid if accessible (one below, same or any above)
        // valid if not already reached
        var candidates = new[]
            { (target.x + 1, target.y), (target.x - 1, target.y), (target.x, target.y + 1), (target.x, target.y - 1) };

        var validCandidates = new List<(int x, int y)>();

        foreach (var (x, y) in candidates)
        {
            var isOnGrid = x >= 0 && x < grid.Length && y >= 0 && y < grid.First().Length;
            if (!isOnGrid)
            {
                continue;
            }
            
            var isUnReached = distances[x][y] == 0 || distances[x][y] > distance;
            var isAccessible = grid[x][y] == previousHeight || previousHeight - grid[x][y] == 1 ||
                               grid[x][y] - previousHeight > 1;

            if (isAccessible && isOnGrid && isUnReached)
            {
                validCandidates.Add((x, y));
            }
        }

        return validCandidates.OrderBy(position => grid[position.x][position.y]).ToList();
    }

    private static (int x, int y) GetPosition(char[][] grid, char target)
    {
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                if (grid[row][col] == target)
                {
                    return (row, col);
                }
            }
        }

        throw new InvalidOperationException();
    }

    private static char[][] ParseGrid(string[] input)
    {
        return input.Select(x => x.ToCharArray()).ToArray();
    }
}