using AdventOfCode2015.Day5;

namespace AdventOfCode2015Tests.Day5Tests;

[TestFixture]
public class Day5Tests
{
    [TestCase("ugknbfddgicrmopn", 1)]
    [TestCase("aaa", 1)]
    [TestCase("jchzalrnumimnmhp", 0)]
    [TestCase("haegwjzuvuyypxyu", 0)]
    [TestCase("dvszwmarrgswjxmb", 0)]
    public void GetNumNiceStrings(string input, int numNiceStrings)
    {
        var result = Day5.GetNumNiceStrings(new[] {input});
        Assert.That(result, Is.EqualTo(numNiceStrings));
    }
    
    [TestCase("qjhvhtzxzqqjkmpb", 1)]
    [TestCase("xxyxx", 1)]
    [TestCase("uurcxstgmygtbstg", 0)]
    [TestCase("ieodomkazucvgmuy", 0)]
    [TestCase("aaadefeghi", 0)]
    [TestCase("aaaadefeghi", 1)]
    public void GetNumNiceStrings2(string input, int numNiceStrings)
    {
        var result = Day5.GetNumNiceStrings2(new[] {input});
        Assert.That(result, Is.EqualTo(numNiceStrings));
    }
}