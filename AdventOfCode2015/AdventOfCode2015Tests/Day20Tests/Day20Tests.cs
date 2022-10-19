using AdventOfCode2015.Day20;

namespace AdventOfCode2015Tests.Day20Tests;

[TestFixture]
public class Day20Tests
{
    [TestCase(10, 1)]
    [TestCase(30, 2)]
    [TestCase(50, 4)]
    [TestCase(75, 6)]
    [TestCase(100, 6)]
    [TestCase(150, 8)]
    public static void CorrectlyCountDistinctMolecules(int input, int expected)
    {
        var result = Day20.GetLowestHouseNumber(input);
        
        Assert.That(result, Is.EqualTo(expected));
    }
}