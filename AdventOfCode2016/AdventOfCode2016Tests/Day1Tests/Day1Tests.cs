using AdventOfCode2016.Day1;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day1Tests;

[TestFixture]
public class Day1Tests
{
    [TestCase("R2, L3", 5)]
    [TestCase("R2, R2, R2", 2)]
    [TestCase("R5, L5, R5, R3", 12)]
    public void GetDistanceInBlocks(string input, int expectedDistance)
    {
        var result = Day1.GetBlockCount(input);
        Assert.That(result, Is.EqualTo(expectedDistance));
    }
    
    
    [TestCase("R8, R4, R4, R8", 4)]
    public void GetDistanceInBlocks_ToFirstDoubleVisitedBlock(string input, int expectedDistance)
    {
        var result = Day1.GetBlockCountToDoubleVisitedBlock(input);
        Assert.That(result, Is.EqualTo(expectedDistance));
    }
}