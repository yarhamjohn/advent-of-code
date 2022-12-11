using AdventOfCode2022.Day10;

namespace AdventOfCode2022Tests.Day10Tests;

[TestFixture]
public class Day10Tests
{
    [Test]
    public void CalculatePositionsVisited()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day10.CalculateSignalStrength(input.ToArray());
        Assert.That(result, Is.EqualTo(13140));
    }
}