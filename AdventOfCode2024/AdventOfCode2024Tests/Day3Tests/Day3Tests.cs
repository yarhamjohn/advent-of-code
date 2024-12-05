using AdventOfCode2024.Day3;

namespace AdventOfCode2024Tests.Day3Tests;

[TestFixture]
public class Day3Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day3.Part1(input);
        Assert.That(result, Is.EqualTo(161));
    }
    
    [Test]
    public void Part2Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day3.Part2(input);
        Assert.That(result, Is.EqualTo(48));
    }
}