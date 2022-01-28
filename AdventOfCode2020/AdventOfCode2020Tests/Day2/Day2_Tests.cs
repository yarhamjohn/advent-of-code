using System.IO;
using NUnit.Framework;

namespace AdventOfCode2020Tests.Day2
{
    [TestFixture]
    public class Day2Tests
    {
        [Test]
        public void Should_count_valid_passwords_given_wrong_policy()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2/Input/Example.txt");
            var input = File.ReadAllLines(inputPath);
            var result = AdventOfCode2020.Day2.Day2.CountValidPasswordsWrongPolicy(input);
            Assert.That(result, Is.EqualTo(2));
        }
        
        [Test]
        public void Should_count_valid_passwords_given_correct_policy()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day2/Input/Example.txt");
            var input = File.ReadAllLines(inputPath);
            var result = AdventOfCode2020.Day2.Day2.CountValidPasswordsCorrectPolicy(input);
            Assert.That(result, Is.EqualTo(1));
        }
    }
}