using System.Runtime.InteropServices;

namespace AdventOfCode2016.Day13;

public static class Day13
{
    public static long CountSteps((int col, int row) targetCoordinate, int favouriteNumber)
    {
        // starting at (1, 1) get all surrounding spaces (not diagonal, or negative indexes).
        // move to each in turn and repeat until blocked.
        // continue until reaching the targetCoordinate
        // stop searching each if no more options or if every option takes more steps than already taken
        
        var currentCoordinate = (col: 1, row: 1);
        
        Iterate(targetCoordinate, currentCoordinate, Array.Empty<(int, int)>(), favouriteNumber, 0);

        return _stepsTaken;
    }

    public static long CountLocations(int favouriteNumber)
    {
        // starting at (1, 1) get all surrounding spaces (not diagonal, or negative indexes).
        // move to each in turn and repeat until blocked.
        // continue until reaching the targetCoordinate
        // stop searching each if no more options or if every option takes more steps than already taken
        
        var currentCoordinate = (col: 1, row: 1);

        var stepsTaken = 0;
        while (stepsTaken < 50)
        {
            
            stepsTaken++;
        }
        
        Iterate2(currentCoordinate, Array.Empty<(int, int)>(), favouriteNumber, 0);

        return LocationsVisited.Count;
    }
    
    private static int _stepsTaken = int.MaxValue;
    private static readonly List<(int, int)> LocationsVisited = new ();
    
    private static void Iterate2((int col, int row) currentCoordinate, (int row, int col)[] previousCoordinates, int favouriteNumber, int stepsTaken)
    {
        if (!LocationsVisited.Contains(currentCoordinate))
        {
            LocationsVisited.Add(currentCoordinate);
        }

        // Get next positions but exclude where we just came from and invalid positions
        var nextCoordinates = GetNextCoordinates(currentCoordinate, previousCoordinates, favouriteNumber);

        stepsTaken++;

        // Reached max steps allowed
        if (stepsTaken > 50)
        {
            return;
        }
        
        foreach (var coord in nextCoordinates)
        {
            Iterate2(coord, previousCoordinates.Concat(new[] {currentCoordinate}).ToArray(), favouriteNumber, stepsTaken);
        }
    }

    private static void Iterate((int col, int row) targetCoordinate, (int col, int row) currentCoordinate, (int row, int col)[] previousCoordinates, int favouriteNumber, int stepsTaken)
    {
        // Reached target and stepsTaken is lowest yet seen so finish
        if (currentCoordinate.row == targetCoordinate.row && currentCoordinate.col == targetCoordinate.col)
        {
            _stepsTaken = stepsTaken;
        }
        
        // Get next positions but exclude where we just came from and invalid positions
        var nextCoordinates = GetNextCoordinates(currentCoordinate, previousCoordinates, favouriteNumber);

        stepsTaken++;

        if (stepsTaken >= _stepsTaken)
        {
            // Too many steps have been taken already
            return;
        }

        foreach (var coord in nextCoordinates)
        {
            Iterate(targetCoordinate, coord, previousCoordinates.Concat(new[] {currentCoordinate}).ToArray(), favouriteNumber, stepsTaken);
        }
    }

    private static IEnumerable<(int, int)> GetNextCoordinates((int col, int row) currentCoordinate, (int row, int col)[] previousCoordinates, int favouriteNumber)
    {
        return new[]
            {
                currentCoordinate with { col = currentCoordinate.col - 1 },
                currentCoordinate with { col = currentCoordinate.col + 1 },
                currentCoordinate with { row = currentCoordinate.row - 1 },
                currentCoordinate with { row = currentCoordinate.row + 1 }
            }
            .Except(previousCoordinates) // Exclude where we came from
            .Where(x => x is { Item1: >= 0, Item2: >= 0 }) // Exclude positions off the grid
            .Where(y => IsOpenSpace(y, favouriteNumber)); // Exclude positions known to be walls
    }

    private static bool IsOpenSpace((int col, int row) coordinate, int favouriteNumber)
    {
        var formula = coordinate.col * coordinate.col 
                      + 3 * coordinate.col 
                      + 2 * coordinate.col * coordinate.row
                      + coordinate.row
                      + coordinate.row * coordinate.row
                      + favouriteNumber;

        return Convert.ToString(formula, 2).Count(x => x == '1') % 2 == 0;
    }
}