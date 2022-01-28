using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day6
{
    [TestFixture]
    public class Day6_Tests
    {
        [Test]
        public void Should_Calculate_Sum_Of_All_Questions_Answered_Yes()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day6/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day6.CalculateQuestionSumAllQuestions(input);
            Assert.That(result, Is.EqualTo(11));
        }
        
        [Test]
        public void Should_Calculate_Sum_Of_All_Questions_Everyone_Answered_Yes()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day6/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day6.CalculateQuestionSumSameQuestions(input);
            Assert.That(result, Is.EqualTo(6));
        }
    }
}