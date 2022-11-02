using System.IO;
using System.Linq;
using AdventOfCode2016.Day2;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day2Tests;

[TestFixture]
public class Day2Tests
{
    [Test]
    public void GetBathroomCode()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();

        var result = Day2.GetBathroomCode(input);
        Assert.That(result, Is.EqualTo("1985"));
    }
    
    [Test]
    public void GetExtendedBathroomCode()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();

        var result = Day2.GetExtendedBathroomCode(input);
        Assert.That(result, Is.EqualTo("5DB3"));
    }
}