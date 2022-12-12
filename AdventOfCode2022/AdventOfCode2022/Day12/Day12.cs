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
    
    public static long CountShortestWalk(string[] input)
    {
        var grid = ParseGrid(input);
        var distances = grid.Select(x => x.Select(_ => 0).ToArray()).ToArray();
        
        var (startX, startY) = GetPosition(grid, 'S');
        var end = GetPosition(grid, 'E');

        grid[startX][startY] = 'a';
        CalculateDistances(grid, distances, end, 0, 'z');

        return GetShortestPathFromLowestPoint(grid, distances);
    }

    private static int GetShortestPathFromLowestPoint(char[][] grid, int[][] distances)
    {
        var minDistance = int.MaxValue;
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                if (grid[row][col] == 'a' && distances[row][col] != 0)
                {
                    if (distances[row][col] < minDistance)
                    {
                        minDistance = distances[row][col];
                    }
                }
            }
        }

        return minDistance;
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

    private static List<(int x, int y)> GetValidNextPositions(
        char[][] grid, int[][] distances, (int x, int y) target, int distance, char previousHeight)
    {
        var (targetX, targetY) = target;
        var candidates = new[]
        {
            (targetX + 1, targetY),
            (targetX - 1, targetY),
            (targetX, targetY + 1),
            (targetX, targetY - 1)
        };

        return GetValidCandidates(grid, distances, distance, previousHeight, candidates)
            .OrderBy(position => grid[position.x][position.y])
            .ToList();
    }

    private static List<(int x, int y)> GetValidCandidates(char[][] grid, int[][] distances, int distance, char previousHeight,
        (int, int)[] candidates)
    {
        var validCandidates = new List<(int x, int y)>();

        foreach (var (x, y) in candidates)
        {
            if (!IsOnGrid(grid, x, y))
            {
                continue;
            }

            if (IsAccessible(grid, previousHeight, x, y) &&
                (IsUnreached(distances, x, y) || IsCloserThenPreviousRoute(distances, distance, x, y)))
            {
                validCandidates.Add((x, y));
            }
        }

        return validCandidates;
    }

    private static bool IsAccessible(char[][] grid, char previousHeight, int x, int y)
    {
        return grid[x][y] == previousHeight || previousHeight - grid[x][y] == 1 ||
               grid[x][y] - previousHeight > 1;
    }

    private static bool IsCloserThenPreviousRoute(int[][] distances, int distance, int x, int y)
    {
        return distances[x][y] > distance;
    }

    private static bool IsUnreached(int[][] distances, int x, int y)
    {
        return distances[x][y] == 0;
    }

    private static bool IsOnGrid(char[][] grid, int x, int y)
    {
        return x >= 0 && x < grid.Length && y >= 0 && y < grid.First().Length;
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