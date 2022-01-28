using AdventOfCode.Day10;

namespace AdventOfCodeTests.Day10Tests;

[TestFixture]
public class Day10Tests
{
    [Test]
    public void Should_calculate_syntax_error_score()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day10.CalculateSyntaxErrorScore(input.ToArray());
        
        Assert.That(result, Is.EqualTo(26397));
    }
    
    [Test]
    public void Should_calculate_middle_completion_score()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day10.CalculateMiddleCompletionScore(input.ToArray());
        
        Assert.That(result, Is.EqualTo(288957));
    }
}