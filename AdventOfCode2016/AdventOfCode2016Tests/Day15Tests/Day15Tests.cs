using System.IO;
using System.Linq;
using AdventOfCode2016.Day15;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day15Tests;

[TestFixture]
public class Day15Tests
{
    [Test]
    public void GetEarliestTime()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day15Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();

        var result = Day15.GetEarliestTime(input);
        Assert.That(result, Is.EqualTo(5));
    }
}