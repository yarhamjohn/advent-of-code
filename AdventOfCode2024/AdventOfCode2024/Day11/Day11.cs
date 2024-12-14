namespace AdventOfCode2024.Day11;

public static class Day11
{
    public static long Part1(string input)
    {
        return Evaluate(input, 25).Values.Sum();
    }

    public static long Part2(string input)
    {
        return Evaluate(input, 75).Values.Sum();
    }

    private static Dictionary<long, long> Evaluate(string input, int totalBlinks)
    {
        var stoneMap = input.Split(" ").Select(long.Parse).ToDictionary(x => x, _ => 1L);

        for (var blinks = 0; blinks < totalBlinks; blinks++)
        {
            var nextStoneMap = new Dictionary<long, long>();

            foreach (var (stone, count) in stoneMap)
            {
                foreach (var s in GetNextStones(stone))
                {
                    nextStoneMap.TryAdd(s, 0);
                    nextStoneMap[s] += count;
                }
            }

            stoneMap = nextStoneMap;
        }

        return stoneMap;
    }

    private static IEnumerable<long> GetNextStones(long stone)
    {
        if (stone == 0)
        {
           return [1];
        }

        var stringStone = stone.ToString();
        
        if (stringStone.Length % 2 != 0)
        {
            return [stone * 2024];
        }
        
        var newStones = new[]
        {
            stringStone[..(stringStone.Length / 2)],
            stringStone[(stringStone.Length / 2)..]
        };
            
        return newStones.Select(long.Parse);
    }
}