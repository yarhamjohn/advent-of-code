using AdventOfCode2021.Day13;

namespace AdventOfCode2021Tests.Day13Tests;

[TestFixture]
public class Day13Tests
{
    [Test]
    public void Should_calculate_number_of_dots_first_fold()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day13.CalculateFirstFold(input.ToArray());
        
        Assert.That(result, Is.EqualTo(17));
    }
    
    [Test]
    public void Should_calculate_number_of_dots_all_folds()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day13.CalculateAllFolds(input.ToArray());
        
        Assert.That(result, Is.EqualTo(16));
    }
}