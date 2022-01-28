using AdventOfCode2021.Day3;

namespace AdventOfCode2021Tests.Day3Tests;

[TestFixture]
public class Day3Tests
{
    [Test]
    public void Should_calculate_power_consumption()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day3.CalculatePowerConsumption(input);
        
        Assert.That(result, Is.EqualTo(198));
    }
    
    [Test]
    public void Should_calculate_life_support_rating()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day3.CalculateLifeSupportRating(input);
        
        Assert.That(result, Is.EqualTo(230));
    }
}