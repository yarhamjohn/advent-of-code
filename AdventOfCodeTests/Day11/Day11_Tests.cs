using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day11
{
    [TestFixture]
    public class Day11_Tests
    {
        [Test]
        public void Should_Count_Empty_Seats()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day11/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day11.CountEmptySeats(input);
            Assert.That(result, Is.EqualTo(37));
        }
        
        [Test]
        public void Should_Count_Empty_Seats_New_Rules()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day11/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day11.CountEmptySeatsNewRules(input);
            Assert.That(result, Is.EqualTo(26));
        }
    }
}