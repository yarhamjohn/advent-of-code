using AdventOfCode2023.Day4;

namespace AdventOfCode2023Tests.Day4Tests;

[TestFixture]
public class Day4Tests
{
    [Test]
    public void CountCardPoint()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day4.CountCardPoints(input);
        Assert.That(result, Is.EqualTo(13));
    }
    
    [Test]
    public void CountCards()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day4.CountCards(input);
        Assert.That(result, Is.EqualTo(30));
    }
}