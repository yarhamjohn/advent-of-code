using AdventOfCode2024.Day6;

namespace AdventOfCode2024Tests.Day6Tests;

[TestFixture]
public class Day6Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day6Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day6.Part1(input.ToArray());
        Assert.That(result, Is.EqualTo(41));
    }
    
    [Test]
    public void Part2Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day6Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day6.Part2(input.ToArray());
        Assert.That(result, Is.EqualTo(6));
    }
}