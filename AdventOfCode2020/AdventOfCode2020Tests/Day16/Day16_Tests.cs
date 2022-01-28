using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2020Tests.Day16
{
    [TestFixture]
    public class Day16_Tests
    {
        [Test]
        public void Should_Calculate_Scanning_Error_Rate()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day16.Day16.CalculateScanningErrorRate(input);
            Assert.That(result, Is.EqualTo(71));
        }
        
        [TestCase("class", 12)]
        [TestCase("row", 11)]
        [TestCase("seat", 13)]
        public void Should_Calculate_Correct_Fields(string rule, int target)
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day16/Input/Example2.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day16.Day16.CalculateDepartureFields(input, rule);
            Assert.That(result, Is.EqualTo(target));
        }
    }
}