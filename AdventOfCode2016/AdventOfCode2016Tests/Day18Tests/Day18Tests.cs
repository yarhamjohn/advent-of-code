using AdventOfCode2016.Day18;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day18Tests;

[TestFixture]
public class Day18Tests
{
    [TestCase("..^^.", 3, 6)]
    [TestCase(".^^.^.^^^^", 10, 38)]
    public void CountSafeTiles(string input, int totalRows, int expected)
    {
        var result = Day18.CountSafeTiles(input, totalRows);
        Assert.That(result, Is.EqualTo(expected));
    }
}