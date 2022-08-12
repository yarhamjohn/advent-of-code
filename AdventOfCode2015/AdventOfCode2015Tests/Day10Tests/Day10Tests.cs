using AdventOfCode2015.Day10;

namespace AdventOfCode2015Tests.Day10Tests;

[TestFixture]
public class Day10Tests
{
    [Test]
    public void GetLength()
    {
        var result = Day10.GetLength("1", 5);
        Assert.That(result, Is.EqualTo(6));
    }
}