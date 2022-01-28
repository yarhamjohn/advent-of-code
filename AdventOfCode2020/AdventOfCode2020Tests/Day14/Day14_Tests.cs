using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day14
{
    [TestFixture]
    public class Day14_Tests
    {
        [Test]
        public void Should_Sum_In_Memory_Values()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day14.SumInMemoryValues(input);
            Assert.That(result, Is.EqualTo(165));
        }
        [Test]
        
        public void Should_Sum_In_Memory_Values_Version_Two()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day14/Input/Example2.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day14.SumInMemoryValuesVersion2(input);
            Assert.That(result, Is.EqualTo(208));
        }
    }
}