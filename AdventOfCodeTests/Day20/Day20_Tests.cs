using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day20
{
    [TestFixture]
    public class Day20_Tests
    {
        [Test]
        public void Should_Calculate_Result_Easy()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day20/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day20.SumCorners(input);
            Assert.That(result, Is.EqualTo(20899048083289));
        }
    }
}