using AdventOfCode2023.Day13;

namespace AdventOfCode2023Tests.Day13Tests;

[TestFixture]
public class Day13Tests
{
    [Test]
    public static void ReflectionSum()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day13.ReflectionSum(input);
        Assert.That(result, Is.EqualTo(405));
    }
    
    [Test]
    public static void ReflectionSmudgeSum()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day13.ReflectionSmudgeSum(input);
        Assert.That(result, Is.EqualTo(400));
    }
    
    [Test]
    public static void ReflectionSmudgeSum2()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13Tests/Input/example2.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day13.ReflectionSmudgeSum(input);
        Assert.That(result, Is.EqualTo(900));
    }
}