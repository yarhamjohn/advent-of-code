using AdventOfCode.Day14;

namespace AdventOfCodeTests.Day14Tests;

[TestFixture]
public class Day14Tests
{
    [Test]
    public void Should_calculate_polymer_after_10_turns()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day14.CalculatePolymerTemplate(input.ToArray(), 10);
        
        Assert.That(result, Is.EqualTo(1588));
    }
    
    [Test]
    public void Should_calculate_polymer_after_40_turns()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day14.CalculatePolymerTemplateForFortyTurns(input.ToArray());
        
        Assert.That(result, Is.EqualTo(2188189693529));
    }
}