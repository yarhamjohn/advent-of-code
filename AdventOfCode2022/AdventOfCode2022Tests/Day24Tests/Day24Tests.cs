using AdventOfCode2022.Day24;

namespace AdventOfCode2022Tests.Day24Tests;

[TestFixture]
public class Day24Tests
{
    [Test]
    public void CountEmptyGroundTiles()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day24Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day24.CountMinutesTaken(input);
        Assert.That(result, Is.EqualTo(18));
    }
}