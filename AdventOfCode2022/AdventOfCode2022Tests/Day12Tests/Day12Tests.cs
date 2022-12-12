using AdventOfCode2022.Day12;

namespace AdventOfCode2022Tests.Day12Tests;

[TestFixture]
public class Day12Tests
{
    [Test]
    public void CalculateDistance()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day12.CountFewestSteps(input.ToArray());
        Assert.That(result, Is.EqualTo(31));
    }
    
    [Test]
    public void CalculateShortestWalk()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day12.CountShortestWalk(input.ToArray());
        Assert.That(result, Is.EqualTo(29));
    }
}