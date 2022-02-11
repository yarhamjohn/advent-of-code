using AdventOfCode2015.Day4;

namespace AdventOfCode2015Tests.Day4Tests;

[TestFixture]
public class Day4Tests
{
    [TestCase("abcdef", 609043)]
    [TestCase("pqrstuv", 1048970)]
    public void GetLowestHashableNumber(string input, int num)
    {
        var result = Day4.GetLowestHashableNumber(input, 5);
        Assert.That(result, Is.EqualTo(num));
    }
}