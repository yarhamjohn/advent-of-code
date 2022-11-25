using AdventOfCode2016.Day14;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day14Tests;

[TestFixture]
public class Day14Tests
{

    [Test]
    public void Get64thKeyIndex()
    {
        var result = Day14.Get64thKeyIndex("abc");
        Assert.That(result, Is.EqualTo(22728));
    }
}