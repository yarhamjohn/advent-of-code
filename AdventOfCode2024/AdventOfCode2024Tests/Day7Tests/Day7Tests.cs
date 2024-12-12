using AdventOfCode2024.Day7;

namespace AdventOfCode2024Tests.Day7Tests;

[TestFixture]
public class Day7Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day7.Part1(input.ToArray());
        Assert.That(result, Is.EqualTo(3749));
    }
    
    [Test]
    public void Part2Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day7.Part2(input.ToArray());
        Assert.That(result, Is.EqualTo(11387));
    }
}