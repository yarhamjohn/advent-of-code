using AdventOfCode2023.Day17;

namespace AdventOfCode2023Tests.Day17Tests;

[TestFixture]
public class Day17Tests
{
    [Test]
    public static void CountEnergizedTiles()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day17Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day17.CountEnergyLost(input);
        Assert.That(result, Is.EqualTo(102));
    }
}