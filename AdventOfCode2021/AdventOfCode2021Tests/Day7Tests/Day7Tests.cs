using AdventOfCode2021.Day7;

namespace AdventOfCode2021Tests.Day7Tests;

[TestFixture]
public class Day7Tests
{
    [Test]
    public void Should_calculate_fuel_required()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day7.CalculateFuelRequired(input);
        
        Assert.That(result, Is.EqualTo(37));
    }
    
    [Test]
    public void Should_calculate_fuel_required_including_increase()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day7.CalculateFuelRequiredIncludingIncrease(input);
        
        Assert.That(result, Is.EqualTo(168));
    }
}