using AdventOfCode2024.Day8;

namespace AdventOfCode2024Tests.Day8Tests;

[TestFixture]
public class Day8Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day8.Part1(input.ToArray());
        Assert.That(result, Is.EqualTo(14));
    }
    
    [Test]
    public void Part2ATest()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);

        var result = Day8.Part2(input.ToArray());
        Assert.That(result, Is.EqualTo(9));
    }
    
    [Test]
    public void Part2BTest()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day8.Part2(input.ToArray());
        Assert.That(result, Is.EqualTo(34));
    }
}