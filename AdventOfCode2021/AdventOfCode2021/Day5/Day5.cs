namespace AdventOfCode2021.Day5;

public static class Day5
{
    public static int CalculateDangerousVents(string[] input)
    {
        var coordinates = BuildCoordinates(input);

        var grid = BuildGrid(coordinates);

        PlaceVents(coordinates, grid);
        
        return CountDangerousVents(grid);
    }

    public static int CalculateDangerousVentsWithDiagonals(string[] input)
    {
        var coordinates = BuildCoordinates(input);

        var grid = BuildGrid(coordinates);

        PlaceVentsWithDiagonals(coordinates, grid);
        
        return CountDangerousVents(grid);
    }

    private static int CountDangerousVents(int[,] grid) => grid.Cast<int>().Count(elem => elem > 1);

    private static void PlaceVents(List<(Coordinate Start, Coordinate End)> coordinates, int[,] grid)
    {
        foreach (var pair in coordinates)
        {
            if (IsHorizontal(pair))
            {
                AddHorizontalVents(grid, pair);
            }
            else if (IsVertical(pair))
            {
                AddVerticalVents(grid, pair);
            }
        }
    }
    
    private static void PlaceVentsWithDiagonals(List<(Coordinate Start, Coordinate End)> coordinates, int[,] grid)
    {
        foreach (var pair in coordinates)
        {
            if (IsHorizontal(pair))
            {
                AddHorizontalVents(grid, pair);
            }
            else if (IsVertical(pair))
            {
                AddVerticalVents(grid, pair);
            }
            else
            {
                AddDiagonalVents(grid, pair);
            }
        }
    }

    private static bool IsVertical((Coordinate start, Coordinate end) pair) => pair.start.Y == pair.end.Y;

    private static bool IsHorizontal((Coordinate start, Coordinate end) pair) => pair.start.X == pair.end.X;
    
    private static void AddDiagonalVents(int[,] grid, (Coordinate start, Coordinate end) pair)
    {
        var (start, end) = pair;
        
        var numVents = Math.Abs(start.X - end.X);

        if (start.X > end.X && start.Y > end.Y)
        {
            for (var i = 0; i <= numVents; i++)
            {
                grid[end.X + i, end.Y + i]++;
            }
        }
        else if (start.X > end.X && start.Y < end.Y)
        {
            for (var i = 0; i <= numVents; i++)
            {
                grid[end.X + i, end.Y - i]++;
            }
        }
        else if (start.X < end.X && start.Y > end.Y)
        {
            for (var i = 0; i <= numVents; i++)
            {
                grid[end.X - i, end.Y + i]++;
            }
        }
        else if (start.X < end.X && start.Y < end.Y)
        {
            for (var i = 0; i <= numVents; i++)
            {
                grid[end.X - i, end.Y -+ i]++;
            }
        }
    }
    
    private static void AddVerticalVents(int[,] grid, (Coordinate start, Coordinate end) pair)
    {
        var (start, end) = pair;

        if (start.X > end.X)
        {
            for (var i = end.X; i <= start.X; i++)
            {
                grid[i, start.Y]++;
            }
        }
        else
        {
            for (var i = start.X; i <= end.X; i++)
            {
                grid[i, start.Y]++;
            }
        }
    }

    private static void AddHorizontalVents(int[,] grid, (Coordinate start, Coordinate end) pair)
    {
        var (start, end) = pair;

        if (start.Y > end.Y)
        {
            for (var i = end.Y; i <= start.Y; i++)
            {
                grid[start.X, i]++;
            }
        }
        else
        {
            for (var i = start.Y; i <= end.Y; i++)
            {
                grid[start.X, i]++;
            }
        }
    }

    private static int[,] BuildGrid(IReadOnlyCollection<(Coordinate Start, Coordinate End)> coordinates)
    {
        var xCoords = coordinates.SelectMany(x => new[] { x.Start.X, x.End.X });
        var yCoords = coordinates.SelectMany(y => new[] { y.Start.Y, y.End.Y });

        return new int[xCoords.Max() + 1, yCoords.Max() + 1];
    }

    private static List<(Coordinate Start, Coordinate End)> BuildCoordinates(string[] input) =>
        input
            .Select(ParseInputLine)
            .Select(BuildCoordinatePair)
            .ToList();

    private static (Coordinate Start, Coordinate End) BuildCoordinatePair(int[][] pair)
    {
        var start = new Coordinate(pair[0][0], pair[0][1]);
        var end = new Coordinate(pair[1][0], pair[1][1]);
        
        return (Start: start, End: end);
    }

    private static int[][] ParseInputLine(string line) => 
        line.Split(" -> ")
            .Select(ParseCoordinate)
            .ToArray();

    private static int[] ParseCoordinate(string pair) =>
        pair.Split(",")
            .Select(y => Convert.ToInt32(y))
            .ToArray();

    private record Coordinate(int X, int Y);
}

