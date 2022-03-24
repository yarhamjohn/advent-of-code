using AdventOfCode2015.Day6;

namespace AdventOfCode2015Tests.Day6Tests;

[TestFixture]
public class Day6Tests
{
    [TestCase("turn on 0,0 through 999,999", 1000000)]
    [TestCase("toggle 0,0 through 999,0", 1000)]
    [TestCase("turn off 499,499 through 500,500", 0)]
    public void GetNumLitLights(string input, int numLitLights)
    {
        var result = Day6.GetNumLitLights(new[] {input});
        Assert.That(result, Is.EqualTo(numLitLights));
    }
    
    [TestCase("turn on 0,0 through 0,0", 1)]
    [TestCase("toggle 0,0 through 999,999", 2000000)]
    public void GetTotalBrightness(string input, int totalBrightness)
    {
        var result = Day6.GetTotalBrightness(new[] {input});
        Assert.That(result, Is.EqualTo(totalBrightness));
    }
}