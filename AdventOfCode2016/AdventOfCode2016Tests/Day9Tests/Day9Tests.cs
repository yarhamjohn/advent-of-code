using System.IO;
using System.Linq;
using AdventOfCode2016.Day8;
using AdventOfCode2016.Day9;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day9Tests;

[TestFixture]
public class Day9Tests
{
    [TestCase("ADVENT", 6)]
    [TestCase("A(1x5)BC", 7)]
    [TestCase("A(2x2)BCD(2x2)EFG", 11)]
    [TestCase("(6x1)(1x3)A", 6)]
    [TestCase("X(8x2)(3x3)ABCY", 18)]
    public void GetDecompressedLength(string input, int expectedLength)
    {        
        var result = Day9.GetDecompressedLength(input);
        Assert.That(result, Is.EqualTo(expectedLength));
    }
}