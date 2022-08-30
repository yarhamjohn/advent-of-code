using System.IO;
using AdventOfCode2015.Day13;

namespace AdventOfCode2015Tests.Day13Tests;

[TestFixture]
public class Day13Tests
{
    [Test]
    public static void CalculatesOptimalHappiness()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day13.CalculateOptimalHappiness(input);
        
        Assert.That(result, Is.EqualTo(330));
    }
}