using System.IO;
using System.Linq;
using AdventOfCode2016.Day8;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day8Tests;

[TestFixture]
public class Day8Tests
{
    [Test]
    public void CountTlsIps()
    {        
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();
        
        var result = Day8.CountLitPixels(input, 3, 7);
        Assert.That(result, Is.EqualTo(6));
    }
    
    [Test]
    public void GetCode()
    {        
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();
        
        Day8.PrintCode(input, 3, 7);
    }
}