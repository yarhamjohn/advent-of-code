using AdventOfCode2021.Day9;

namespace AdventOfCode2021Tests.Day9Tests;

[TestFixture]
public class Day9Tests
{
    [Test]
    public void Should_calculate_risk_level()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day9.CalculateRiskLevel(input.ToArray());
        
        Assert.That(result, Is.EqualTo(15));
    }
    
    [Test]
    public void Should_calculate_basins()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day9.CalculateBasins(input.ToArray());
        
        Assert.That(result, Is.EqualTo(1134));
    }
}