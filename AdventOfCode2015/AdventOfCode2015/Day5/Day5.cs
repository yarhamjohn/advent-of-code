namespace AdventOfCode2015.Day5;

public static class Day5
{
    public static long GetNumNiceStrings(string[] input) => 
        input.Count(IsNiceString);
    
    public static long GetNumNiceStrings2(string[] input) => 
        input.Count(IsNiceString2);

    private static bool IsNiceString(string s) =>
        ContainsThreeVowels(s) && ContainsLetterPair(s) && !ContainsNaughtyStrings(s);

    private static bool IsNiceString2(string s) =>
        ContainsADuplicateNonOverlappingPair(s) && ContainsLetterSandwich(s);

    private static bool ContainsLetterSandwich(string s)
    {
        for (var i = 0; i < s.Length - 2; i++)
        {
            if (s[i] == s[i + 2])
            {
                return true;
            }
        }

        return false;
    }

    private static bool ContainsADuplicateNonOverlappingPair(string s)
    {
        var pairs = new Dictionary<int, string>();
        for (var i = 0; i < s.Length - 1; i++)
        {
            pairs.Add(i, $"{s[i]}{s[i+1]}");
        }

        var duplicatePairs = new Dictionary<int, string>();
        foreach (var (key, value) in pairs)
        {
            if (pairs.Values.Count(x => x == value) > 1)
            {
                duplicatePairs.Add(key, value);
            }
        }

        foreach (var value in duplicatePairs.Values.Distinct())
        {
            var keys = duplicatePairs.Where(pair => pair.Value == value).Select(pair => pair.Key).ToArray();
            if (keys.Length > 2)
            {
                return true;
            }

            if (keys.Last() - keys.First() > 1)
            {
                return true;
            }
        }

        return false;
    }

    private static bool ContainsNaughtyStrings(string s) => 
        s.Contains("ab") || s.Contains("cd") || s.Contains("pq") || s.Contains("xy");

    private static bool ContainsLetterPair(string s)
    {
        for (var i = 0; i < s.Length - 1; i++)
        {
            if (s[i] == s[i + 1])
            {
                return true;
            }
        }

        return false;
    }

    private static bool ContainsThreeVowels(string s) =>
        s.Count(x => x is 'a' or 'e' or 'i' or 'o' or 'u') >= 3;
}