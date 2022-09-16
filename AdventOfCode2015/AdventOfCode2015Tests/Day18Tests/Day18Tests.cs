using System.IO;
using AdventOfCode2015.Day18;

namespace AdventOfCode2015Tests.Day18Tests;

[TestFixture]
public class Day18Tests
{
    [Test]
    public static void GetCorrectNumberOfLights()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day18Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day18.CountLights(input, 4);
        
        Assert.That(result, Is.EqualTo(4));
    }
    
    [Test]
    public static void GetCorrectNumberOfLightsWhenStuckOn()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day18Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day18.CountLightsWithStuckOn(input, 5);
        
        Assert.That(result, Is.EqualTo(17));
    }
}