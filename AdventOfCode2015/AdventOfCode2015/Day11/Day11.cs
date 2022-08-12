namespace AdventOfCode2015.Day11;

public static class Day11
{
    public static string GetNextPassword(string input)
    {
        var nextPassword = input;

        do
        {
            nextPassword = IncrementString(nextPassword);
        } while (!IsValidPassword(nextPassword));

        return nextPassword;
    }

    public static string IncrementString(string input)
    {
        var chars = input.ToCharArray();
        if (chars[^1] != 'z')
        {
            chars[^1]++;
        } 
        else if (chars.Length == 1)
        {
            chars[^1] = 'a';
        }
        else
        {
            var slice = string.Join("", chars[..^1]);
            chars = IncrementString(slice).Append('a').ToArray();
        }

        return string.Join("", chars);
    }

    public static bool IsValidPassword(string password) =>
        !ContainsInvalidChars(password) && ContainsTwoPairs(password) && ContainsThreeCharStraight(password);

    private static bool ContainsInvalidChars(string password) =>
        password.Contains('i') || password.Contains('o') || password.Contains('l');

    private static bool ContainsTwoPairs(string password)
    {
        var numPairs = 0;
        for (var i = 0; i < password.Length - 1; i++)
        {
            if (password[i] == password[i + 1])
            {
                numPairs++;
                i++;
            }
        }

        return numPairs >= 2;
    }

    private static bool ContainsThreeCharStraight(string password)
        => new[] { 0, 1, 2, 3, 4, 5 }.Any(index =>
            password[index] + 1 == password[index + 1] &&
            password[index] + 2 == password[index + 2]);
}