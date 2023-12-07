using AdventOfCode2023.Day7;

namespace AdventOfCode2023Tests.Day7Tests;

[TestFixture]
public class Day7Tests
{
    [Test]
    public void CalculateTotalWinnings()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day7.CalculateTotalWinnings(input);
        Assert.That(result, Is.EqualTo(6440));
    }
}