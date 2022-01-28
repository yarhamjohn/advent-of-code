using AdventOfCode.Day15;

namespace AdventOfCodeTests.Day15Tests;

[TestFixture]
public class Day15Tests
{
    [Test]
    public void Should_calculate_path()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day15Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day15.CalculatePath(input.ToArray());
        
        Assert.That(result, Is.EqualTo(40));
    }
    
    [Test]
    public void Should_calculate_path_big()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day15Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day15.CalculatePathBig(input.ToArray());
        
        Assert.That(result, Is.EqualTo(315));
    }
}