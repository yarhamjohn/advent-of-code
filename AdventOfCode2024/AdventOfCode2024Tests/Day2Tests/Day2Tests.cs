using AdventOfCode2024.Day2;

namespace AdventOfCode2024Tests.Day2Tests;

[TestFixture]
public class Day2Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day2.Part1(input);
        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void Part2Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day2.Part2(input);
        Assert.That(result, Is.EqualTo(4));
    }
}