using AdventOfCode2021.Day2;

namespace AdventOfCode2021Tests.Day2Tests;

[TestFixture]
public class Day2Tests
{
    [Test]
    public void Should_calculate_position()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day2.CalculatePosition(input);
        
        Assert.That(result, Is.EqualTo(150));
    }
    
    [Test]
    public void Should_calculate_position_with_aim()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day2.CalculatePositionWithAim(input);
        
        Assert.That(result, Is.EqualTo(900));
    }
}