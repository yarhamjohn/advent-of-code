using AdventOfCode2022.Day21;

namespace AdventOfCode2022Tests.Day21Tests;

[TestFixture]
public class Day21Tests
{
    [Test]
    public void NumberYelledByRoot()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day21Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day21.NumberYelledByRoot(input);
        Assert.That(result, Is.EqualTo(152));
    }
    
    [Test]
    public void NumberYelledByRootPartTwo()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day21Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day21.NumberYelledByRootPartTwo(input);
        Assert.That(result, Is.EqualTo(301));
    }
}