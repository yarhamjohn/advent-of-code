using AdventOfCode2023.Day18;

namespace AdventOfCode2023Tests.Day18Tests;

[TestFixture]
public class Day18Tests
{
    [Test]
    public static void CalculateLagoonSize()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day18Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day18.CalculateLagoonSize(input);
        Assert.That(result, Is.EqualTo(62));
    }
}