namespace AdventOfCode2022.Day8;

public static class Day8
{
    public static long CountVisibleTrees(string[] input)
        => input
            .SelectMany((x, i) => x.Select((_, j) => IsHidden(input, i, j) ? 0 : 1))
            .Sum();

    public static long CalculateHighestScenicScore(string[] input)
        => input
            .SelectMany((x, i) => x.Select((_, j) => CalculateScenicScore(input, i, j)))
            .Max();

    private static long CalculateScenicScore(IReadOnlyList<string> input, int row, int col)
        => CountVisibleTrees(GetLeftTrees(input, row, col).Reverse(), input[row][col])
           * CountVisibleTrees(GetRightTrees(input, row, col), input[row][col])
           * CountVisibleTrees(GetTopTrees(input, row, col).Reverse(), input[row][col])
           * CountVisibleTrees(GetBottomTrees(input, row, col), input[row][col]);

    private static long CountVisibleTrees(IEnumerable<char> trees, char target)
    {
        var count = 0;

        foreach (var tree in trees)
        {
            count++;

            if (tree >= target)
            {
                break;
            }
        }

        return count;
    }

    private static bool IsHidden(IReadOnlyList<string> input, int row, int col)
    {
        return GetLeftTrees(input, row, col).Any(x => x >= input[row][col])
               && GetRightTrees(input, row, col).Any(x => x >= input[row][col])
               && GetTopTrees(input, row, col).Any(x => x >= input[row][col]) 
               && GetBottomTrees(input, row, col).Any(x => x >= input[row][col]);
    }

    private static char[] GetBottomTrees(IEnumerable<string> input, int row, int col)
        => input.Where((_, i) => i > row).Select(x => x[col]).ToArray();

    private static char[] GetTopTrees(IEnumerable<string> input, int row, int col)
        => input.Where((_, i) => i < row).Select(x => x[col]).ToArray();

    private static char[] GetRightTrees(IReadOnlyList<string> input, int row, int col)
        => input[row][(col + 1)..].ToArray();

    private static char[] GetLeftTrees(IReadOnlyList<string> input, int row, int col)
        => input[row][..col].ToArray();
}