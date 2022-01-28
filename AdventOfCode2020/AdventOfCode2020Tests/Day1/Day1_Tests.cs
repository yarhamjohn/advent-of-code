using System.IO;
using NUnit.Framework;

namespace AdventOfCodeTests.Day1
{
    [TestFixture]
    public class Day1Tests
    {
        [Test]
        public void Should_calculate_total_of_two_numbers_adding_to_2020()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1/Input/Example.txt");
            var input = File.ReadAllLines(inputPath);
            var result = AdventOfCode.Day1.CalculateTotalForTwoNumbers(input, 2020);
            Assert.That(result, Is.EqualTo(514579));
        }
        
        [Test]
        public void Should_calculate_total_of_three_numbers_adding_to_2020()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1/Input/Example.txt");
            var input = File.ReadAllLines(inputPath);
            var result = AdventOfCode.Day1.CalculateTotalForThreeNumbers(input);
            Assert.That(result, Is.EqualTo(241861950));
        }
    }
}