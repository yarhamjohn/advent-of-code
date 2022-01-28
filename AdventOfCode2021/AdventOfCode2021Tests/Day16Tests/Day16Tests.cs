using AdventOfCode.Day16;

namespace AdventOfCodeTests.Day16Tests;

[TestFixture]
public class Day16Tests
{
    [Test]
    public void Should_calculate_version_from_literal()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateVersionSum(input);
        
        Assert.That(result, Is.EqualTo(6));
    }
    
    [Test]
    public void Should_calculate_version_from_operator_length()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example2.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateVersionSum(input);
        
        Assert.That(result, Is.EqualTo(9));
    }
    
    [Test]
    public void Should_calculate_version_from_operator_num()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example3.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateVersionSum(input);
        
        Assert.That(result, Is.EqualTo(14));
    }
    
    [Test]
    public void Should_calculate_version_one()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example4.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateVersionSum(input);
        
        Assert.That(result, Is.EqualTo(16));
    }
    
    [Test]
    public void Should_calculate_version_two()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example5.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateVersionSum(input);
        
        Assert.That(result, Is.EqualTo(12));
    }
    
    [Test]
    public void Should_calculate_version_three()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example6.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateVersionSum(input);
        
        Assert.That(result, Is.EqualTo(23));
    }
    
    [Test]
    public void Should_calculate_version_four()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example7.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateVersionSum(input);
        
        Assert.That(result, Is.EqualTo(31));
    }
    
    [Test]
    public void Should_calculate_version_complex_one()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example8.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateValue(input);
        
        Assert.That(result, Is.EqualTo(3));
    }
    
    [Test]
    public void Should_calculate_version_complex_two()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example9.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateValue(input);
        
        Assert.That(result, Is.EqualTo(54));
    }
    
    [Test]
    public void Should_calculate_version_complex_three()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example10.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateValue(input);
        
        Assert.That(result, Is.EqualTo(7));
    }
    
    [Test]
    public void Should_calculate_version_complex_four()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example11.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateValue(input);
        
        Assert.That(result, Is.EqualTo(9));
    }
    
    [Test]
    public void Should_calculate_version_complex_five()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example12.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateValue(input);
        
        Assert.That(result, Is.EqualTo(1));
    }
    
    [Test]
    public void Should_calculate_version_complex_six()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example13.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateValue(input);
        
        Assert.That(result, Is.EqualTo(0));
    }
    
    [Test]
    public void Should_calculate_version_complex_seven()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example14.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateValue(input);
        
        Assert.That(result, Is.EqualTo(0));
    }
    
    [Test]
    public void Should_calculate_version_complex_eight()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example15.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateValue(input);
        
        Assert.That(result, Is.EqualTo(1));
    }
    
    [Test]
    public void Should_calculate_version_complex_nine()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16Tests/Input/example16.txt");
        var input = File.ReadAllText(inputPath);
        
        var result = Day16.CalculateValue(input);
        
        Assert.That(result, Is.EqualTo(110));
    }
}