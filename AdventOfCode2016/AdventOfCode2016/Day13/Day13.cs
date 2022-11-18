namespace AdventOfCode2016.Day13;

public static class Day13
{
    public static long CountSteps((int col, int row) targetCoordinate, int favouriteNumber)
    {
        var startingCoordinate = (col: 1, row: 1);

        for (var row = 0; row < 31; row++)
        {
            for (var col = 0; col < 39; col++)
            {
                Console.Write(IsOpenSpace((col, row), favouriteNumber) ? "." : "#");
            }

            Console.WriteLine();
        }
        
        return 0;
    }

    public static bool IsOpenSpace((int col, int row) coordinate, int favouriteNumber)
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