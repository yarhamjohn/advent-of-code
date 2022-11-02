namespace AdventOfCode2016.Day1;

public static class Day1
{
    public static int GetBlockCountToDoubleVisitedBlock(string input)
    {
        var chunks = input.Split(", ").Select(x => (direction: x[0], distance: Convert.ToInt32(x[1..])));

        var coordinateStore = new List<(int x, int y)>();

        var currentCoordinate = (x: 0, y: 0);
        var currentDirection = Direction.North;
        foreach (var (direction, distance) in chunks)
        {
            var nextDirection = GetNextDirection(currentDirection, direction);

            for (var i = 0; i < distance; i++)
            {
                switch (nextDirection)
                {
                    case Direction.North:
                        coordinateStore.Add((currentCoordinate.x, y: currentCoordinate.y += 1));
                        break;
                    case Direction.South:
                        coordinateStore.Add((currentCoordinate.x, y: currentCoordinate.y -= 1));
                        break;
                    case Direction.East:
                        coordinateStore.Add((x: currentCoordinate.x += 1, currentCoordinate.y));
                        break;
                    case Direction.West:
                        coordinateStore.Add((x: currentCoordinate.x -= 1, currentCoordinate.y));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(nextDirection), nextDirection, "Not a valid direction.");
                }
                
                if (coordinateStore.Distinct().Count() != coordinateStore.Count)
                {
                    return Math.Abs(coordinateStore.Last().x) + Math.Abs(coordinateStore.Last().y);
                }
            }

            currentCoordinate = coordinateStore.Last();
            currentDirection = nextDirection;
        }

        throw new InvalidOperationException("Something went wrong - no double visit was seen.");
    }
    
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