namespace AdventOfCode2022.Day10;

public static class Day10
{
    public static long CalculateSignalStrength(string[] input)
    {
        var cycles = 1;

        var dict = new Dictionary<int, int> { { 1, 1 } };
        
        foreach (var line in input)
        {
            cycles++;

            if (line == "noop")
            {
                dict[cycles] = dict[cycles - 1];
            }
            else
            {
                dict[cycles] = dict[cycles - 1];
                cycles++;
                dict[cycles] = dict[cycles - 1] + Convert.ToInt32(line.Split(" ")[1]);
            }
        }
        
        return dict
            .Where(x => new [] {20, 60, 100, 140, 180, 220}.Contains(x.Key))
            .Sum(y => y.Key * y.Value);
    }
}