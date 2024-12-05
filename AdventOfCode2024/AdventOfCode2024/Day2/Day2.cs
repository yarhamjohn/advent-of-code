namespace AdventOfCode2024.Day2;

public static class Day2
{
    public static int Part1(IEnumerable<string> input)
    {
        return input
            .Select(line => line.Split(" ").Select(int.Parse).ToList())
            .Count(IsSafeReport);
    }

    public static int Part2(IEnumerable<string> input)
    {
        return input
            .Select(line => line.Split(" ").Select(int.Parse).ToList())
            .Count(report => IsSafeReport(report) || IsToleratedReport(report));
    }

    private static bool IsSafeReport(List<int> levels)
    {
        var steps = new List<int>();
        
        for (var i = 0; i < levels.Count - 1; i++)
        {
            steps.Add(levels[i] - levels[i + 1]);
        }

        return steps.All(y => y is > 0 and <= 3) || steps.All(y => y is < 0 and >= -3);
    }

    private static bool IsToleratedReport(List<int> levels)
    {
        for (var i = 0; i < levels.Count - 1; i++)
        {
            levels.RemoveAt(i);
            
            if (IsSafeReport(levels.ToList()))
            {
                return true;
            }
        }

        return false;
    }
}