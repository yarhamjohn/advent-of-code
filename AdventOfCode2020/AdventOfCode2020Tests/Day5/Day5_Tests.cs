using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2020Tests.Day5
{
    [TestFixture]
    public class Day5_Tests
    {
        [Test]
        public void Should_Get_Highest_Seat_Id()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day5.Day5.GetHighestSeatId(input);
            Assert.That(result, Is.EqualTo(820));
        }
    }
}