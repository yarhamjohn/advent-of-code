using AdventOfCode2021.Day6;

namespace AdventOfCode2021Tests.Day6Tests;

[TestFixture]
public class Day6Tests
{
    [TestCase(18, 26)]
    [TestCase(80, 5934)]
    [TestCase(256, 26984457539)]
    public void Should_calculate_lantern_fish(int days, long totalFish)
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day6Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day6.CalculateLanternFish(input, days);
        
        Assert.That(result, Is.EqualTo(totalFish));
    }
}