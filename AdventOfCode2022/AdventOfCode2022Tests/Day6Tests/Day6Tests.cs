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
        var result = Day6.GetStartOfPacket(input);
        Assert.That(result, Is.EqualTo(expected));
    }
}