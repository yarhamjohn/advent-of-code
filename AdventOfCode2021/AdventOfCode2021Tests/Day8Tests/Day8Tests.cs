using AdventOfCode.Day8;

namespace AdventOfCodeTests.Day8Tests;

[TestFixture]
public class Day8Tests
{
    [Test]
    public void Should_calculate_unique_digit_appearance()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day8.CalculateUniqueDigitAppearance(input);
        
        Assert.That(result, Is.EqualTo(26));
    }
    
    [Test]
    public void Should_calculate_outputs()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day8.CalculateOutputs(input);
        
        Assert.That(result, Is.EqualTo(61229));
    }
}