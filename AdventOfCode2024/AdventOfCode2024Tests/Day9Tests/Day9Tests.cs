using AdventOfCode2024.Day9;

namespace AdventOfCode2024Tests.Day9Tests;

[TestFixture]
public class Day9Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);

        var result = Day9.Part1(input);
        Assert.That(result, Is.EqualTo(1928));
    }
    
    [Test]
    public void Part2Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);

        var result = Day9.Part2(input.ToArray());
        Assert.That(result, Is.EqualTo(9));
    }
}