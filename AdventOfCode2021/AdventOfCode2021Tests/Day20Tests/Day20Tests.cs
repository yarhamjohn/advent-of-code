using AdventOfCode2021.Day20;

namespace AdventOfCode2021Tests.Day20Tests;

[TestFixture]
public class Day20Tests
{
    [Test]
    public void Should_calculate_lit_pixels()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day20Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day20.CalculateLitPixels(input.ToArray(), 2);
        
        Assert.That(result, Is.EqualTo(35));
    }
    
    [Test]
    public void Should_calculate_lit_pixels_big()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day20Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day20.CalculateLitPixels(input.ToArray(), 50);
        
        Assert.That(result, Is.EqualTo(3351));
    }
}