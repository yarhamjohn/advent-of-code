using System.IO;
using System.Linq;
using AdventOfCode2015.Day7;

namespace AdventOfCode2015Tests.Day7Tests;

[TestFixture]
public class Day7Tests
{
    [TestCase("d", 72)]
    [TestCase("e", 507)]
    [TestCase("f", 492)]
    [TestCase("g", 114)]
    [TestCase("h", 65412)]
    [TestCase("i", 65079)]
    [TestCase("x", 123)]
    [TestCase("y", 456)]
    public void GetWireSignal(string wire, int signal)
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day7.GetWireSignal(wire, input.ToArray());
        Assert.That(result, Is.EqualTo(signal));
    }
}