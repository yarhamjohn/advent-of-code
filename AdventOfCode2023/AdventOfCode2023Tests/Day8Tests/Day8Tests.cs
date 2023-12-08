using AdventOfCode2023.Day8;

namespace AdventOfCode2023Tests.Day8Tests;

[TestFixture]
public class Day8Tests
{
    [Test]
    public void CalculateNumStepsOne()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day8.CalculateNumSteps(input);
        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void CalculateNumStepsTwo()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example2.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day8.CalculateNumSteps(input);
        Assert.That(result, Is.EqualTo(6));
    }
    
    [Test]
    public void CalculateNumStepsGhosts()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example3.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day8.CalculateNumStepsGhosts(input);
        Assert.That(result, Is.EqualTo(6));
    }
}