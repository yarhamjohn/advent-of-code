using AdventOfCode2024.Day10;

namespace AdventOfCode2024Tests.Day10Tests;

[TestFixture]
public class Day10Tests
{
    [Test]
    public void Part1ATest()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day10.Part1(input);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void Part1BTest()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example2.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day10.Part1(input);
        Assert.That(result, Is.EqualTo(36));
    }
    
    [Test]
    public void Part2Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day10.Part2(input);
        Assert.That(result, Is.EqualTo(2858));
    }
}