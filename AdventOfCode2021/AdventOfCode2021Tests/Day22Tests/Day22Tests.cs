using AdventOfCode2021.Day22;

namespace AdventOfCode2021Tests.Day22Tests;

[TestFixture]
public class Day22Tests
{
    [Test]
    public void Should_calculate_on_cubes_small()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day22Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day22.CalculateOnCubesSmall(input.ToArray());
        
        Assert.That(result, Is.EqualTo(39));
    }
    
    [Test]
    public void Should_calculate_on_cubes_big()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day22Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day22.CalculateOnCubesSmall(input.ToArray());
        
        Assert.That(result, Is.EqualTo(590784));
    }
    
    [Test]
    public void Should_calculate_on_cubes_full()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day22Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day22.CalculateOnCubes(input.ToArray());
        
        Assert.That(result, Is.EqualTo(2758514936282235));
    }
}