using System.IO;
using System.Linq;
using AdventOfCode2016.Day4;
using NUnit.Framework;

namespace AdventOfCode2016Tests.Day4Tests;

[TestFixture]
public class Day4Tests
{
    [Test]
    public void GetSectorIdSum()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4Tests/Input/example.txt");
        var input = File.ReadLines(inputPath).ToArray();

        var result = Day4.GetSectorIdSum(input);
        Assert.That(result, Is.EqualTo(1514));
    }
    
    [Test]
    public void GetRoomSectorId()
    {
        var result = Day4.GetRoomSectorId(new [] {"qzmt-zixmtkozy-ivhz-343[zimth]"}, "very encrypted name");
        Assert.That(result, Is.EqualTo(343));
    }
}