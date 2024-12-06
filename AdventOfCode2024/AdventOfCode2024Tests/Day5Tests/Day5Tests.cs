using AdventOfCode2024.Day5;

namespace AdventOfCode2024Tests.Day5Tests;

[TestFixture]
public class Day5Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day5.Part1(input.ToArray());
        Assert.That(result, Is.EqualTo(143));
    }
    
    [Test]
    public void Part2Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day5.Part2(input.ToArray());
        Assert.That(result, Is.EqualTo(123));
    }
}