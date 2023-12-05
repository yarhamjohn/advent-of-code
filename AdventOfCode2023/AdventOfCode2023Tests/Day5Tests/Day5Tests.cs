using AdventOfCode2023.Day5;

namespace AdventOfCode2023Tests.Day5Tests;

[TestFixture]
public class Day5Tests
{
    [Test]
    public void GetLowestLocationNumber()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day5.GetLowestLocationNumber(input);
        Assert.That(result, Is.EqualTo(35));
    }
    
    [Test]
    public void GetLowestLocationNumberRange()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day5.GetLowestLocationNumberRange(input);
        Assert.That(result, Is.EqualTo(46));
    }
}