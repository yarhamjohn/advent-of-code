using AdventOfCode2022.Day9;

namespace AdventOfCode2022Tests.Day9Tests;

[TestFixture]
public class Day9Tests
{
    [Test]
    public void CountVisibleTrees()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day9.CalculatePositionsVisited(input.ToArray());
        Assert.That(result, Is.EqualTo(13));
    }
}