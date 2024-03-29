﻿using System.IO;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2020Tests.Day17
{
    [TestFixture]
    public class Day17_Tests
    {
        [Test]
        public void Should_Count_Active_Cubes()
        {
            var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day17/Input/Example.txt");
            var input = File.ReadAllLines(inputPath).ToList();
            var result = AdventOfCode2020.Day17.Day17.CountActiveCubes(input);
            Assert.That(result, Is.EqualTo(112));
        }
    }
}