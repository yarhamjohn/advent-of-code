using AdventOfCode2022.Day9;

namespace AdventOfCode2022Tests.Day9Tests;

[TestFixture]
public class Day9Tests
{
    [Test]
    public void CalculatePositionsVisited()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day9.CalculatePositionsVisited(input.ToArray(), 2);
        Assert.That(result, Is.EqualTo(13));
    }
    
    [Test]
    public void CalculatePositionsVisitedMoreKnots()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day9.CalculatePositionsVisited(input.ToArray(), 10);
        Assert.That(result, Is.EqualTo(1));
    }
    
    [Test]
    public void CalculatePositionsVisitedMoreKnots2()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);

        var result = Day9.CalculatePositionsVisited(input.ToArray(), 10);
        Assert.That(result, Is.EqualTo(36));
    }
}