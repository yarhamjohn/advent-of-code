using AdventOfCode2023.Day2;

namespace AdventOfCode2023Tests.Day2Tests;

[TestFixture]
public class Day2Tests
{
    [Test]
    public void GetGameIdSum()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day2.GetGameIdSum(input);
        Assert.That(result, Is.EqualTo(8));
    }
    
    [Test]
    public void GetPowerCubeSum()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day2.GetPowerCubeSum(input);
        Assert.That(result, Is.EqualTo(2286));
    }
}