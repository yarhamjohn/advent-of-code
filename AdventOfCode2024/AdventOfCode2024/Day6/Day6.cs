using System.Data;

namespace AdventOfCode2024.Day6;

public static class Day6
{
    public static int Part1(string[] input)
    {
        var position = GetPosition(input);

        var blockers = GetBlockers(input);

        List<(int row, int col)> visitedLocations = [(position.row, position.col)];
        
        while (true)
        {
            position = GetNextPosition(position, blockers);
            
            if (IsOffGrid(position, input))
            {
                break;
            }
            
            visitedLocations.Add((position.row, position.col));
        }
        
        return visitedLocations.Count;
    }
    
    private static (string direction, int row, int col) GetNextPosition(
        (string direction, int row, int col) position, 
        List<(int row, int col)> blockers)
    {
    }
    
    private static bool IsOffGrid((string direction, int row, int col) position, string[] input)
    {
        return position.row < 0 || 
               position.row >= input.Length || 
               position.col < 0 || 
               position.col >= input[position.row].Length;
    }
    
    private static List<(int row, int col)> GetBlockers(string[] input)
    {
        var blockers = new List<(int row, int col)>();
        
        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] == '#')
                {
                    blockers.Add((row, col));
                }
            }
        }

        return blockers;
    }
    
    private static (string direction, int row, int col) GetPosition(string[] input)
    {
        string[] options = ["^", ">", "v", "<"];

        for (var row = 0; row < input.Length; row++)
        {
            if (options.All(x => !input[row].Contains(x)))
            {
                continue;
            }

            var location = options
                .Select((x, i) => (direction: options[i], col: input[row].IndexOf(x, StringComparison.Ordinal)))
                .MaxBy(x => x.Item2);
                
            return (location.direction, row, location.col);
        }
        
        throw new InvalidOperationException("No starting position found");
    }
}