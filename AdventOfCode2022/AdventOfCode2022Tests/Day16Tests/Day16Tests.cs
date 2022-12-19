using AdventOfCode2022.Day16;

namespace AdventOfCode2022Tests.Day16Tests;

[TestFixture]
public class Day16Tests
{
    [Test]
    public void CalculatePressureReleased()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day16.CalculatePressureReleased(input.ToArray());
        Assert.That(result, Is.EqualTo(1651));
    }
}