using AdventOfCode2023.Day6;

namespace AdventOfCode2023Tests.Day6Tests;

[TestFixture]
public class Day6Tests
{
    [Test]
    public void GetRecords()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day6Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day6.GetRecords(input);
        Assert.That(result, Is.EqualTo(288));
    }
    
    [Test]
    public void GetRecordsBig()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day6Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);
        
        var result = Day6.GetRecordsBig(input);
        Assert.That(result, Is.EqualTo(71503));
    }
}