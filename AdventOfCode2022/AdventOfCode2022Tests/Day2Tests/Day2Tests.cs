using AdventOfCode2022.Day2;

namespace AdventOfCode2022Tests.Day2Tests;

[TestFixture]
public class Day2Tests
{
    [Test]
    public void GetTotalRockPaperScissorsScore()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day2.GetTotalScore(input);
        Assert.That(result, Is.EqualTo(15));
    }
}