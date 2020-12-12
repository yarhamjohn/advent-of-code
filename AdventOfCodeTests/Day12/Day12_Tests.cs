using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCodeTests.Day12
{
    [TestFixture]
    public class Day12_Tests
    {
        [Test]
        public void Should_Calculate_Manhattan_Distance()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day12.CalculateManhattanDistance(input);
            Assert.That(result, Is.EqualTo(25));
        }
        
        [Test]
        public void Should_Calculate_Manhattan_Distance_Using_Waypoint()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode.Day12.CalculateManhattanDistanceUsingWaypoint(input);
            Assert.That(result, Is.EqualTo(286));
        }
    }
}