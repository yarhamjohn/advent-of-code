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

    private static Place[][] GetNextGrid(
        IReadOnlyCollection<Place[]> grid,
        (int mins, int x, int y) currentLocation,
        IReadOnlyDictionary<int, Place[][]> blizzardPositions)
    {
        var repeatTime = (grid.Count - 2) * (grid.First().Length - 2);
        var currentLocationMinutes = currentLocation.mins < repeatTime
            ? currentLocation.mins + 1
            : currentLocation.mins % repeatTime + 1;

        return blizzardPositions[currentLocationMinutes];
    }

    private static Dictionary<int, Place[][]> ComputeAllBlizzards(Place[][] grid)
    {
        var result = new Dictionary<int, Place[][]> { { 0, CloneGrid(grid)}};
        
        var workingGrid = CloneGrid(grid);
        var repeatSize = (grid.Length - 2) * (grid.First().Length - 2);
        for (var i = 1; i <= repeatSize; i++)
        {
            workingGrid = MoveBlizzards(workingGrid);
            result.Add(i, CloneGrid(workingGrid));
        }

        return result;
    }

    private static Place[][] CloneGrid(IEnumerable<Place[]> grid)
        => grid.Select(x => x.Select(y => y).ToArray()).ToArray();

    private static bool WouldBeRepeatVisit(
        (int mins, int x, int y) nextLocation,
        Place[][] grid,
        ICollection<(int mins, int x, int y)> history)
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

    private static List<(int x, int y)> GetPossibleLocations(IReadOnlyList<Place[]> grid, int x, int y)
    {
        var result = new List<(int x, int y)>();
        
        if (!grid[x][y].Blizzards.Any())
        {
            result.Add((x, y));
        }
        
        if (x == 0)
        {
            if (!grid[1][y].Blizzards.Any())
            {
                result.Add((1, y));
            }

            return result;
        }

        if (IsEmpty(grid[x - 1][y]))
        {
            result.Add((x - 1, y));
        }
        
        if (IsEmpty(grid[x + 1][y]))
        {
            result.Add((x + 1, y));
        }
        
        if (IsEmpty(grid[x][y - 1]))
        {
            result.Add((x, y - 1));
        }
        
        if (IsEmpty(grid[x][y + 1]))
        {
            result.Add((x, y + 1));
        }

        return result;
    }

    private static bool IsEmpty(Place target) => !target.IsWall() && !target.Blizzards.Any();

    private static Place[][] MoveBlizzards(Place[][] grid)
    {
        var nextGrid = grid.Select(x =>
                x.Select(y => y.IsWall() ? y : new Place())
                    .ToArray())
            .ToArray();
        
        for (var row = 1; row < grid.Length - 1; row++)
        {
            for (var col = 1; col < grid[row].Length - 1; col++)
            {
                var blizzards = grid[row][col].Blizzards;

                foreach (var blizzard in blizzards)
                {
                    var (x, y) = GetNextPosition(blizzard, row, col, grid);
                    nextGrid[x][y].AddBlizzard(blizzard);
                }
            }
        }

        return nextGrid;
    }

    private static (int x, int y) GetNextPosition(char blizzard, int row, int col, Place[][] grid)
        => blizzard switch
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

    private static void PrintGrid(IReadOnlyList<Place[]> grid, (int x, int y) location)
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

    private static Place[][] Parse(string[] input)
        => Enumerable.Range(0, input.Length).Select(x => 
                Enumerable.Range(0, input.First().Length)
                    .Select(y => BuildPlace(input[x][y]))
                    .ToArray())
            .ToArray();

    private static Place BuildPlace(char input)
    {
        if (input == '#')
        {
            return new Place(true);
        }

        var place = new Place();
        if (input != '.')
        {
            place.AddBlizzard(input);
        }
            
        return place;
    }

    private class Place
    {
        private readonly bool _isWall;

        public List<char> Blizzards { get; } = new();

        public Place(bool isWall = false)
        {
            _isWall = isWall;
        }

        public void AddBlizzard(char direction) => Blizzards.Add(direction);

        public override string ToString()
        {
            if (_isWall)
            {
                return "#";
            }

            return Blizzards.Count switch
            {
                0 => ".",
                1 => Blizzards.Single().ToString(),
                _ => Blizzards.Count.ToString()
            };
        }

        public bool IsWall() => _isWall;
    }
}