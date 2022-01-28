using AdventOfCode.Day11;

namespace AdventOfCodeTests.Day11Tests;

[TestFixture]
public class Day11Tests
{
    [Test]
    public void Should_calculate_syntax_error_score()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day11Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day11.CalculateNumberOfFlashes(input.ToArray());
        
        Assert.That(result, Is.EqualTo(1656));
    }
    
    [Test]
    public void Should_calculate_first_simultaneous_flash()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day11Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day11.CalculateFirstSimultaneousFlash(input.ToArray());
        
        Assert.That(result, Is.EqualTo(195));
    }
}