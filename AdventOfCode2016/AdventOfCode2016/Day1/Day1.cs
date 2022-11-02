namespace AdventOfCode2016.Day1;

public static class Day1
{
    public static int GetBlockCount(string input)
    {
        var chunks = input.Split(", ").Select(x => (direction: x[0], distance: Convert.ToInt32(x[1..])));

        var coordinate = (x: 0, y: 0);

        var currentDirection = Direction.North;
        foreach (var (direction, distance) in chunks)
        {
            var nextDirection = GetNextDirection(currentDirection, direction);

            coordinate = nextDirection switch
            {
                Direction.North => (coordinate.x, coordinate.y += distance),
                Direction.South => (coordinate.x, coordinate.y -= distance),
                Direction.East => (coordinate.x += distance, coordinate.y),
                Direction.West => (coordinate.x -= distance, coordinate.y),
                _ => throw new ArgumentOutOfRangeException(nameof(nextDirection), nextDirection, "Not a valid direction.")
            };

            currentDirection = nextDirection;
        }
        
        return Math.Abs(coordinate.x) + Math.Abs(coordinate.y);
    }

    private static Direction GetNextDirection(Direction currentDirection, char direction)
    {
        return currentDirection switch
        {
            Direction.North => direction == 'L' ? Direction.West : Direction.East,
            Direction.South => direction == 'L' ? Direction.East : Direction.West,
            Direction.West => direction == 'L' ? Direction.South : Direction.North,
            Direction.East => direction == 'L' ? Direction.North : Direction.South,
            _ => throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, "Not a valid direction.")
        };
    }

    private enum Direction
    {
        North,
        South,
        East,
        West
    };
}