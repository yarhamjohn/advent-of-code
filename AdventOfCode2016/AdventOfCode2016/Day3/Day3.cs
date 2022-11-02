namespace AdventOfCode2016.Day3;

public static class Day3
{
    public static int CountPossibleTriangles(IEnumerable<string> input)
    {
        return input
            .Select(triangle => triangle.Split(" ")
                .Where(x => x.Length != 0)
                .Select(x => Convert.ToInt32(x))
                .ToArray())
            .Count(IsValidTriangle);
    }

    private static bool IsValidTriangle(int[] sides) =>
        sides[0] + sides[1] > sides[2] 
        && sides[0] + sides[2] > sides[1]
        && sides[1] + sides[2] > sides[0];
}