using AdventOfCode2016.Day17;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day17Tests;

[TestFixture]
public class Day17Tests
{
    [TestCase("ihgpwlah", "DDRRRD")]
    [TestCase("kglvqrro", "DDUDRLRRUDRD")]
    [TestCase("ulqzkmiv", "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
    public void GetShortestPath(string input, string expected)
    {
        var result = Day17.GetShortestPath(input);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [TestCase("ihgpwlah", 370)]
    [TestCase("kglvqrro", 492)]
    [TestCase("ulqzkmiv", 830)]
    public void GetLongestPath(string input, int expected)
    {
        var result = Day17.GetLongestPath(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}