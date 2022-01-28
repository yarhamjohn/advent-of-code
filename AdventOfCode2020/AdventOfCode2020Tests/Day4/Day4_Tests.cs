using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day4
{
    [TestFixture]
    public class Day4Tests
    {
        [Test]
        public void Should_count_valid_passports()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day4.CountPassportsWithExpectedFields(input);
            Assert.That(result, Is.EqualTo(2));
        }
        
        [Test]
        public void Should_count_valid_passports_with_validation()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day4/Input/ExampleB.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day4.CountPassportsWithValidFields(input);
            Assert.That(result, Is.EqualTo(4));
        }
    }
}