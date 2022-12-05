using AdventOfCode2022.Day3;

namespace AdventOfCode2022Tests.Day3Tests;

[TestFixture]
public class Day3Tests
{
    [Test]
    public void GetPrioritySum()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day3.GetPrioritySum(input);
        Assert.That(result, Is.EqualTo(157));
    }
    
    [Test]
    public void GetBadgePrioritySum()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day3.GetBadgePrioritySum(input.ToArray());
        Assert.That(result, Is.EqualTo(70));
    }
}