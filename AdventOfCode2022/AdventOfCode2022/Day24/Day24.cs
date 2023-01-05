namespace AdventOfCode2022.Day24;

public static class Day24
{
    public static long CountMinutesTaken(string[] input)
    {
        Mins = int.MaxValue;
        
        var grid = Parse(input);

        var location = (x: 0, y: 1);

        PrintGrid(grid, location);
        
        Thing(grid, location, 0);

        return Mins;
    }

    public static int Mins;

    private static void Thing(IThing[][] grid, (int x, int y) location, int minsElapsed)
    {
        // Console.WriteLine($"Minute {minsElapsed}");
        // PrintGrid(grid, location);
        
        if (minsElapsed >= Mins)
        {
            Console.WriteLine($"Minute {minsElapsed}");
            PrintGrid(grid, location);
            
            return;
        }

        if (((Place)grid[location.x][location.y]).TimesVisited >= grid.First().Length)
        {
            Console.WriteLine($"Minute {minsElapsed}");
            PrintGrid(grid, location);
            
            return;
        }
        
        if (location.x == grid.Length - 1)
        {
            Console.WriteLine($"Minute {minsElapsed}");
            PrintGrid(grid, location);
            
            if (minsElapsed < Mins)
            {
                Mins = minsElapsed;
            }
            
            return;
        }
        
        grid = MoveBlizzards(grid);
        
        var possibleLocations = GetPossibleLocations(grid, location);

        foreach (var loc in possibleLocations)
        {
            Thing(grid, loc, minsElapsed + 1);
        }
    }

    private static List<(int x, int y)> GetPossibleLocations(IThing[][] grid, (int x, int y) location)
    {
        ((Place)grid[location.x][location.y]).TimesVisited++;
        
        var result = new List<(int x, int y)>();

        if (location.x != 0 && !((Place)grid[location.x][location.y]).Blizzards.Any())
        {
            result.Add(location);
        }

        if (location.x == 0 && ((Place)grid[location.x + 1][location.y]).Blizzards.Any())
        {
            result.Add(location);
        }

        if (location.x > 1 && grid[location.x - 1][location.y] is Place up && !up.Blizzards.Any())
        {
            result.Add((location.x - 1, location.y));
        }
        
        if (grid[location.x + 1][location.y] is Place down && !down.Blizzards.Any())
        {
            result.Add((location.x + 1, location.y));
        }
        
        if (grid[location.x][location.y - 1] is Place left && !left.Blizzards.Any())
        {
            result.Add((location.x, location.y - 1));
        }
        
        if (grid[location.x][location.y + 1] is Place right && !right.Blizzards.Any())
        {
            result.Add((location.x, location.y + 1));
        }

        return result;
    }

    private static IThing[][] MoveBlizzards(IThing[][] grid)
    {
        var nextGrid = grid.Select(x => x.Select(y =>
        {
            if (y is Wall)
            {
                return y;
            }
            
            var newPlace = new Place();
            newPlace.TimesVisited = ((Place)y).TimesVisited;

            return newPlace;
        }).ToArray()).ToArray();
        
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

    private static void PrintGrid(IThing[][] grid, (int x, int y) location)
    {
        for (var row = 0; row < grid.Length; row++)
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

    public interface IThing
    {
        string ToString();
    }

    public class Place : IThing
    {
        public List<char> Blizzards { get; }
        
        public int MinsTaken { get; set; }
        
        public int TimesVisited { get; set; }

        public Place()
        {
            Blizzards = new List<char>();
            MinsTaken = 0;
        }
        
        public void AddBlizzard(char direction)
        {
            Blizzards.Add(direction);
        }

        public void RemoveBlizzard(char direction)
        {
            Blizzards.Remove(direction);
        }

        public override string ToString()
        {
            if (!Blizzards.Any())
            {
                return ".";
            }

            if (Blizzards.Count == 1)
            {
                return Blizzards.Single().ToString();
            }

            return Blizzards.Count.ToString();
        }
    }
    
    public class Wall : IThing
    {
        public override string ToString()
        {
            return "#";
        }
    }
}