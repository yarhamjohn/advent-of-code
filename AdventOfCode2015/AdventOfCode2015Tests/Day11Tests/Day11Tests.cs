using AdventOfCode2015.Day11;

namespace AdventOfCode2015Tests.Day11Tests;

[TestFixture]
public class Day11Tests
{
    [TestCase("abcdefgh", "abcdffaa")]
    [TestCase("ghijklmn", "ghjaabcc")]
    public void GetNextPassword(string input, string expected)
    {
        var result = Day11.GetNextPassword(input);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("hhjklmmn")]
    [TestCase("abbceffg")]
    [TestCase("abbcdgjk")]
    public void IsInvalidPassword(string password)
    {
        var result = Day11.IsValidPassword(password);
        Assert.That(result, Is.False);
    }

    [TestCase("abcdffaa")]
    [TestCase("ghjaabcc")]
    public void IsValidPassword(string password)
    {
        var result = Day11.IsValidPassword(password);
        Assert.That(result, Is.True);
    }

    [TestCase("a", "b")]
    [TestCase("aa", "ab")]
    [TestCase("ba", "bb")]
    [TestCase("z", "a")]
    [TestCase("az", "ba")]
    [TestCase("abz", "aca")]
    public static void IncrementsString(string input, string expected)
    {
        var result = Day11.IncrementString(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}