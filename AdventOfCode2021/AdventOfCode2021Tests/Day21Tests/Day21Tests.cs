using AdventOfCode.Day21;

namespace AdventOfCodeTests.Day21Tests;

[TestFixture]
public class Day21Tests
{
    [Test]
    public void Should_calculate_deterministic_score()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day21Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day21.CalculateDeterministicScore(input.ToArray());
        
        Assert.That(result, Is.EqualTo(739785));
    }
    
    [Test]
    public void Should_calculate_quantum_score()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day21Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day21.CalculateQuantumScore(input.ToArray());
        
        Assert.That(result, Is.EqualTo(444356092776315));
    }
}