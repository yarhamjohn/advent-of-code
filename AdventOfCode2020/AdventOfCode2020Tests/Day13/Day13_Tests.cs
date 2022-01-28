using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day13
{
    [TestFixture]
    public class Day13_Tests
    {
        [Test]
        public void Should_Calculate_Bus_Id()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day13.CalculateBusId(input);
            Assert.That(result, Is.EqualTo(295));
        }
        
        [Test]
        public void Should_Calculate_Timestamp()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13/Input/Example1.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day13.CalculateTimestamp(input);
            Assert.That(result, Is.EqualTo(1068781));
        }
        
        [Test]
        public void Should_Calculate_Timestamp2()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13/Input/Example2.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day13.CalculateTimestamp(input);
            Assert.That(result, Is.EqualTo(3417));
        }
        
        [Test]
        public void Should_Calculate_Timestamp3()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13/Input/Example3.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day13.CalculateTimestamp(input);
            Assert.That(result, Is.EqualTo(754018));
        }
        
        [Test]
        public void Should_Calculate_Timestamp4()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13/Input/Example4.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day13.CalculateTimestamp(input);
            Assert.That(result, Is.EqualTo(779210));
        }
        
        [Test]
        public void Should_Calculate_Timestamp5()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13/Input/Example5.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day13.CalculateTimestamp(input);
            Assert.That(result, Is.EqualTo(1261476));
        }
        
        [Test]
        public void Should_Calculate_Timestamp6()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13/Input/Example6.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day13.CalculateTimestamp(input);
            Assert.That(result, Is.EqualTo(1202161486));
        }
        
        [Test]
        public void Should_Calculate_Timestamp7()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day13/Input/Example7.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day13.CalculateTimestamp(input);
            Assert.That(result, Is.EqualTo(17));
        }
    }
}