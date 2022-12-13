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
    //
    // [TestCase("[1,1,3,1,1]","[1,1,5,1,1]", true)]
    // [TestCase("[[1],[2,3,4]]","[[1],4]", true)]
    // [TestCase("[9]","[[8,7,6]]", false)]
    // [TestCase("[[4,4],4,4]","[[4,4],4,4,4]", true)]
    // [TestCase("[7,7,7,7]","[7,7,7]", false)]
    // [TestCase("[]","[3]", true)]
    // [TestCase("[[[]]]","[[]]", false)]
    // [TestCase("[1,[2,[3,[4,[5,6,7]]]],8,9]","[1,[2,[3,[4,[5,6,0]]]],8,9]", false)]
    // public void CalculateDistance(string left, string right, bool expected)
    // {
    //     var result = Day13.CorrectlyOrdered((left, right));
    //     Assert.That(result, Is.EqualTo(expected));
    // }

    [TestCase("[]")]
    [TestCase("[1]")]
    [TestCase("[1,2]")]
    [TestCase("[[1],2]")]
    [TestCase("[[8,7,6]]")]
    [TestCase("[[4,4],4,4]")]
    public void Test(string input)
    {
        var result = Day13.Parse(input);
        Assert.Pass();
    }
}