using System;
using System.Diagnostics;
using AdventOfCode2021.Day23;

namespace AdventOfCode2021Tests.Day23Tests;

[TestFixture]
public class Day23Tests
{
    [Test]
    public void Should_calculate_lowest_energy()
    {
        var watch = new Stopwatch();
        watch.Start();
        
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day23Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day23.CalculateLowestEnergy(input.ToArray());
        
        watch.Stop();
        Console.WriteLine(watch.ElapsedMilliseconds);
        
        Assert.That(result, Is.EqualTo(12521));
    }
    
    [Test]
    public void Should_calculate_lowest_energy2()
    {
        var watch = new Stopwatch();
        watch.Start();
        
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day23Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day23.CalculateLowestEnergy(input.ToArray());
        
        
        watch.Stop();
        Console.WriteLine(watch.ElapsedMilliseconds);
        
        Assert.That(result, Is.EqualTo(12521));
    }
    
    [Test]
    public void Should_calculate_lowest_energy3()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day23Tests/Input/example3.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day23.CalculateLowestEnergy(input.ToArray());
        
        Assert.That(result, Is.EqualTo(8470));
    }
}