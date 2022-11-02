namespace AdventOfCode2016.Day2;

public static class Day2
{
    public static string GetExtendedBathroomCode(string[] input)
    {
        var code = new List<string>();
        var location = (x: 2, y: 0);
        
        foreach (var sequence in input)
        {
            foreach (var move in sequence)
            {
                location = move switch
                {
                    'U' when CanMoveUp(location) => (location.x - 1, location.y),
                    'D' when CanMoveDown(location) => (location.x + 1, location.y),
                    'L' when CanMoveLeft(location) => (location.x, location.y - 1),
                    'R' when CanMoveRight(location) => (location.x, location.y + 1),
                    _ => location
                };
            }
            
            code.Add(GetExtendedDigit(location));
        }
        
        return string.Join("", code);
    }

    private static bool CanMoveUp((int x, int y) location)
    {
        return location.x == 1 && location.y == 2
               || location.x == 2 && location.y is > 0 and < 4
               || location.x > 2;
    }
    
    private static bool CanMoveDown((int x, int y) location)
    {
        return location.x == 3 && location.y == 2
               || location.x == 2 && location.y is > 0 and < 4
               || location.x < 2;
    }

    private static bool CanMoveRight((int x, int y) location)
    {
        return location.y == 3 && location.x == 2 
               || location.y == 2 && location.x is > 0 and < 4 
               || location.y < 2;
    }

    private static bool CanMoveLeft((int x, int y) location)
    {
        return location.y == 1 && location.x == 2 
               || location.y == 2 && location.x is > 0 and < 4 
               || location.y > 2;
    }

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
                    'U' when location.x > 0 => (location.x - 1, location.y),
                    'D' when location.x < 2 => (location.x + 1, location.y),
                    'L' when location.y > 0 => (location.x, location.y - 1),
                    'R' when location.y < 2 => (location.x, location.y + 1),
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
    
    private static string GetExtendedDigit((int x, int y) location)
    {
        var (x, y) = location;
        return x switch
        {
            0 when y == 2 => "1",
            1 when y == 1 => "2",
            1 when y == 2 => "3",
            1 when y == 3 => "4",
            2 when y == 0 => "5",
            2 when y == 1 => "6",
            2 when y == 2 => "7",
            2 when y == 3 => "8",
            2 when y == 4 => "9",
            3 when y == 1 => "A",
            3 when y == 2 => "B",
            3 when y == 3 => "C",
            4 when y == 2 => "D",
            _ => throw new InvalidOperationException($"invalid location x: {x}, y: {y}")
        };
    }
}