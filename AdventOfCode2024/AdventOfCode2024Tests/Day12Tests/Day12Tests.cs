using AdventOfCode2024.Day12;

namespace AdventOfCode2024Tests.Day12Tests;

[TestFixture]
public class Day12Tests
{
    [Test]
    public void Part1ATest()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day12.Part1(input);
        Assert.That(result, Is.EqualTo(140));
    }
    
    [Test]
    public void Part1BTest()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example2.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day12.Part1(input);
        Assert.That(result, Is.EqualTo(772));
    }
    
    [Test]
    public void Part1CTest()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example3.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day12.Part1(input);
        Assert.That(result, Is.EqualTo(1930));
    }
}