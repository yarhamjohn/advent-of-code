using AdventOfCode2016.Day16;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day16Tests;

[TestFixture]
public class Day16Tests
{
    [Test]
    public void GetChecksum()
    {
        var result = Day16.GetChecksum("10000", 20);
        Assert.That(result, Is.EqualTo("01100"));
    }
}