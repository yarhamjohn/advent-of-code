using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2020Tests.Day8
{
    [TestFixture]
    public class Day8_Tests
    {
        [Test]
        public void Should_Get_Num_Before_Infinite_Loop()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day8.Day8.GetNumBeforeInfiniteLoop(input);
            Assert.That(result, Is.EqualTo(5));
        }
        
        [Test]
        public void Should_Get_Num_After_Breaking_Infinite_Loop()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day8/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day8.Day8.GetNumAfterBreakingInfiniteLoop(input);
            Assert.That(result, Is.EqualTo(8));
        }
    }
}