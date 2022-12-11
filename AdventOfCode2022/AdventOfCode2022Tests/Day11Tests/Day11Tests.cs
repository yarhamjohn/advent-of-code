using AdventOfCode2022.Day11;

namespace AdventOfCode2022Tests.Day11Tests;

[TestFixture]
public class Day11Tests
{
    [Test]
    public void CalculateMonkeyBusiness()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day11Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day11.CalculateMonkeyBusiness(input.ToArray());
        Assert.That(result, Is.EqualTo(10605));
    }
}