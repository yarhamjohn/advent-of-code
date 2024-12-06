using AdventOfCode2024.Day4;

namespace AdventOfCode2024Tests.Day4Tests;

[TestFixture]
public class Day4Tests
{
    [Test]
    public void Part1Test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day4.Part1(input.ToArray());
        Assert.That(result, Is.EqualTo(18));
    }
}