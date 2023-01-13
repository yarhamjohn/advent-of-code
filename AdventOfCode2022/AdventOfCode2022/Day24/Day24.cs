namespace AdventOfCode2022.Day24;

public static class Day24
{
    public static long CountMinutesTaken(string[] input)
    {
        var grid = Parse(input);

        var queue = new Queue<(int mins, int x, int y)>();
        queue.Enqueue((mins: 0, x: 0, y: 1));

        var history = new List<(int mins, int x, int y)>();

        var blizzardPositions = ComputeAllBlizzards(grid);
        
        while (queue.Any())
        {
            var currentLocation = queue.Dequeue();
            history.Add(currentLocation);

            if (currentLocation.x == grid.Length - 1)
            {
                return currentLocation.mins;
            }

            var workingGrid = GetNextGrid(grid, currentLocation, blizzardPositions);

            foreach (var loc in GetPossibleLocations(workingGrid, currentLocation.x, currentLocation.y))
            {
                var nextLocation = (mins: currentLocation.mins + 1, loc.x, loc.y);
                
                if (loc.x == grid.Length - 1)
                {
                    queue.Clear();
                    queue.Enqueue(nextLocation);
                    break;
                }

                if (!queue.Contains(nextLocation) && !WouldBeRepeatVisit(nextLocation, grid, history))
                {
                    queue.Enqueue(nextLocation);
                }
            }
        }

        return 0;
    }

    private static IThing[][] GetNextGrid(
        IReadOnlyCollection<IThing[]> grid,
        (int mins, int x, int y) currentLocation,
        IReadOnlyDictionary<int, IThing[][]> blizzardPositions)
    {
        var repeatTime = (grid.Count - 2) * (grid.First().Length - 2);
        var currentLocationMinutes = currentLocation.mins < repeatTime
            ? currentLocation.mins + 1
            : currentLocation.mins % repeatTime + 1;

        return blizzardPositions[currentLocationMinutes];
    }

    private static Dictionary<int, IThing[][]> ComputeAllBlizzards(IThing[][] grid)
    {
        var result = new Dictionary<int, IThing[][]> { { 0, CloneGrid(grid)}};
        
        var workingGrid = CloneGrid(grid);
        var repeatSize = (grid.Length - 2) * (grid.First().Length - 2);
        for (var i = 1; i <= repeatSize; i++)
        {
            workingGrid = MoveBlizzards(workingGrid);
            result.Add(i, CloneGrid(workingGrid));
        }

        return result;
    }

    private static IThing[][] CloneGrid(IThing[][] grid)
    {
        return grid.Select(x => x.Select(y => y).ToArray()).ToArray();
    }

    private static bool WouldBeRepeatVisit((int mins, int x, int y) nextLocation, IThing[][] grid, List<(int mins, int x, int y)> history)
    {
        var remainder = nextLocation.mins % (grid.First().Length - 2);
        for (var i = remainder; i <= nextLocation.mins; i += grid.First().Length - 2)
        {
            if (history.Contains((i, nextLocation.x, nextLocation.y)))
            {
                return true;
            }
        }

        return false;
    }

    private static List<(int x, int y)> GetPossibleLocations(IThing[][] grid, int x, int y)
    {
        var result = new List<(int x, int y)>();
        
        if (!((Place)grid[x][y]).Blizzards.Any())
        {
            result.Add((x, y));
        }
        
        if (x == 0)
        {
            if (!((Place)grid[1][y]).Blizzards.Any())
            {
                result.Add((1, y));
            }

            return result;
        }

        if (grid[x - 1][y] is Place up && !up.Blizzards.Any())
        {
            result.Add((x - 1, y));
        }
        
        if (grid[x + 1][y] is Place down && !down.Blizzards.Any())
        {
            result.Add((x + 1, y));
        }
        
        if (grid[x][y - 1] is Place left && !left.Blizzards.Any())
        {
            result.Add((x, y - 1));
        }
        
        if (grid[x][y + 1] is Place right && !right.Blizzards.Any())
        {
            result.Add((x, y + 1));
        }

        return result;
    }

    private static IThing[][] MoveBlizzards(IThing[][] grid)
    {
        var nextGrid = grid.Select(x =>
                x.Select(y => y is Wall
                        ? y
                        : new Place { TimesVisited = ((Place)y).TimesVisited })
                    .ToArray())
            .ToArray();
        
        for (var row = 1; row < grid.Length - 1; row++)
        {
            for (var col = 1; col < grid[row].Length - 1; col++)
            {
                var blizzards = ((Place)grid[row][col]).Blizzards;

                foreach (var blizzard in blizzards)
                {
                    var (x, y) = GetNextPosition(blizzard, row, col, grid);
                    ((Place)nextGrid[x][y]).AddBlizzard(blizzard);
                }
            }
        }

        return nextGrid;
    }

    private static (int x, int y) GetNextPosition(char blizzard, int row, int col, IThing[][] grid)
    {
        return blizzard switch
        {
            '^' when row == 1 => (grid.Length - 2, col),
            '^' => (row - 1, col),
            'v' when row == grid.Length - 2 => (1, col),
            'v' => (row + 1, col),
            '>' when col == grid.First().Length - 2 => (row, 1),
            '>' => (row, col + 1),
            '<' when col == 1 => (row, grid.First().Length - 2),
            '<' => (row, col - 1),
            _ => throw new Exception($"Not a valid blizzard: {blizzard}")
        };
    }

    private static void PrintGrid(IReadOnlyList<IThing[]> grid, (int x, int y) location)
    {
        for (var row = 0; row < grid.Count; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                if (row == location.x && col == location.y)
                {
                    Console.Write("E");
                }
                else
                {
                    Console.Write(grid[row][col]);
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static IThing[][] Parse(string[] input)
    {
        var grid = Enumerable.Range(0, input.Length)
            .Select(_ => Enumerable.Range(0, input.First().Length).Select(_ => (IThing) new Place()).ToArray()).ToArray();
        
        for (var x = 0; x < input.Length; x++)
        {
            for (var y = 0; y < input[x].Length; y++)
            {
                if (input[x][y] == '#')
                {
                    grid[x][y] = new Wall();
                } 
                else if (input[x][y] != '.')
                {
                    ((Place)grid[x][y]).AddBlizzard(input[x][y]);
                }
            }
        }

        return grid;
    }

    private interface IThing { }

    private class Place : IThing
    {
        public List<char> Blizzards { get; } = new();

        public int TimesVisited { get; init; }

        public void AddBlizzard(char direction) => Blizzards.Add(direction);

        public override string ToString()
            => Blizzards.Count switch
            {
                0 => ".",
                1 => Blizzards.Single().ToString(),
                _ => Blizzards.Count.ToString()
            };
    }

    private class Wall : IThing
    {
        public override string ToString() => "#";
    }
}