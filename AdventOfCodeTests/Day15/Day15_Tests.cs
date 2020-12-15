using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day15
{
    [TestFixture]
    public class Day15_Tests
    {
        [TestCase("0,3,6", 436)]
        [TestCase("1,3,2", 1)]
        [TestCase("2,1,3", 10)]
        [TestCase("1,2,3", 27)]
        [TestCase("2,3,1", 78)]
        [TestCase("3,2,1", 438)]
        [TestCase("3,1,2", 1836)]
        public void Should_Find_2020th_Number(string input, int target)
        {
            var result = AdventOfCode.Day15.FindNumber(input, 2020);
            Assert.That(result, Is.EqualTo(target));
        }
        
        [TestCase("0,3,6", 175594)]
        [TestCase("1,3,2", 2578)]
        [TestCase("2,1,3", 3544142)]
        [TestCase("1,2,3", 261214)]
        [TestCase("2,3,1", 6895259)]
        [TestCase("3,2,1", 18)]
        [TestCase("3,1,2", 362)]
        public void Should_Find_30000000th_Number(string input, int target)
        {
            var result = AdventOfCode.Day15.FindNumber(input, 30000000);
            Assert.That(result, Is.EqualTo(target));
        }
    }
}