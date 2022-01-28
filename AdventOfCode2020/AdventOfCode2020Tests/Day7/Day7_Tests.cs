using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2020Tests.Day7
{
    [TestFixture]
    public class Day7_Tests
    {
        [Test]
        public void Should_Get_Count_Of_Bag_Colours()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day7.Day7.CountContainingBagColours(input);
            Assert.That(result, Is.EqualTo(4));
        }
        
        [Test]
        public void Should_Get_Count_Of_Bags()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day7/Input/Example2.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day7.Day7.CountContainingBags(input);
            Assert.That(result, Is.EqualTo(126));
        }
    }
}