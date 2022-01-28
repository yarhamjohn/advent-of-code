namespace AdventOfCode.Day17;

public static class Day17
{
    public static long CalculateMaximumY(string input)
    {
        var targetCoordinates = ParseInput(input).ToArray();

        var maxYVelocity = GetMaxYVelocity(targetCoordinates);

        return Enumerable.Range(0, maxYVelocity + 1).Sum();
    }

    private static int GetMaxYVelocity(Coordinate[] targetCoordinates)
    {
        return targetCoordinates.Select(x => x.Y).Min() * -1 - 1;
    }

    public static long CalculateAllVelocities(string input)
    {
        var targetCoordinates = ParseInput(input).ToArray();
        
        var possibleXVelocities = GetPossibleXVelocities(targetCoordinates).Distinct();
        
        var velocities = CalculateVelocities(possibleXVelocities, targetCoordinates);

        return velocities.Distinct().ToArray().Length;
    }

    private static List<(int xV, int yV)> CalculateVelocities(IEnumerable<int> possibleXVelocities, Coordinate[] targetCoordinates)
    {
        var minXPosition = targetCoordinates.Min(x => x.X);
        var maxXPosition = targetCoordinates.Max(x => x.X);
        var maxYPosition = targetCoordinates.Min(x => x.Y);
        var minYPosition = targetCoordinates.Max(x => x.Y);
        
        var velocities = new List<(int xV, int yV)>();
        foreach (var xV in possibleXVelocities)
        {
            for (var yV = maxYPosition; yV <= GetMaxYVelocity(targetCoordinates); yV++)
            {
                var currentPosition = new Coordinate(0, 0);

                var currentXVelocity = xV;
                var currentYVelocity = yV;
                while (currentPosition.X <= maxXPosition && currentPosition.Y >= maxYPosition)
                {
                    var newXPosition = currentPosition.X + currentXVelocity;
                    var newYPosition = currentPosition.Y + currentYVelocity;
                    currentPosition = new Coordinate(newXPosition, newYPosition);

                    var inTarget = currentPosition.X >= minXPosition && currentPosition.X <= maxXPosition &&
                                   currentPosition.Y <= minYPosition && currentPosition.Y >= maxYPosition;

                    if (inTarget)
                    {
                        velocities.Add((xV, yV));
                        break;
                    }

                    currentXVelocity = currentXVelocity > 0 ? currentXVelocity - 1 : 0;
                    currentYVelocity--;
                }
            }
        }

        return velocities;
    }

    private static List<int> GetPossibleXVelocities(Coordinate[] targetCoordinates)
    {
        var minXTarget = targetCoordinates.Min(x => x.X);
        var maxXTarget = targetCoordinates.Max(x => x.X);
        
        var velocities = new List<int>();
        for (var velocity = 1; velocity <= maxXTarget; velocity++)
        {
            var currentXPosition = 0;
            var currentXVelocity = velocity;

            while (currentXPosition <= maxXTarget)
            {
                currentXPosition += currentXVelocity;
                currentXVelocity--;

                var withinTargetXRange = currentXPosition >= minXTarget && currentXPosition <= maxXTarget;
                if (withinTargetXRange)
                {
                    velocities.Add(velocity);
                }

                if (currentXVelocity == 0)
                {
                    break;
                }
            }
        }

        return velocities;
    }

    private static IEnumerable<Coordinate> ParseInput(string input)
    {
        var xAndY = input.Split(": ")[1].Split(", ");
        var xRange = xAndY[0].Split("=")[1].Split("..").Select(x => Convert.ToInt32(x)).ToArray();
        var yRange = xAndY[1].Split("=")[1].Split("..").Select(x => Convert.ToInt32(x)).ToArray();

        for (var x = xRange[0]; x <= xRange[1]; x++)
        {
            for (var y = yRange[0]; y <= yRange[1]; y++)
            {
                yield return new(x, y);
            }
        }
    }

    private record Coordinate(int X, int Y);
}