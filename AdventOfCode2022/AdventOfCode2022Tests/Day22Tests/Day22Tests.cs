using AdventOfCode2022.Day22;

namespace AdventOfCode2022Tests.Day22Tests;

[TestFixture]
public class Day22Tests
{
    [Test]
    public void CalculatePassword()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day22Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day22.CalculatePassword(input);
        Assert.That(result, Is.EqualTo(6032));
    }
}