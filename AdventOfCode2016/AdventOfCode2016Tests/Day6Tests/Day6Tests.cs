using System.IO;
using System.Linq;
using AdventOfCode2016.Day6;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day6Tests;

[TestFixture]
public class Day6Tests
{
    [Test]
    public void GetMessage()
    {        
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day6Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();
        
        var result = Day6.GetMessage(input);
        Assert.That(result, Is.EqualTo("easter"));
    }
    
    [Test]
    public void GetMessageModified()
    {        
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day6Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();
        
        var result = Day6.GetMessageModified(input);
        Assert.That(result, Is.EqualTo("advent"));
    }
}