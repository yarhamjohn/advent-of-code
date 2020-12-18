using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day17
{
    [TestFixture]
    public class Day17_Tests
    {
        [Test]
        public void Should_Count_Active_Cubes_In_3D()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day17/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day17.CountActiveCubes(input, 3);
            Assert.That(result, Is.EqualTo(112));
        }
        
        [Test]
        public void Should_Count_Active_Cubes_In_4D()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day17/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day17.CountActiveCubes(input, 4);
            Assert.That(result, Is.EqualTo(848));
        }
    }
}