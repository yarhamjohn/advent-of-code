using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2020Tests.Day10
{
    [TestFixture]
    public class Day10_Tests
    {
        [Test]
        public void Should_Get_Num_Jolts_Difference()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day10.Day10.GetJoltDifference(input);
            Assert.That(result, Is.EqualTo(35));
        }
        
        [Test]
        public void Should_Get_Num_Jolts_Difference2()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10/Input/Example2.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day10.Day10.GetJoltDifference(input);
            Assert.That(result, Is.EqualTo(220));
        }
        
        [Test]
        public void Should_Get_Num_Adapter_Arrangements()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day10.Day10.GetAdapterArrangements(input);
            Assert.That(result, Is.EqualTo(8));
        }
        
        [Test]
        public void Should_Get_Num_Adapter_Arrangements2()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10/Input/Example2.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day10.Day10.GetAdapterArrangements(input);
            Assert.That(result, Is.EqualTo(19208));
        }
        
        [Test]
        public void Should_Get_Num_Adapter_Arrangements3()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10/Input/Example3.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day10.Day10.GetAdapterArrangements(input);
            Assert.That(result, Is.EqualTo(4));
        }
        
        [Test]
        public void Should_Get_Num_Adapter_Arrangements4()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day10/Input/Example4.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day10.Day10.GetAdapterArrangements(input);
            Assert.That(result, Is.EqualTo(49));
        }
    }
}