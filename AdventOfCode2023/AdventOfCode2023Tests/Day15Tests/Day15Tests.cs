using AdventOfCode2023.Day15;

namespace AdventOfCode2023Tests.Day15Tests;

[TestFixture]
public class Day15Tests
{
    [Test]
    public static void SumHashAlgorithm()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day15Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);

        var result = Day15.SumHashAlgorithm(input);
        Assert.That(result, Is.EqualTo(1320));
    }
    
    [Test]
    public static void SumHashAlgorithm2()
    {
        var result = Day15.SumHashAlgorithm("HASH");
        Assert.That(result, Is.EqualTo(52));
    }
    
    [Test]
    public static void CalculateFocusingPower()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day15Tests/Input/example.txt");
        var input = File.ReadAllText(inputPath);

        var result = Day15.CalculateFocusingPower(input);
        Assert.That(result, Is.EqualTo(145));
    }
}