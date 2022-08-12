using AdventOfCode2015.Day12;

namespace AdventOfCode2015Tests.Day12Tests;

[TestFixture]
public class Day12Tests
{
    [TestCase("[1,2,3]", 6)]
    [TestCase("{\"a\":2,\"b\":4}", 6)]
    [TestCase("[[[3]]]", 3)]
    [TestCase("{\"a\":{\"b\":4},\"c\":-1}", 3)]
    [TestCase("{\"a\":[-1,1]}", 0)]
    [TestCase("[-1,{\"a\":1}]", 0)]
    [TestCase("[]", 0)]
    [TestCase("{}", 0)]
    [TestCase("[1,{\"c\":\"red\",\"b\":2},3]", 6)]
    [TestCase("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 15)]
    [TestCase("[1,\"red\",5]", 6)]
    public static void HasCorrectSum(string input, int expected)
    {
        var result = Day12.GetSum(input);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [TestCase("[1,2,3]", 6)]
    [TestCase("[1,{\"c\":\"red\",\"b\":2},3]", 4)]
    [TestCase("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 0)]
    [TestCase("[1,\"red\",5]", 6)]
    public static void HasCorrectSumWithoutRed(string input, int expected)
    {
        var result = Day12.GetSumWithoutRed(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}