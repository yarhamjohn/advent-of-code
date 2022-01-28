using AdventOfCode.Day19;

namespace AdventOfCodeTests.Day19Tests;

[TestFixture]
public class Day19Tests
{
    [Test]
    public void Should_calculate_final_magnitude()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day19.CalculateBeacons(input.ToArray());
        
        Assert.That(result, Is.EqualTo(79));
    }
}