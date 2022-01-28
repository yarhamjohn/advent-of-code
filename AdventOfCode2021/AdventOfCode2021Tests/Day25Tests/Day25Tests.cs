using AdventOfCode2021.Day25;

namespace AdventOfCode2021Tests.Day25Tests;

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