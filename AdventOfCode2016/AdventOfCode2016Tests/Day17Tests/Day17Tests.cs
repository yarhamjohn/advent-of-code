using AdventOfCode2016.Day17;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day17Tests;

[TestFixture]
public class Day17Tests
{
    [TestCase("ihgpwlah", "DDRRRD")]
    [TestCase("kglvqrro", "DDUDRLRRUDRD")]
    [TestCase("ulqzkmiv", "DRURDRUDDLLDLUURRDULRLDUUDDDRR")]
    public void GetChecksum(string input, string expected)
    {
        var result = Day17.GetShortestPath(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}