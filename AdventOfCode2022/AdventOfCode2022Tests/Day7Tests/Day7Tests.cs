using AdventOfCode2022.Day7;

namespace AdventOfCode2022Tests.Day7Tests;

[TestFixture]
public class Day7Tests
{
    [Test]
    public void GetFileSizes()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day7.GetFileSizes(input.ToArray());
        Assert.That(result, Is.EqualTo(95437));
    }
    
    [Test]
    public void GetDirectoryToDelete()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day7.GetDirectoryToDelete(input.ToArray());
        Assert.That(result, Is.EqualTo(24933642));
    }
}