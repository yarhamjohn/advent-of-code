using AdventOfCode.Day4;

namespace AdventOfCodeTests.Day4Tests;

[TestFixture]
public class Day4Tests
{
    [Test]
    public void Should_calculate_bingo_winner()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day4.CalculateBingoWinner(input);
        
        Assert.That(result, Is.EqualTo(4512));
    }
    
    [Test]
    public void Should_calculate_bingo_loser()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day4.CalculateBingoLoser(input);
        
        Assert.That(result, Is.EqualTo(1924));
    }
}