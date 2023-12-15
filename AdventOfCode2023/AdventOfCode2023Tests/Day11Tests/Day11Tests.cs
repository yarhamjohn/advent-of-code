using AdventOfCode2023.Day11;

namespace AdventOfCode2023Tests.Day11Tests;

[TestFixture]
public class Day11Tests
{
    [Test]
    public void SumExtrapolatedValues()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day11Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day11.SumPathLengths(input, 2);
        Assert.That(result, Is.EqualTo(374));
    }
    
    [Test]
    public void SumExtrapolatedValuesBig()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day11Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day11.SumPathLengths(input, 10);
        Assert.That(result, Is.EqualTo(1030));
    }
    
    [Test]
    public void SumExtrapolatedValuesBig2()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day11Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day11.SumPathLengths(input, 100);
        Assert.That(result, Is.EqualTo(8410));
    }
}