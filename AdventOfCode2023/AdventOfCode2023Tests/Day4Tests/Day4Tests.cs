using AdventOfCode2023.Day4;

namespace AdventOfCode2023Tests.Day4Tests;

[TestFixture]
public class Day3Tests
{
    [Test]
    public void SumPartNumbers()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day4.CountCardPoints(input);
        Assert.That(result, Is.EqualTo(13));
    }
}