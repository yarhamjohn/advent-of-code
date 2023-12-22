using AdventOfCode2023.Day14;

namespace AdventOfCode2023Tests.Day14Tests;

[TestFixture]
public class Day14Tests
{
    [Test]
    public static void SumRockLoad()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day14.SumRockLoad(input);
        Assert.That(result, Is.EqualTo(136));
    }
    
    [Test]
    public static void SumRockLoadCycles()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day14.SumRockLoadCycles(input);
        Assert.That(result, Is.EqualTo(64));
    }
}