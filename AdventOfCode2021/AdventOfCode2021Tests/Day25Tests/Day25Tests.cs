using AdventOfCode.Day25;

namespace AdventOfCodeTests.Day25Tests;

[TestFixture]
public class Day25Tests
{
    [Test]
    public void Should_calculate_lowest_energy()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day25Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day25.CalculateStepsWithoutCucumbers(input.ToArray());
        
        Assert.That(result, Is.EqualTo(58));
    }
}