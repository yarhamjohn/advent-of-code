namespace AdventOfCode2022.Day23;

public static class Day23
{
    public static long CountEmptyGroundTiles(string[] input)
    {
        var grid = ParseInput(input);

        PrintGrid(grid);

        var rounds = 0;
        while (rounds < 10)
        {
            var proposedPositions = GetProposedPositions(grid);

            grid = UpdateGrid(proposedPositions, grid);
            
            rounds++;
        }
        
        return CountEmptySpaces(grid);
    }

    private static Dictionary<(int x, int y), (int x, int y)> GetProposedPositions(ITile[][] grid)
    {
        var proposedPositions = new Dictionary<(int x, int y), (int x, int y)>();
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                if (grid[row][col] is Elf)
                {
                    var position = GetProposedPosition(grid, row, col);
                    if (position is not null)
                    {
                        proposedPositions[(row, col)] = (position.Value.x, position.Value.y);
                    }

                    ((Elf)grid[row][col]).Reorganise();
                }
            }
        }

        return RemoveDuplicates(proposedPositions);
    }

    private static ITile[][] UpdateGrid(Dictionary<(int x, int y), (int x, int y)> proposedPositions, ITile[][] grid)
    {
        //TODO: Extend grid if any proposed position is off grid
        
        foreach (var (key, value) in proposedPositions)
        {
            grid[value.x][value.y] = grid[key.x][key.y];
            grid[key.x][key.y] = new Space();
        }
    }

    private static Dictionary<(int x, int y),(int x, int y)> RemoveDuplicates(Dictionary<(int x, int y),(int x, int y)> proposedPositions)
    {
        return proposedPositions
            .GroupBy(x => x.Value)
            .Where(y => y.Count() == 1)
            .SelectMany(z => z)
            .ToDictionary(a => a.Key, a => a.Value);
    }

    private static (int x, int y)? GetProposedPosition(ITile[][] grid, int row, int col)
    {
        var directions = ((Elf)grid[row][col]).GetOrder();

        if (AllNeighboursAreEmpty(grid, row, col))
        {
            return null;
        }

        while (directions.Count > 0)
        {
            //TODO: Handle off grid
            var direction = directions.Dequeue();
            if (direction == "N" && grid[row - 1][col - 1] is Space && grid[row - 1][col] is Space && grid[row - 1][col + 1] is Space)
            {
                return (row - 1, col);
            }
            
            if (direction == "S" && grid[row + 1][col - 1] is Space && grid[row + 1][col] is Space && grid[row + 1][col + 1] is Space)
            {
                return (row + 1, col);
            }
            
            if (direction == "W" && grid[row - 1][col - 1] is Space && grid[row][col - 1] is Space && grid[row + 1][col - 1] is Space)
            {
                return (row, col - 1);
            }
            
            if (direction == "E" && grid[row - 1][col + 1] is Space && grid[row][col + 1] is Space && grid[row + 1][col + 1] is Space)
            {
                return (row, col + 1);
            }
        }

        return null;
    }

    private static int CountEmptySpaces(ITile[][] grid)
    {
        return grid.SelectMany(x => x).Count(y => y is Space);
    }

    private static void PrintGrid(ITile[][] grid)
    {
        foreach (var row in grid)
        {
            foreach (var col in row)
            {
                Console.Write(col is Elf ? "#" : ".");
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine();
    }

    private static ITile[][] ParseInput(string[] input)
    {
        return input.Select(row => row.Select(col => col == '#' ? (ITile) new Elf() : new Space()).ToArray()).ToArray();
    }

    public interface ITile
    {
        
    }

    public class Elf : ITile
    {
        private Queue<string> directions { get; }

        public Elf()
        {
            directions = new Queue<string>();
            directions.Enqueue("N");
            directions.Enqueue("S");
            directions.Enqueue("W");
            directions.Enqueue("E");
        }

        public void Reorganise()
        {
            var dir = directions.Dequeue();
            directions.Enqueue(dir);
        }

        public Queue<string> GetOrder()
        {
            return directions;
        }
    }

    public class Space : ITile
    {
        
    }
}