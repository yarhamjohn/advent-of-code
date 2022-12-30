using AdventOfCode2022.Day25;

namespace AdventOfCode2022Tests.Day25Tests;

[TestFixture]
public class Day25Tests
{
    [Test]
    public void CountEmptyGroundTiles()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day25Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day25.CalculateSnafuNumber(input);
        Assert.That(result, Is.EqualTo("2=-1=0"));
    }
    
    [TestCase(1, "1")]
    [TestCase(2, "2")]
    [TestCase(3, "1=")]
    [TestCase(4, "1-")]
    [TestCase(5, "10")]
    [TestCase(6, "11")]
    [TestCase(7, "12")]
    [TestCase(8, "2=")]
    [TestCase(9, "2-")]
    [TestCase(10, "20")]
    [TestCase(11, "21")]
    [TestCase(12, "22")]
    [TestCase(13, "1==")]
    [TestCase(14, "1=-")]
    [TestCase(15, "1=0")]
    [TestCase(20, "1-0")]
    [TestCase(100, "1-00")]
    [TestCase(120, "10-0")]
    [TestCase(125, "1000")]
    [TestCase(978, "2=-1=")]
    [TestCase(2022, "1=11-2")]
    [TestCase(12345, "1-0---0")]
    [TestCase(314159265, "1121-1110-1=0")]
    public void CalculateSnafu(int input, string expected)
    {
        var result = Day25.CalculateSnafu(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}