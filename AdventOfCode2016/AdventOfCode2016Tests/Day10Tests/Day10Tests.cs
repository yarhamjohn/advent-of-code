using System.IO;
using System.Linq;
using AdventOfCode2016.Day10;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day10Tests;

[TestFixture]
public class Day10Tests
{
    [Test]
    public void GetTargetBot()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();

        var result = Day10.GetComparerBotNumber(input, 5, 2);
        Assert.That(result, Is.EqualTo(2));
    }
}