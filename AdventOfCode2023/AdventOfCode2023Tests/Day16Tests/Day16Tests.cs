using AdventOfCode2023.Day16;

namespace AdventOfCode2023Tests.Day16Tests;

[TestFixture]
public class Day16Tests
{
    [Test]
    public static void CountEnergizedTiles()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day16.CountEnergizedTiles(input);
        Assert.That(result, Is.EqualTo(46));
    }
    
    [Test]
    public static void MaxEnergizedTiles()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day16.MaxEnergizedTiles(input);
        Assert.That(result, Is.EqualTo(51));
    }
}