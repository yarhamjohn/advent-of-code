using AdventOfCode2021.Day1;

namespace AdventOfCode2021Tests.Day1Tests;

[TestFixture]
public class Day1Tests
{
    [Test]
    public void Should_calculate_times_depth_increased()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day1.CalculateTimesDepthIncreased(input);
        
        Assert.That(result, Is.EqualTo(7));
    }
    
    [Test]
    public void Should_calculate_times_window_depth_increased()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day1.CalculateTimesWindowDepthIncreased(input);
        
        Assert.That(result, Is.EqualTo(5));
    }
}