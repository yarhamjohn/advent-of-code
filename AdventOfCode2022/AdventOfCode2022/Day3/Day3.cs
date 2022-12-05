namespace AdventOfCode2022.Day3;

public static class Day3
{
    public static int GetPrioritySum(IEnumerable<string> input)
        => input
            .Select(line => CalculatePriority(GetCommonItem(GetCompartments(line))))
            .Sum();

    public static int GetBadgePrioritySum(string[] input)
    {
        var total = 0;

        for (var i = 0; i < input.Length; i += 3)
        {
            total += CalculatePriority(GetBadgeItem(input, i));
        }
        
        return total;
    }

    private static char GetBadgeItem(string[] input, int i)
        => input[i].Intersect(input[i + 1]).Intersect(input[i + 2]).Single();

    private static char[][] GetCompartments(string line)
        => line.Chunk(line.Length / 2).ToArray();

    private static char GetCommonItem(char[][] compartments)
        => compartments.First().Intersect(compartments.Last()).Single();

    private static int CalculatePriority(char intersection)
        => intersection < 97 ? intersection - 38 : intersection - 96;
}