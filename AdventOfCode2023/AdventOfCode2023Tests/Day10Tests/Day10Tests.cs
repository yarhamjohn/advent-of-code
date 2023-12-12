using AdventOfCode2023.Day10;

namespace AdventOfCode2023Tests.Day10Tests;

[TestFixture]
public class Day10Tests
{
    [Test]
    public void CountSteps()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day10.CountSteps(input);
        Assert.That(result, Is.EqualTo(4));
    }
    
    [Test]
    public void CountStepsTwo()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example2.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day10.CountSteps(input);
        Assert.That(result, Is.EqualTo(8));
    }
    
    [Test]
    public void CountInternalSpaces()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example3.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day10.CountInternalSpaces(input);
        Assert.That(result, Is.EqualTo(4));
    }
    
    [Test]
    public void CountInternalSpacesTwo()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example4.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day10.CountInternalSpaces(input);
        Assert.That(result, Is.EqualTo(4));
    }
    
    [Test]
    public void CountInternalSpacesThree()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example5.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day10.CountInternalSpaces(input);
        Assert.That(result, Is.EqualTo(8));
    }
    
    [Test]
    public void CountInternalSpacesFour()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example6.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day10.CountInternalSpaces(input);
        Assert.That(result, Is.EqualTo(10));
    }
}