using AdventOfCode2016.Day5;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day5Tests;

[TestFixture]
public class Day5Tests
{
    [Test]
    public void GetPassword()
    {
        var result = Day5.GetPassword("abc");
        Assert.That(result, Is.EqualTo("18f47a30"));
    }
}