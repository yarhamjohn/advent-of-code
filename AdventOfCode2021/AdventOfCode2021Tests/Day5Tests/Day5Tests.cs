using AdventOfCode2021.Day5;

namespace AdventOfCode2021Tests.Day5Tests;

[TestFixture]
public class Day5Tests
{
    [Test]
    public void Should_calculate_dangerous_vents()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day5.CalculateDangerousVents(input);
        
        Assert.That(result, Is.EqualTo(5));
    }
    
    [Test]
    public void Should_calculate_dangerous_vents_with_diagonals()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day5.CalculateDangerousVentsWithDiagonals(input);
        
        Assert.That(result, Is.EqualTo(12));
    }
}