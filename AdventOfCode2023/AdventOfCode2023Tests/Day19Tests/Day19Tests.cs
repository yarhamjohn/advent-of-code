using AdventOfCode2023.Day19;

namespace AdventOfCode2023Tests.Day19Tests;

[TestFixture]
public class Day19Tests
{
    [Test]
    public static void SumPartRatings()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day19.SumPartRatings(input);
        Assert.That(result, Is.EqualTo(19114));
    }
    
    [Test]
    public static void SumPartRatingsSuper()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day19.SumPartRatingsSuper(input);
        Assert.That(result, Is.EqualTo(167409079868000));
    }
}