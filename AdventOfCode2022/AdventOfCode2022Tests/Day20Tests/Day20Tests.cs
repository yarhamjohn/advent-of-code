using AdventOfCode2022.Day20;

namespace AdventOfCode2022Tests.Day20Tests;

[TestFixture]
public class Day20Tests
{
    [Test]
    public void CalculateGroveCoordinates()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day20Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day20.CalculateGroveCoordinates(input);
        Assert.That(result, Is.EqualTo(3));
    }
}