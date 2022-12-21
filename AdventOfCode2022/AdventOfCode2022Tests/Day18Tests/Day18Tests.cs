using AdventOfCode2022.Day18;

namespace AdventOfCode2022Tests.Day18Tests;

[TestFixture]
public class Day18Tests
{
    [Test]
    public void CalculateSurfaceArea()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day18Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day18.CalculateSurfaceArea(input);
        Assert.That(result, Is.EqualTo(10));
    }
    
    [Test]
    public void CalculateSurfaceArea2()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day18Tests/Input/example2.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day18.CalculateSurfaceArea(input);
        Assert.That(result, Is.EqualTo(64));
    }
    
    [Test]
    public void CalculateExternalSurfaceArea()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day18Tests/Input/example2.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day18.CalculateExternalSurfaceArea(input);
        Assert.That(result, Is.EqualTo(58));
    }
}