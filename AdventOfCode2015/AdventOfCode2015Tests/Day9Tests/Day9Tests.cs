using System.IO;
using System.Linq;
using AdventOfCode2015.Day9;

namespace AdventOfCode2015Tests.Day9Tests;

[TestFixture]
public class Day9Tests
{
    [Test]
    public void GetMinDistance()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day9.GetMinDistance(input.ToArray());
        Assert.That(result, Is.EqualTo(605));
    }
    
    [Test]
    public void GetMaxDistance()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day9.GetMaxDistance(input.ToArray());
        Assert.That(result, Is.EqualTo(982));
    }
}