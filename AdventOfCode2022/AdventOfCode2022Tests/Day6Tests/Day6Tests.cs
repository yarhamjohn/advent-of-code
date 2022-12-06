using AdventOfCode2022.Day6;

namespace AdventOfCode2022Tests.Day6Tests;

[TestFixture]
public class Day6Tests
{
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    public void GetStartOfPacket(string input, int expected)
    {
        var result = Day6.GetStartOfSection(input, 4);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    public void GetStartOfMessage(string input, int expected)
    {
        var result = Day6.GetStartOfSection(input, 14);
        Assert.That(result, Is.EqualTo(expected));
    }
}