using AdventOfCode2024.Day1;

namespace AdventOfCode2024Tests.Day1Tests;

[TestFixture]
public class Day1Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day1.Part1(input);
        Assert.That(result, Is.EqualTo(11));
    }
    
    [Test]
    public void Part2Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day1.Part2(input);
        Assert.That(result, Is.EqualTo(31));
    }
}