namespace AdventOfCode2023.Day13;

public static class Day13
{
    public static long ReflectionSum(string[] input)
    {
        var result = 0;

        var patterns = ParseInput(input);
        foreach (var pattern in patterns)
        {
            if (IsHorizontalReflection(pattern, out var numRowsAbove))
            {
                result += numRowsAbove * 100;
                continue;
            }

            if (IsVerticalReflection(pattern, out var numColsLeft))
            {
                result += numColsLeft;
            }
        }

        return result;
    }

    public static long ReflectionSmudgeSum(string[] input)
    {
        var result = 0;

        var patterns = ParseInput(input);
        foreach (var pattern in patterns)
        {
            Console.WriteLine("*********************");
            PrintPattern(pattern);

            var found = false;

            var originalReflection = DetermineOriginalReflection(pattern);

            Console.WriteLine(originalReflection);
            Console.WriteLine("*********************");

            for (var row = 0; row < pattern.Count; row++)
            {
                for (var col = 0; col < pattern.First().Length; col++)
                {
                    // insert smudge
                    SmudgePattern(pattern, row, col);

                    if (IsHorizontalReflection(pattern, out var numRowsAbove, originalReflection))
                    {
                        result += numRowsAbove * 100;
                        found = true;

                        Console.WriteLine($"Horizontal---- {row}, {col} : {numRowsAbove}  ---------");
                        PrintPattern(pattern);

                        // reset smudge
                        SmudgePattern(pattern, row, col);
                        break;
                    }

                    if (IsVerticalReflection(pattern, out var numColsLeft, originalReflection))
                    {
                        result += numColsLeft;
                        found = true;

                        Console.WriteLine($"Vertical---- {row}, {col} : {numColsLeft} ---------");
                        PrintPattern(pattern);

                        // reset smudge
                        SmudgePattern(pattern, row, col);
                        break;
                    }
                    
                    // reset smudge
                    SmudgePattern(pattern, row, col);
                }

                if (found)
                {
                    break;
                }
            }
        }

        return result;
    }

    private static void SmudgePattern(List<string[]> pattern, int row, int col)
    {
        if (pattern[row][col] == "#")
        {
            pattern[row][col] = ".";
        }
        else
        {
            pattern[row][col] = "#";
        }
    }

    private static (string BindingDirection, int num)? DetermineOriginalReflection(List<string[]> pattern)
    {
        (string BindingDirection, int num)? originalReflection = null;
        
        if (IsHorizontalReflection(pattern, out var initialRowsAbove))
        {
            originalReflection = ("horizontal", initialRowsAbove);
        }
        else if (IsVerticalReflection(pattern, out var initialColsLeft))
        {
            originalReflection = ("vertical", initialColsLeft);
        }

        return originalReflection;
    }

    private static bool IsVerticalReflection(IReadOnlyCollection<string[]> pattern, out int numColsLeft, (string bindingDirection, int num)? originalReflection = null)
    {
        numColsLeft = 0;
        for (var col = 0; col < pattern.First().Length; col++)
        {
            // reached the end of the cols
            if (col == pattern.First().Length - 1)
            {
                return false;
            }
            
            if (GetCol(pattern, col).Zip(GetCol(pattern, col + 1)).All(x => x.First == x.Second))
            {
                if (IsValidColReflection(pattern, col, col + 1, originalReflection))
                {
                    numColsLeft = col + 1;
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsValidColReflection(IReadOnlyCollection<string[]> pattern, int leftCol, int rightCol, (string bindingDirection, int num)? originalReflection)
    {
        // We know we can't use the same reflection as the original
        if (originalReflection?.bindingDirection == "vertical" &&
            originalReflection?.num == leftCol + 1)
        {
            return false;
        }
        
        var colPairs = new List<(int leftCol, int rightCol)>();
        while (leftCol >= 0 && rightCol < pattern.First().Length)
        {
            colPairs.Add((leftCol, rightCol));
            leftCol--;
            rightCol++;
        }

        foreach (var (left, right) in colPairs)
        {
            if (GetCol(pattern, left).Zip(GetCol(pattern, right)).Any(x => x.First != x.Second))
            {
                return false;
            }
        }

        return true;
    }

    private static string[] GetCol(IEnumerable<string[]> pattern, int col)
    {
        return pattern.Select(t => t[col]).ToArray();
    }

    private static bool IsHorizontalReflection(List<string[]> pattern, out int numRowsAbove, (string bindingDirection, int num)? originalReflection = null)
    {
        numRowsAbove = 0;
        for (var row = 0; row < pattern.Count; row++)
        {
            // reached the end of the rows
            if (row == pattern.Count - 1)
            {
                return false;
            }
            
            if (pattern[row].Zip(pattern[row + 1]).All(x => x.First == x.Second))
            {
                if (IsValidRowReflection(pattern, row, row + 1, originalReflection))
                {
                    numRowsAbove = row + 1;
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsValidRowReflection(IReadOnlyList<string[]> pattern, int topRow, int bottomRow, (string bindingDirection, int num)? originalReflection)
    {
        // We know we can't use the same reflection as the original
        if (originalReflection?.bindingDirection == "horizontal" &&
            originalReflection?.num == topRow + 1)
        {
            return false;
        }
        
        var rowPairs = new List<(int topRow, int bottomRow)>();
        while (topRow >= 0 && bottomRow < pattern.Count)
        {
            rowPairs.Add((topRow, bottomRow));
            topRow--;
            bottomRow++;
        }

        foreach (var (top, bottom) in rowPairs)
        {
            if (pattern[top].Zip(pattern[bottom]).Any(x => x.First != x.Second))
            {
                return false;
            }
        }

        return true;
    }

    private static List<List<string[]>> ParseInput(string[] input)
    {
        var result = new List<List<string[]>>();
        var isInPattern = false;
        var pattern = new List<string[]>();
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                result.Add(pattern);
                pattern = [];
                isInPattern = false;
            }
            else
            {
                if (isInPattern)
                {
                    pattern.Add(line.ToCharArray().Select(x => x.ToString()).ToArray());
                    continue;
                }

                pattern = [line.ToCharArray().Select(x => x.ToString()).ToArray()];
                isInPattern = true;
            }
        }
        
        if (pattern.Count != 0)
        {
            result.Add(pattern);
        }

        return result;
    }

    private static void PrintPattern(List<string[]> pattern)
    {
        for (var row = 0; row < pattern.Count; row++)
        {
            for (var col = 0; col < pattern.First().Length; col++)
            {
                Console.Write(pattern[row][col]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}