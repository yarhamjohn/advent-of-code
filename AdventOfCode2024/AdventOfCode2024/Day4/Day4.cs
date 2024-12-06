using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day4;

public static class Day4
{
    public static int Part1(string[] input)
    {
        var verticals = GetVerticals(input);

        var diagonals = GetDiagonals(input);

        return GetCount(input) + GetCount(verticals) + GetCount(diagonals);
    }

    private static IEnumerable<string> GetDiagonals(string[] input)
    {
        List<string> diagonals = [];
        
        for (var col = 0; col < input.First().Length; col++)
        {
            diagonals.Add(GetTopRowDownAndRightDiagonals(input, col));
            diagonals.Add(GetTopRowDownAndLeftDiagonals(input, col));
        }
        
        // Start at 1 for the bottom row because the last col on the top row already covered the first col here
        for (var col = 1; col < input.First().Length - 1; col++)
        {
            diagonals.Add(GetBottomRowUpAndRightDiagonals(input, col));
            diagonals.Add(GetBottomRowUpAndLeftDiagonals(input, col));
        }

        return diagonals;
    }

    private static string GetBottomRowUpAndRightDiagonals(string[] input, int col)
    {
        var row = input.Length - 1;
        
        var newString = new StringBuilder();
        newString.Append(input[row][col]);

        while (true)
        {
            row--;
            col++;
                
            if (col == input.First().Length || row < 0)
            {
                break;
            }

            newString.Append(input[row][col]);
        }

        return newString.ToString();
    }

    private static string GetBottomRowUpAndLeftDiagonals(string[] input, int col)
    {
        var row = input.Length - 1;
        
        var newString = new StringBuilder();
        newString.Append(input[row][col]);

        while (true)
        {
            row--;
            col--;
                
            if (col < 0 || row < 0)
            {
                break;
            }

            newString.Append(input[row][col]);
        }

        return newString.ToString();
    }

    private static string GetTopRowDownAndLeftDiagonals(string[] input, int col)
    {
        var row = 0;
        
        var newString = new StringBuilder();
        newString.Append(input[row][col]);

        while (true)
        {
            row++;
            col--;
                
            if (col < 0 || row == input.Length)
            {
                break;
            }

            newString.Append(input[row][col]);
        }

        return newString.ToString();
    }

    private static string GetTopRowDownAndRightDiagonals(string[] input, int col)
    {
        var row = 0;
        
        var newString = new StringBuilder();
        newString.Append(input[row][col]);

        while (true)
        {
            row++;
            col++;
                
            if (col == input.First().Length || row == input.Length)
            {
                break;
            }

            newString.Append(input[row][col]);
        }

        return newString.ToString();
    }

    private static IEnumerable<string> GetVerticals(string[] input)
    {
        for (var col = 0; col < input.First().Length; col++)
        {
            var vertical = new StringBuilder();
            foreach (var row in input)
            {
                vertical.Append(row[col]);
            }

            yield return vertical.ToString();
        }
    }

    private static int GetCount(IEnumerable<string> rows)
    {
        return rows.Sum(row => 
            Regex.Matches(row, "XMAS").Count + 
            Regex.Matches(row, "SAMX").Count);
    }
}