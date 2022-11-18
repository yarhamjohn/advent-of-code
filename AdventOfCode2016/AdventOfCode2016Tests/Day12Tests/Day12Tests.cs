using System.IO;
using System.Linq;
using AdventOfCode2016.Day12;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day12Tests;

[TestFixture]
public class Day12Tests
{
    [Test]
    public void GetValueOfRegisterA()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();

        var result = Day12.GetValueOfRegister(input, "a");
        Assert.That(result, Is.EqualTo(42));
    }
}