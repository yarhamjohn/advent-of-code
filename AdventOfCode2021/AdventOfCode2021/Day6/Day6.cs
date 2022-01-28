namespace AdventOfCode.Day6;

public static class Day6
{
    public static long CalculateLanternFish(string input, int days)
    {
        var fishAges = Enumerable.Range(0, 9).ToDictionary(age => age, _ => 0L);

        var startingAges = input
            .Split(",")
            .GroupBy(Convert.ToInt32)
            .Select(age => new { age.Key, Value = age.Count() })
            .ToArray();

        foreach (var age in startingAges)
        {
            fishAges[age.Key] = age.Value;
        }

        for (var day = 1; day <= days; day++)
        {
            fishAges = new Dictionary<int, long>
            {
                { 0, fishAges[1] },
                { 1, fishAges[2] },
                { 2, fishAges[3] },
                { 3, fishAges[4] },
                { 4, fishAges[5] },
                { 5, fishAges[6] },
                { 6, fishAges[7] + fishAges[0] },
                { 7, fishAges[8] },
                { 8, fishAges[0] }
            };
        }

        return fishAges.Sum(x => x.Value);
    }
}

