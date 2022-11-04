using System.IO;
using System.Linq;
using AdventOfCode2016.Day7;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day7Tests;

[TestFixture]
public class Day7Tests
{
    [Test]
    public void CountTlsIps()
    {        
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();
        
        var result = Day7.CountTlsIps(input);
        Assert.That(result, Is.EqualTo(2));
    }
    
    [Test]
    public void CountSslIps()
    {        
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath).ToArray();
        
        var result = Day7.CountSslIps(input);
        Assert.That(result, Is.EqualTo(3));
    }
}