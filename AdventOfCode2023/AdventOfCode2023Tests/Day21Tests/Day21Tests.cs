using AdventOfCode2023.Day21;

namespace AdventOfCode2023Tests.Day21Tests;

[TestFixture]
public class Day21Tests
{
    [Test]
    public static void CountGardenPlots()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day21Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day21.CountGardenPlots(input, 6);
        Assert.That(result, Is.EqualTo(16));
    }
    
    [TestCase(6, 16)]
    [TestCase(10, 50)]
    [TestCase(50, 1594)]
    [TestCase(100, 6536)]
    [TestCase(500, 167004)]
    [TestCase(1000, 668697)]
    [TestCase(5000, 16733044)]
    public static void CountGardenPlots(int steps, int expected)
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day21Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day21.CountGardenPlotsBig(input, steps);
        Assert.That(result, Is.EqualTo(expected));
    }
}