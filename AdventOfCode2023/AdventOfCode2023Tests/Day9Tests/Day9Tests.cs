using AdventOfCode2023.Day9;

namespace AdventOfCode2023Tests.Day9Tests;

[TestFixture]
public class Day9Tests
{
    [Test]
    public void SumExtrapolatedValues()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day9.SumExtrapolatedValues(input);
        Assert.That(result, Is.EqualTo(114));
    }
    
    
    [Test]
    public void SumExtrapolatedValuesReverse()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day9.SumExtrapolatedValuesReverse(input);
        Assert.That(result, Is.EqualTo(2));
    }
}