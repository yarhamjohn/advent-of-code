using AdventOfCode2016.Day13;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day13Tests;

[TestFixture]
public class Day13Tests
{

    [Test]
    public void GetMinimumSteps()
    {        
        var result = Day13.CountSteps((7, 4), 10);
        Assert.That(result, Is.EqualTo(11));
    }
}