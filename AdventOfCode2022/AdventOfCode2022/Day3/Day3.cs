namespace AdventOfCode2022.Day3;

public static class Day3
{
    public static int GetPrioritySum(IEnumerable<string> input)
    {
        var total = 0;
        
        foreach (var line in input)
        {
            var compartments = line.Chunk(line.Length / 2).ToArray();
            var intersection = compartments.First().Intersect(compartments.Last()).Single();

            if (intersection >= 97 && intersection <= 122)
            {
                total += intersection - 96;
            }
            else
            {
                total += intersection - 38;
            }
        }
        
        return total;
    }
}