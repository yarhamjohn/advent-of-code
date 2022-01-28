using AdventOfCode2021.Day24;

namespace AdventOfCode2021Tests.Day24Tests;

[TestFixture]
public class Day24Tests
{
    [Test]
    public void Should_calculate_basic_alu()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day24Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day24.CalculateLargestModelNumbers(input.ToArray());
        
        Assert.That(result, Is.True);
    }
}