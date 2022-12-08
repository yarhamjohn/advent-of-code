using AdventOfCode2022.Day8;

namespace AdventOfCode2022Tests.Day8Tests;

[TestFixture]
public class Day8Tests
{
    [Test]
    public void GetFileSizes()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day8.CountVisibleTrees(input.ToArray());
        Assert.That(result, Is.EqualTo(21));
    }
}