using AdventOfCode2015.Day1;

namespace AdventOfCode2015Tests.Day1Tests;

[TestFixture]
public class Day1Tests
{
    [TestCase("(())", 0)]
    [TestCase("()()", 0)]
    [TestCase("(((", 3)]
    [TestCase("(()(()(", 3)]
    [TestCase("))(((((", 3)]
    [TestCase("())", -1)]
    [TestCase("))(", -1)]
    [TestCase(")))", -3)]
    [TestCase(")())())", -3)]
    public void GetTargetFloor_Returns_CorrectFloor(string input, int targetFloor)
    {
        var result = Day1.GetTargetFloor(input);
        Assert.That(result, Is.EqualTo(targetFloor));
    }
    
    [TestCase(")", 1)]
    [TestCase("()())", 5)]
    public void GetBasementEntry_Returns_CorrectPosition(string input, int inputPosition)
    {
        var result = Day1.GetBasementEntry(input);
        Assert.That(result, Is.EqualTo(inputPosition));
    }
}