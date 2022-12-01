namespace AdventOfCode2022.Day1;

public static class Day1
{
    public static int GetTotalCalories(IEnumerable<string> input, int numElves)
    {
        var elves = new List<int>();

        var currentElf = 0;
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                elves.Add(currentElf);
                currentElf = 0;
            }
            else
            {
                currentElf += Convert.ToInt32(line);
            }
        }
        
        elves.Add(currentElf);

        return elves.OrderByDescending(x => x).ToArray()[..numElves].Sum();
    }
}