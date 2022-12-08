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

    public static long CalculateHighestScenicScore(string[] input)
    {
        var scenicScore = 0L;

        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                var score = CalculateScenicScore(input, i, j);
                if (score > scenicScore)
                {
                    scenicScore = score;
                }
            }
        }
        
        return scenicScore;
    }

    private static long CalculateScenicScore(string[] input, int row, int col)
    {
        var left = 0;
        var right = 0;
        var top = 0;
        var bottom = 0;
        var target = input[row][col];

        var leftTrees = GetLeftTrees(input, row, col).ToArray();
        for (var i = leftTrees.Length - 1; i >= 0; i--)
        {
            if (leftTrees[i] < target)
            {
                left++;
            }

            if (leftTrees[i] >= target)
            {
                left++;
                break;
            }
        }
        
        var rightTrees = GetRightTrees(input, row, col).ToArray();
        for (var i = 0; i < rightTrees.Length; i++)
        {
            if (rightTrees[i] < target)
            {
                right++;
            }

            if (rightTrees[i] >= target)
            {
                right++;
                break;
            }
        }
        
        var topTrees = GetTopTrees(input, row).Select(x => x[col]).ToArray();
        for (var i = topTrees.Length - 1; i >= 0; i--)
        {
            if (topTrees[i] < target)
            {
                top++;
            }

            if (topTrees[i] >= target)
            {
                top++;
                break;
            }
        }

        var bottomTrees = GetBottomTrees(input, row).Select(x => x[col]).ToArray();
        for (var i = 0; i < bottomTrees.Length; i++)
        {
            if (bottomTrees[i] < target)
            {
                bottom++;
            }

            if (bottomTrees[i] >= target)
            {
                bottom++;
                break;
            }
        }
        
        return left * right * top * bottom;
    }

    private static bool IsHidden(IReadOnlyList<string> input, int row, int col)
    {
        var target = input[row][col];
        return GetLeftTrees(input, row, col).Any(x => x >= target)
               && GetRightTrees(input, row, col).Any(x => x >= target)
               && GetTopTrees(input, row).Any(x => x[col] >= target) 
               && GetBottomTrees(input, row).Any(x => x[col] >= target);
    }

    private static IEnumerable<string> GetBottomTrees(IReadOnlyList<string> input, int row)
    {
        return input.Where((_, i) => i > row);
    }

    private static IEnumerable<string> GetTopTrees(IReadOnlyList<string> input, int row)
    {
        return input.Where((_, i) => i < row);
    }

    private static IEnumerable<char> GetRightTrees(IReadOnlyList<string> input, int row, int col)
    {
        return input[row][(col + 1)..];
    }

    private static IEnumerable<char> GetLeftTrees(IReadOnlyList<string> input, int row, int col)
    {
        return input[row][..col];
    }
}