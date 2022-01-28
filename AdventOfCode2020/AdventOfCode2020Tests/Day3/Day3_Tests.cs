using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2020Tests.Day3
{
    [TestFixture]
    public class Day3Tests
    {
        [Test]
        public void Should_count_trees()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day3.Day3.CountTreesHit(input, 3, 1);
            Assert.That(result, Is.EqualTo(7));
        }
        
        [Test]
        public void Should_count_trees_for_all_paths()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day3/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result1 = AdventOfCode2020.Day3.Day3.CountTreesHit(input, 1, 1);
            var result2 = AdventOfCode2020.Day3.Day3.CountTreesHit(input, 3, 1);
            var result3 = AdventOfCode2020.Day3.Day3.CountTreesHit(input, 5, 1);
            var result4 = AdventOfCode2020.Day3.Day3.CountTreesHit(input, 7, 1);
            var result5 = AdventOfCode2020.Day3.Day3.CountTreesHit(input, 1, 2);
            Assert.That(result1 * result2 * result3 * result4 * result5 , Is.EqualTo(336));
        }
    }
}