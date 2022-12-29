namespace AdventOfCode2022.Day23;

public static class Day23
{
    public static long CountEmptyGroundTiles(string[] input)
    {
        var grid = ParseInput(input);

        var rounds = 0;
        while (rounds < 10)
        {
            var proposedPositions = GetProposedPositions(grid);

            UpdateGrid(proposedPositions, grid);
            
            rounds++;
        }
        
        return CountEmptySpaces(grid);
    }
    
    public static long CountRoundsRequired(string[] input)
    {
        var grid = ParseInput(input);

        var rounds = 0;
        while (true)
        {
            var proposedPositions = GetProposedPositions(grid);

            if (!proposedPositions.Any())
            {
                break;
            }
            
            UpdateGrid(proposedPositions, grid);
            
            rounds++;
        }
        
        return rounds + 1;
    }

    private static Dictionary<(int x, int y), (int x, int y)> GetProposedPositions(ITile[][] grid)
    {
        var proposedPositions = new Dictionary<(int x, int y), (int x, int y)>();
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                if (grid[row][col] is Elf elf)
                {
                    var position = GetProposedPosition(grid, row, col);
                    if (position is not null)
                    {
                        proposedPositions[(row, col)] = (position.Value.x, position.Value.y);
                    }

                    elf.Reorganise();
                }
            }
        }

        return RemoveDuplicates(proposedPositions);
    }

    private static void UpdateGrid(Dictionary<(int x, int y), (int x, int y)> proposedPositions, ITile[][] grid)
    {
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

        foreach (var dir in directions)
        {
            switch (dir)
            {
                case "N" when NorthIsSpace(grid, row, col):
                    return (row - 1, col);
                case "S" when SouthIsSpace(grid, row, col):
                    return (row + 1, col);
                case "W" when WestIsSpace(grid, row, col):
                    return (row, col - 1);
                case "E" when EastIsSpace(grid, row, col):
                    return (row, col + 1);
            }
        }

        return null;
    }

    private static bool EastIsSpace(ITile[][] grid, int row, int col)
    {
        return IsSpace(grid, row - 1, col + 1) && IsSpace(grid, row, col + 1) && IsSpace(grid, row + 1, col + 1);
    }

    private static bool WestIsSpace(ITile[][] grid, int row, int col)
    {
        return IsSpace(grid, row - 1, col - 1) && IsSpace(grid, row, col - 1) && IsSpace(grid, row + 1, col - 1);
    }

    private static bool SouthIsSpace(ITile[][] grid, int row, int col)
    {
        return IsSpace(grid, row + 1, col - 1) && IsSpace(grid, row + 1, col) && IsSpace(grid, row + 1, col + 1);
    }

    private static bool NorthIsSpace(ITile[][] grid, int row, int col)
    {
        return IsSpace(grid, row - 1, col - 1) && IsSpace(grid, row - 1, col) && IsSpace(grid, row - 1, col + 1);
    }

    private static bool IsSpace(ITile[][] grid, int row, int col)
    {
        if (row < 0 || row >= grid.Length || col < 0 || col >= grid[row].Length)
        {
            return true;
        }

        return grid[row][col] is Space;
    }

    private static bool AllNeighboursAreEmpty(ITile[][] grid, int row, int col)
    {
        return NorthIsSpace(grid, row, col) &&
               SouthIsSpace(grid, row, col) &&
               WestIsSpace(grid, row, col) &&
               EastIsSpace(grid, row, col);
    }

    private static int CountEmptySpaces(ITile[][] grid)
    {
        var topRow = (false, 0);
        var bottomRow = 0;
        var leftCol = (false, 0);
        var rightCol = 0;
        
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                if (grid[row][col] is Elf)
                {
                    if (!topRow.Item1)
                    {
                        topRow = (true, row);
                    }
                    else
                    {
                        bottomRow = row;
                    }

                    if (!leftCol.Item1)
                    {
                        leftCol = (true, col);
                    }
                    else if (col < leftCol.Item2)
                    {
                        leftCol = (true, col);
                    }
                    else if (col > rightCol)
                    {
                        rightCol = col;
                    }
                }
            }
        }
        
        return grid.Where((_, i) => i >= topRow.Item2 && i <= bottomRow).Select(x => x.Where((_, i) => i >= leftCol.Item2 && i <= rightCol)).SelectMany(x => x).Count(y => y is Space);
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
        var height = input.Length + 100;
        var width = input.First().Length + 100;
        
        var grid = Enumerable.Range(0, height).Select(_ => Enumerable.Range(0, width).Select(_ => (ITile)new Space()).ToArray()).ToArray();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] == '#')
                {
                    grid[row + 50][col + 50] = new Elf();
                }
            }
        }

        return grid;
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

        public string[] GetOrder()
        {
            return directions.ToArray();
        }
    }

    public class Space : ITile
    {
        
    }
}