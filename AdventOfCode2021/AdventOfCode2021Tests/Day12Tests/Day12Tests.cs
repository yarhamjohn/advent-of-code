using AdventOfCode.Day12;

namespace AdventOfCodeTests.Day12Tests;

[TestFixture]
public class Day12Tests
{
    [Test]
    public void Should_calculate_number_of_paths_wip()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/wip.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPaths(input.ToArray());
        
        Assert.That(result, Is.EqualTo(1));
    }
    
    [Test]
    public void Should_calculate_number_of_paths_wip2()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/wip2.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPaths(input.ToArray());
        
        Assert.That(result, Is.EqualTo(1));
    }
    
    [Test]
    public void Should_calculate_number_of_paths_wip3()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/wip3.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPaths(input.ToArray());
        
        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void Should_calculate_number_of_paths_wip4()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/wip4.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPaths(input.ToArray());
        
        Assert.That(result, Is.EqualTo(4));
    }
    
    [Test]
    public void Should_calculate_number_of_paths_small()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPaths(input.ToArray());
        
        Assert.That(result, Is.EqualTo(10));
    }
    
    [Test]
    public void Should_calculate_number_of_paths_medium()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPaths(input.ToArray());
        
        Assert.That(result, Is.EqualTo(19));
    }
    
    [Test]
    public void Should_calculate_number_of_paths_large()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example3.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPaths(input.ToArray());
        
        Assert.That(result, Is.EqualTo(226));
    }
    
    [Test]
    public void Should_calculate_number_of_paths_small_with_double_visit()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPathsWithDoubleVisit(input.ToArray());
        
        Assert.That(result, Is.EqualTo(36));
    }
    
    [Test]
    public void Should_calculate_number_of_paths_medium_With_double_visit()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPathsWithDoubleVisit(input.ToArray());
        
        Assert.That(result, Is.EqualTo(103));
    }
    
    [Test]
    public void Should_calculate_number_of_paths_large_with_double_visit()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example3.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day12.CalculateNumberOfPathsWithDoubleVisit(input.ToArray());
        
        Assert.That(result, Is.EqualTo(3509));
    }
}