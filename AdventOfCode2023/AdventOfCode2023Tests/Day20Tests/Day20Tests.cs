using AdventOfCode2023.Day20;

namespace AdventOfCode2023Tests.Day20Tests;

[TestFixture]
public class Day20Tests
{
    [Test]
    public static void SumPartRatings()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day20Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day20.SumPulses(input);
        Assert.That(result, Is.EqualTo(32000000));
    }
    
    [Test]
    public static void SumPartRatingsSuper()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day20Tests/Input/example2.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day20.SumPulses(input);
        Assert.That(result, Is.EqualTo(11687500));
    }
}