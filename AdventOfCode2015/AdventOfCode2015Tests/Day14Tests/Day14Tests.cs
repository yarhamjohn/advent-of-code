using System.IO;
using AdventOfCode2015.Day14;

namespace AdventOfCode2015Tests.Day14Tests;

[TestFixture]
public class Day14Tests
{
    [Test]
    public static void GetsWinningDistance()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day14.GetWinningDistance(input, 1000);
        
        Assert.That(result, Is.EqualTo(1120));
    }
    
    [Test]
    public static void GetsWinningPoints()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day14.GetWinningPoints(input, 1000);
        
        Assert.That(result, Is.EqualTo(689));
    }
}