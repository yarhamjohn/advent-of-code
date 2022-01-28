using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2020Tests.Day9
{
    [TestFixture]
    public class Day9_Tests
    {
        [Test]
        public void Should_Get_Invalid_Num()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day9.Day9.GetInvalidNum(input, 5);
            Assert.That(result, Is.EqualTo(127));
        }
        
        [Test]
        public void Should_Get_Weakness()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day9/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day9.Day9.GetWeakness(input, 5);
            Assert.That(result, Is.EqualTo(62));
        }
    }
}