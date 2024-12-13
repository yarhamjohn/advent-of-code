namespace AdventOfCode2024.Day11;

public static class Day11
{
    public static long Part1(string input)
    {
        var stoneList = input.Split(" ").Select(long.Parse).ToArray();

        return stoneList.SelectMany(s => GetNewStones(s, 0, 25)).Count();
    }

    public static long Part2(string input)
    {
        return 0;
    }
    
    private static List<long> GetNewStones(long stone, int depth, int blinks)
    {
        return depth == blinks 
            ? [stone] 
            : GetNewStone(stone).SelectMany(s => GetNewStones(s, depth + 1, blinks)).ToList();
    }

    private static List<long> GetNewStone(long stone)
    {
        if (stone == 0)
        {
           return [1];
        }

        if (stone.ToString().Length % 2 != 0)
        {
            return [stone * 2024];
        }
        
        var stringStone = stone.ToString();

        var newStones = new[]
        {
            stringStone[..(stringStone.Length / 2)],
            stringStone[(stringStone.Length / 2)..]
        };
            
        return newStones.Select(long.Parse).ToList();
    }
}