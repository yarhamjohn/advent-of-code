using System.IO;
using AdventOfCode2015.Day17;

namespace AdventOfCode2015Tests.Day17Tests;

[TestFixture]
public class Day17Tests
{
    [Test]
    public static void GetsContainerCombinations()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day17Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day17.GetContainerCombinations(input, 25);
        
        Assert.That(result, Is.EqualTo(4));
    }
    
    [Test]
    public static void GetsNumWays()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day17Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day17.GetNumberOfWays(input, 25);
        
        Assert.That(result, Is.EqualTo(3));
    }
}