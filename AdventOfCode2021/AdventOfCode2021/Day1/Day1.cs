namespace AdventOfCode.Day1;

public static class Day1
{
    public static int CalculateTimesDepthIncreased(IEnumerable<string> input) => 
        Calculate(input.Select(x => Convert.ToInt32(x)).ToArray());
    
    public static int CalculateTimesWindowDepthIncreased(string[] input) =>
        Calculate(input.Select((_, i) => input.Skip(i).Take(3).Sum(Convert.ToInt32)).ToArray());

    private static int Calculate(IReadOnlyList<int> input) =>
        input.Where((x, i) => i != 0 && x > input[i - 1]).Count();
}