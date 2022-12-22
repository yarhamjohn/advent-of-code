using AdventOfCode2022.Day19;

namespace AdventOfCode2022Tests.Day19Tests;

[TestFixture]
public class Day19Tests
{
    [Test]
    public void CalculateQualityLevels()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day19.CalculateQualityLevels(input);
        Assert.That(result, Is.EqualTo(33));
    }
}