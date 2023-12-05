namespace AdventOfCode2023.Day5;

public static class Day5
{
    public static int GetLowestLocationNumber(IEnumerable<string> input)
    {
        var seeds = input.First().Split(":")[1].Split(" ");
        var mappings = new Dictionary<string, List<Mapping>>();
        return 0;
    }
}

public class Mapping(string type, int sourceStart, int destinationStart, int length)
{
    public readonly string Type = type;
    public readonly int SourceStart = sourceStart;
    public readonly int DestinationStart = destinationStart;
    public readonly int Length = length;

    public bool IsInRange(int sourceNumber)
    {
        return sourceNumber >= SourceStart && sourceNumber < SourceStart + Length;
    }

    public int GetDestinationNumber(int sourceNumber)
    {
        return sourceNumber - SourceStart + DestinationStart;
    }
}
