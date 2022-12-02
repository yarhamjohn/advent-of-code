using System.Text;

namespace AdventOfCode2016.Day18;

public static class Day18
{
    public static int CountSafeTiles(string input, int totalRows)
    {
        var rows = new List<string> { input };
        for (var i = 1; i < totalRows; i++)
        {
            rows.Add(CalculateNextRow(rows[i - 1]));
        }
        
        return rows.SelectMany(x => x).Count(y => y == '.');
    }

    private static string CalculateNextRow(string row)
    {
        var result = new StringBuilder();

        for (var i = 0; i < row.Length; i++)
        {
            result.Append(ShouldBeTrap(row, i + 1) ? "^" : ".");
        }
        
        return result.ToString();
    }

    private static bool ShouldBeTrap(string row, int index)
    {
        return ("." + row + ".").Substring(index - 1, 3) is "^^." or ".^^" or "..^" or "^..";
    }
}