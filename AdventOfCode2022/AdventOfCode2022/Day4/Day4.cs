namespace AdventOfCode2022.Day4;

public static class Day4
{
    public static int CountContainedPairs(IEnumerable<string> input)
        => ParseInput(input).Count(x => HasContainedRange(x.First(), x.Last()));

    public static int CountOverlappingPairs(IEnumerable<string> input)
        => ParseInput(input).Count(x => HasOverlappingRange(x.First(), x.Last()));
    
    private static IEnumerable<(int, int)[]> ParseInput(IEnumerable<string> input)
        => input.Select(line => line.Split(",").Select(GetRange).ToArray());

    private static bool HasContainedRange((int, int) rangeOne, (int, int) rangeTwo)
    {
        var rangeOneIsContainedByRangeTwo = rangeOne.Item1 >= rangeTwo.Item1 && rangeOne.Item2 <= rangeTwo.Item2;
        var rangeTwoIsContainedByRangeOne = rangeTwo.Item1 >= rangeOne.Item1 && rangeTwo.Item2 <= rangeOne.Item2;

        return rangeOneIsContainedByRangeTwo || rangeTwoIsContainedByRangeOne;
    }

    private static bool HasOverlappingRange((int, int) rangeOne, (int, int) rangeTwo)
    {
        var rangeOneStartContainedWithinRangeTwo = rangeOne.Item1 >= rangeTwo.Item1 && rangeOne.Item1 <= rangeTwo.Item2;
        var rangeTwoStartContainedWithinRangeOne = rangeTwo.Item1 >= rangeOne.Item1 && rangeTwo.Item1 <= rangeOne.Item2;

        return rangeOneStartContainedWithinRangeTwo || rangeTwoStartContainedWithinRangeOne;
    }
    
    private static (int, int) GetRange(string x)
    {
        var ends = x.Split("-").Select(y => Convert.ToInt32(y)).ToArray();
        return (ends.First(), ends.Last());
    }
}