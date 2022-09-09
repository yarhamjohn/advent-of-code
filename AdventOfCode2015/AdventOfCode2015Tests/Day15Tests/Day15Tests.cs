using System.IO;
using AdventOfCode2015.Day15;

namespace AdventOfCode2015Tests.Day15Tests;

[TestFixture]
public class Day15Tests
{
    [Test]
    public static void GetsHighestScoringCookie()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day15Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day15.GetHighestScoringCookie(input);
        
        Assert.That(result, Is.EqualTo(62842880));
    }
    
    [Test]
    public static void GetsHighestScoringCookieWithCalories()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day15Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
    
        var result = Day15.GetHighestScoringCookieWithCalories(input);
        
        Assert.That(result, Is.EqualTo(57600000));
    }
}