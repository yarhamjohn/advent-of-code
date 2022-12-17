using AdventOfCode2022.Day15;

namespace AdventOfCode2022Tests.Day15Tests;

[TestFixture]
public class Day15Tests
{
    [Test]
    public void CalculatePositionsWithoutBeacons()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day15Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day15.CountPositionsWithoutBeacons(input.ToArray(), 10);
        Assert.That(result, Is.EqualTo(26));
    }
    
    [Test]
    public void CalculateTuningFrequency()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day15Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day15.CalculateTuningFrequency(input.ToArray(), 20);
        Assert.That(result, Is.EqualTo(56000011));
    }
}