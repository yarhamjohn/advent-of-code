namespace AdventOfCode2016.Day3;

public static class Day3
{
    public static int CountPossibleTrianglesVertically(IEnumerable<string> input)
    {
        var chunks = input
            .Select(triangle => triangle.Split(" ")
                .Where(x => x.Length != 0)
                .Select(x => Convert.ToInt32(x))
                .ToArray())
            .Chunk(3);

        var count = 0;
        foreach (var chunk in chunks) {
            for (var i = 0; i < chunk[0].Length; i++)
            {
                if (IsValidTriangle(new[] { chunk[0][i], chunk[1][i], chunk[2][i] }))
                {
                    count++;
                }
            }
        }

        return count;
    }
    
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