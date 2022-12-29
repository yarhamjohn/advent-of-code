using AdventOfCode2022.Day23;

namespace AdventOfCode2022Tests.Day23Tests;

[TestFixture]
public class Day23Tests
{
    [Test]
    public void CountEmptyGroundTiles()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day23Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day23.CountEmptyGroundTiles(input);
        Assert.That(result, Is.EqualTo(110));
    }
    
    [Test]
    public void CountRoundsRequired()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day23Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day23.CountRoundsRequired(input);
        Assert.That(result, Is.EqualTo(20));
    }
}