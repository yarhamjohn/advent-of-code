using System.IO;
using System.Linq;
using AdventOfCode2016.Day4;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day4Tests;

[TestFixture]
public class Day4Tests
{
    [Test]
    public void GetSectorIdSum()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();

        var result = Day4.GetSectorIdSum(input);
        Assert.That(result, Is.EqualTo(1514));
    }
}