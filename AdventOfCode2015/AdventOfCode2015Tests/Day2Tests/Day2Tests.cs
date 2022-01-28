using AdventOfCode2015.Day2;

namespace AdventOfCode2015Tests.Day2Tests;

[TestFixture]
public class Day2Tests
{
    [TestCase("2x3x4", 58)]
    [TestCase("1x1x10", 43)]
    public void GetRequiredWrappingPaper_Returns_CorrectArea(string input, int targetArea)
    {
        var result = Day2.GetRequiredArea(new[] {input});
        Assert.That(result, Is.EqualTo(targetArea));
    }
    
    [TestCase("2x3x4", 34)]
    [TestCase("1x1x10", 14)]
    public void GetRequiredRibbon_Returns_CorrectLength(string input, int targetLength)
    {
        var result = Day2.GetRequiredLength(new[] {input});
        Assert.That(result, Is.EqualTo(targetLength));
    }
}