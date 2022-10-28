using System.Reflection;

namespace AdventOfCode2015.Day25;

public static class Day25
{
    public static readonly long StartCode = 20151125;
    
    public static long CalculateTargetCoord(int row, int col)
    {
        var numSteps = CountNumSteps(row, col);

        return CalculateCode(numSteps);
    }

    private static long CalculateCode(int numSteps)
    {
        var runningTotal = StartCode;

        while (numSteps > 1)
        {
            runningTotal = runningTotal * 252533 % 33554393;
            
            numSteps--;
        }

        return runningTotal;
    }

    private static int CountNumSteps(int row, int col)
    {
        var count = 0;
        
        while (row > 0 && col > 0)
        {
            (row, col) = GetPreviousCoord(row, col);

            count++;
        }

        return count;
    }

    private static (int row, int col) GetPreviousCoord(int row, int col) =>
        col == 1 ? (1, row - 1) : (row + 1, col - 1);
}