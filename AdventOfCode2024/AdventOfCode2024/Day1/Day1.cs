namespace AdventOfCode2024.Day1;

public static class Day1
{
    public static int Part1(IEnumerable<string> input)
    {
        var (leftCol, rightCol) = GetIntegerLists(input);

        leftCol.Sort();
        rightCol.Sort();

        return leftCol.Zip(rightCol).Sum(a => Math.Abs(a.First - a.Second));
    }

    public static int Part2(IEnumerable<string> input)
    {
        var (leftCol, rightCol) = GetIntegerLists(input);

        return leftCol.Sum(item => item * rightCol.Count(x => x == item));
    }
    
    private static (List<int> leftCol, List<int> rightCol) GetIntegerLists(IEnumerable<string> input)
    {
        var leftCol = new List<int>();
        var rightCol = new List<int>();

        foreach (var line in input)
        {
            var digits = line.Split("   ").Select(int.Parse).ToArray();
            leftCol.Add(digits[0]);
            rightCol.Add(digits[1]);
        }

        return (leftCol, rightCol);
    }
}