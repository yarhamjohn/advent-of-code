using AdventOfCode2021.Day17;

namespace AdventOfCode2021Tests.Day17Tests;

[TestFixture]
public class Day17Tests
{
    [Test]
    public void Should_calculate_maximum_y_velocity()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day17Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day17.CalculateMaximumY(input);
        
        Assert.That(result, Is.EqualTo(45));
    }
    
    [Test]
    public void Should_calculate_unique_velocities()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day17Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day17.CalculateAllVelocities(input);
        
        Assert.That(result, Is.EqualTo(112));
    }
}