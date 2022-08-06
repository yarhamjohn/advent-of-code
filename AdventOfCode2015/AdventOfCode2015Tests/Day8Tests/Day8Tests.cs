using System.IO;
using System.Linq;
using AdventOfCode2015.Day8;

namespace AdventOfCode2015Tests.Day8Tests;

[TestFixture]
public class Day8Tests
{
    [Test]
    public void GetCharacters()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day8.CalculateCharacters(input.ToArray());
        Assert.That(result, Is.EqualTo(12));
    }
    
    [Test]
    public void GetCharacters2()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day8.CalculateCharacters2(input.ToArray());
        Assert.That(result, Is.EqualTo(19));
    }
}