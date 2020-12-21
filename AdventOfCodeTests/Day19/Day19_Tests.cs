using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day19
{
    [TestFixture]
    public class Day19_Tests
    {

        [Test]
        public void Should_Calculate_Result_Easy()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19/Input/ExampleEasy.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day19.CountValidMessages(input);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Should_Calculate_Result()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day19.CountValidMessages(input);
            Assert.That(result, Is.EqualTo(2));
            
            /*
             * 4 1 5
             *
             * becomes
             *
             * "a" ("2 3" | "3 2") "b"
             *
             * becomes
             *
             * "a" ((("4 4" | "5 5") & ("4 5" | "5 4")) | (("4 5" | "5 4") & ("4 4" | "5 5"))) "b"
             *
             * becomes
             *
             * "a" ((("a a" | "b b") & ("a b" | "b a")) | (("a b" | "b a") & ("a a" | "b b"))) "b"
             *
             * becomes
             *
             * "a" ((("aa" | "bb") & ("ab" | "ba")) | (("ab" | "ba") & ("aa" | "bb"))) "b"
             *
             * matches
             * 
             * aaaabb
             * abbabb
             * aaabab
             * abbbab <-
             * aabaab
             * abaaab
             * aabbbb
             * ababbb <-
             *
             * ^a((aa|bb)(ab|ba)|(ab|ba)(aa|bb))b$
             * 
             */
                
        }

        [Test]
        public void Should_Calculate_Result_Part2()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19/Input/ExamplePart2.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day19.CountValidMessages(input);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Should_Calculate_Result_Part2_Hard()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19/Input/ExamplePart2Hard.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day19.CountValidMessages(input);
            Assert.That(result, Is.EqualTo(12));
        }
    }
}