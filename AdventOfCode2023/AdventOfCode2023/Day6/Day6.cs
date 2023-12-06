namespace AdventOfCode2023.Day6;

public static class Day6
{
    public static long GetRecords(string[] input)
    {
        return ParseInput(input)
            .Select(CalculateNumWays)
            .Aggregate((a, b) => a * b);
    }

    private static long CalculateNumWays((int time, long distance) race)
    {
        return Enumerable.Range(0, race.time).Count(x => ((long)race.time - x) * x > race.distance);
    }

    public static long GetRecordsBig(string[] input)
    {
        return CalculateNumWays(ParseInputBig(input));
    }

    private static IEnumerable<(int time, long distance)> ParseInput(string[] input)
    {
        var nums = input.Select(x => x.Split(":")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToArray();
        
        return nums.First().Select(int.Parse).Zip(nums.Last().Select(long.Parse));
    }

    private static (int time, long distance) ParseInputBig(string[] input)
    {
        var nums = input.Select(x => x.Split(":")[1].Replace(" ", "")).ToArray();

        return (int.Parse(nums.First()), long.Parse(nums.Last()));
    }
}