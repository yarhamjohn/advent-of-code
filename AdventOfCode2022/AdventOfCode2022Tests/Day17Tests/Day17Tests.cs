using AdventOfCode2022.Day17;

namespace AdventOfCode2022Tests.Day17Tests;

[TestFixture]
public class Day17Tests
{
    [Test]
    public void CalculateHeight()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day17Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);

        var result = Day17.CalculateHeight(input);
        Assert.That(result, Is.EqualTo(3068));
    }
}