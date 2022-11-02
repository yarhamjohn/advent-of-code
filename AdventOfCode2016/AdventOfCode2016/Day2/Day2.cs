namespace AdventOfCode2016.Day2;

public static class Day2
{
    public static string GetBathroomCode(string[] input)
    {
        var code = new List<int>();
        var location = (x: 1, y: 1);
        foreach (var sequence in input)
        {
            foreach (var move in sequence)
            {
                location = move switch
                {
                    'U' when location.y > 0 => (location.x, location.y - 1),
                    'D' when location.y < 2 => (location.x, location.y + 1),
                    'L' when location.x > 0 => (location.x - 1, location.y),
                    'R' when location.x < 2 => (location.x + 1, location.y),
                    _ => location
                };
            }
            
            code.Add(GetDigit(location));
        }
        
        return string.Join("", code);
    }

    private static int GetDigit((int x, int y) location)
    {
        var (x, y) = location;
        return x switch
        {
            0 when y == 0 => 1,
            0 when y == 1 => 4,
            0 when y == 2 => 7,
            1 when y == 0 => 2,
            1 when y == 1 => 5,
            1 when y == 2 => 8,
            2 when y == 0 => 3,
            2 when y == 1 => 6,
            2 when y == 2 => 9,
            _ => throw new InvalidOperationException($"invalid location x: {x}, y: {y}")
        };
    }
}