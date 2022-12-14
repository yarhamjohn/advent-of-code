using AdventOfCode2022.Day13;

namespace AdventOfCode2022Tests.Day13Tests;

[TestFixture]
public class Day13Tests
{
    [Test]
    public void CalculateOrderedIndices()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day13.CalculateOrderedIndices(input.ToArray());
        Assert.That(result, Is.EqualTo(13));
    }
    
    [TestCase("[]")]
    [TestCase("[1]")]
    [TestCase("[1,2]")]
    [TestCase("[[1],2]")]
    [TestCase("[[8,7,6]]")]
    [TestCase("[[4,4],4,4]")]
    [TestCase("[[],[1],2,[3,4],[[5,6],7]]")]
    [TestCase("[[]]")]
    [TestCase("[[[]]]")]
    [TestCase("[[],[]]")]
    [TestCase("[[[1]]]")]
    public void Test(string input)
    {
        var result = Day13.Parse(input);
        Assert.Pass();
    }
}