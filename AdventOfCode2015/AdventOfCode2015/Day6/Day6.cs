namespace AdventOfCode2015.Day6;

public static class Day6
{
    public static long GetNumLitLights(string[] input)
    {
        var grid = new bool[1000, 1000];
        foreach (var line in input)
        {
            UpdateGrid(line, grid);
        }

        return grid.Cast<bool>().Count(x => x);
    }

    public static long GetTotalBrightness(string[] input)
    {
        var grid = new int[1000, 1000];
        foreach (var line in input)
        {
            UpdateGrid(line, grid);
        }
        
        return grid.Cast<int>().Sum();
    }

    private static void UpdateGrid(string line, int[,] grid)
    {
        var (instruction, start, end) = ParseLine(line);

        for (var x = start.X; x <= end.X; x++)
        {
            for (var y = start.Y; y <= end.Y; y++)
            {
                grid[x, y] = instruction switch
                {
                    Instruction.Toggle => grid[x,y] += 2,
                    Instruction.TurnOn => grid[x,y] += 1,
                    _ => grid[x, y] = grid[x,y] > 0 ? grid[x,y] - 1 : grid[x,y]
                };
            }
        }
    }

    private static void UpdateGrid(string line, bool[,] grid)
    {
        var (instruction, start, end) = ParseLine(line);

        for (var x = start.X; x <= end.X; x++)
        {
            for (var y = start.Y; y <= end.Y; y++)
            {
                grid[x, y] = instruction switch
                {
                    Instruction.Toggle => !grid[x, y],
                    Instruction.TurnOn => true,
                    _ => false
                };
            }
        }
    }

    private static (Instruction instruction, Coordinate start, Coordinate end) ParseLine(string line)
    {
        var chunks = line.Split(" ");
        if (chunks[0] == "toggle")
        {
            var start = chunks[1].Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            var end = chunks[3].Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            return (Instruction.Toggle, new (start[0], start[1]), new(end[0], end[1]));
        }
        else
        {
            var start = chunks[2].Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            var end = chunks[4].Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            var instruction = chunks[1] == "on" ? Instruction.TurnOn : Instruction.TurnOff;
            
            return (instruction, new (start[0], start[1]), new(end[0], end[1]));
        }
    }

    private enum Instruction
    {
        Toggle,
        TurnOn,
        TurnOff
    };

    private record Coordinate(int X, int Y);
}