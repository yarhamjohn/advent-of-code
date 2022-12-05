using AdventOfCode2022.Day4;

namespace AdventOfCode2022Tests.Day4Tests;

[TestFixture]
public class Day4Tests
{
    [Test]
    public void GetContainedPairs()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day4.CountContainedPairs(input);
        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void GetOverlappingPairs()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day4.CountOverlappingPairs(input);
        Assert.That(result, Is.EqualTo(4));
    }
}