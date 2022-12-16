using AdventOfCode2022.Day14;

namespace AdventOfCode2022Tests.Day14Tests;

[TestFixture]
public class Day14Tests
{
    [Test]
    public void CalculateUnitsOfSand()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day14.CalculateUnitsOfSand(input.ToArray());
        Assert.That(result, Is.EqualTo(24));
    }
    
    [Test]
    public void CalculateTotalUnitsOfSand()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day14.CalculateTotalUnitsOfSand(input.ToArray());
        Assert.That(result, Is.EqualTo(93));
    }
}