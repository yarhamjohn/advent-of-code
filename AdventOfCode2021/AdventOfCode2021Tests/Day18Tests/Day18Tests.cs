using AdventOfCode.Day18;

namespace AdventOfCodeTests.Day18Tests;

[TestFixture]
public class Day18Tests
{
    [Test]
    public void Should_calculate_final_magnitude()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day18Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day18.CalculateMagnitude(input.ToArray());
        
        Assert.That(result, Is.EqualTo(4140));
    }
    
    [Test]
    public void Should_calculate_biggest_magnitude_for_two_numbers()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day18Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day18.CalculateBiggestMagnitude(input.ToArray());
        
        Assert.That(result, Is.EqualTo(3993));
    }
}