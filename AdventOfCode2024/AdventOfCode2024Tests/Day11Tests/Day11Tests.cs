using AdventOfCode2024.Day11;

namespace AdventOfCode2024Tests.Day11Tests;

[TestFixture]
public class Day11Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day11Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);

        var result = Day11.Part1(input);
        Assert.That(result, Is.EqualTo(55312));
    }
}