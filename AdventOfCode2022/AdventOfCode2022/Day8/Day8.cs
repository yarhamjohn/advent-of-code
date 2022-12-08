namespace AdventOfCode2022.Day8;

public static class Day8
{
    public static long CountVisibleTrees(string[] input)
    {
        var count = 0;
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                count += IsHidden(input, i, j) ? 0 : 1;
            }
        }
        
        return count;
    }

    private static bool IsHidden(IReadOnlyList<string> input, int row, int col)
    {
        var target = input[row][col];
        return input[row][..col].Any(x => x >= target) 
               && input[row][(col + 1)..].Any(x => x >= target) 
               && input.Where((_, i) => i < row).Any(x => x[col] >= target) 
               && input.Where((_, i) => i > row).Any(x => x[col] >= target);
    }
}