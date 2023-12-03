using AdventOfCode2023.Day3;

namespace AdventOfCode2023Tests.Day3Tests;

[TestFixture]
public class Day3Tests
{
    [Test]
    public void SumPartNumbers()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day3.SumPartNumbers(input);
        Assert.That(result, Is.EqualTo(4361));
    }
    
    [Test]
    public void SumPartNumbers2()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day3.SumPartNumbers(input);
        Assert.That(result, Is.EqualTo(15386));
    }
}