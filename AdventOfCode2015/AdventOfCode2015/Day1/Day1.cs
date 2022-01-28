namespace AdventOfCode2015.Day1;

public static class Day1
{
    public static int GetTargetFloor(string input) => input.Sum(x => x == '(' ? 1 : -1);

    public static int GetBasementEntry(string input)
    {
        var floor = 0;

        for (var i = 0; i < input.Length; i++)
        {
            floor += input[i] == ')' ? -1 : 1;

            if (floor == -1)
            {
                return i + 1; // Result is 1-indexed
            }
        }

        throw new ArgumentException("Never entered the basement");
    }
}