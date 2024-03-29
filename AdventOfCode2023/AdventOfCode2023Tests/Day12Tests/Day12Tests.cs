﻿using AdventOfCode2023.Day12;

namespace AdventOfCode2023Tests.Day12Tests;

[TestFixture]
public class Day12Tests
{
    [Test]
    public static void test()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day12.test(input);
        Assert.That(result, Is.EqualTo(21));
    }
    
    
    [TestCase("???.### 1,1,3", 1)]
    [TestCase(".??..??...?##. 1,1,3", 4)]
    [TestCase("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
    [TestCase("????.#...#... 4,1,1", 1)]
    [TestCase("????.######..#####. 1,6,5", 4)]
    [TestCase("?###???????? 3,2,1", 10)]
    [TestCase("#???????????????#?? 3,2,1,5", 46)]
    public static void test2(string line, int expected)
    {
        var result = Day12.test(new []{line});
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase("#???????????????#?? 3,2,1,5", 1)]
    public static void test3(string line, int expected)
    {
        var result = Day12.testUnfolded(new []{line});
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public static void SumDamageCombinations()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day12.SumDamageCombinations(input);
        Assert.That(result, Is.EqualTo(21));
    }
    
    [Test]
    public static void SumDamageCombinationsUnfolded()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day12Tests/Input/example.txt");
        var input = File.ReadAllLines(inputPath);

        var result = Day12.testUnfolded(input);
        Assert.That(result, Is.EqualTo(525152));
    }
    
    private static IEnumerable<TestCaseData> GetMaskData()
    {
        yield return new TestCaseData(1, new[] { 1 }, new[] { "#" });
        yield return new TestCaseData(2, new[] { 1 }, new[] { "#.", ".#" });
        yield return new TestCaseData(2, new[] { 2 }, new[] { "##" });
        yield return new TestCaseData(3, new[] { 1 }, new[] { "#..", ".#.", "..#" });
        yield return new TestCaseData(3, new[] { 2 }, new[] { "##.", ".##" });
        yield return new TestCaseData(3, new[] { 3 }, new[] { "###" });
        yield return new TestCaseData(4, new[] { 1 }, new[] { "#...", ".#..", "..#.", "...#" });
        yield return new TestCaseData(4, new[] { 2 }, new[] { "##..", ".##.", "..##" });
        yield return new TestCaseData(4, new[] { 3 }, new[] { "###.", ".###" });
        yield return new TestCaseData(4, new[] { 4 }, new[] { "####" });
        
        yield return new TestCaseData(3, new[] { 1, 1 }, new[] { "#.#" });
        yield return new TestCaseData(4, new[] { 1, 1 }, new[] { "#.#.", ".#.#", "#..#" });
        yield return new TestCaseData(4, new[] { 2, 1 }, new[] { "##.#" });
        yield return new TestCaseData(4, new[] { 1, 2 }, new[] { "#.##" });
        yield return new TestCaseData(5, new[] { 1, 1 }, new[] { "#.#..", "#..#.", "#...#", ".#.#.", ".#..#", "..#.#" });
        yield return new TestCaseData(5, new[] { 2, 1 }, new[] { "##.#.", "##..#", ".##.#" });
        yield return new TestCaseData(5, new[] { 1, 2 }, new[] { "#.##.", "#..##", ".#.##" });
        yield return new TestCaseData(5, new[] { 3, 1 }, new[] { "###.#" });
        yield return new TestCaseData(5, new[] { 1, 3 }, new[] { "#.###" });
        yield return new TestCaseData(5, new[] { 2, 2 }, new[] { "##.##" });
        
        yield return new TestCaseData(5, new[] { 1, 1, 1 }, new[] { "#.#.#" });
        yield return new TestCaseData(6, new[] { 1, 1, 1 }, new[] { "#.#.#.", "#..#.#", "#.#..#", ".#.#.#" });
        yield return new TestCaseData(7, new[] { 2, 1, 1 }, new[] { "##.#.#.", "##..#.#", "##.#..#", ".##.#.#" });
        yield return new TestCaseData(7, new[] { 2, 2 }, new[] { "##.##..", "##..##.", "##...##", ".##..##", "..##.##", ".##.##." });
        yield return new TestCaseData(7, new[] { 2, 3 }, new[] { "##.###.", "##..###", ".##.###" });
        yield return new TestCaseData(7, new[] { 3, 2 }, new[] { "###.##.", "###..##", ".###.##" });
        yield return new TestCaseData(7, new[] { 3, 3 }, new[] { "###.###" });
        yield return new TestCaseData(7, new[] { 1, 1, 1, 1 }, new[] { "#.#.#.#" });
        yield return new TestCaseData(7, new[] { 1, 1, 2 }, new[] { ".#.#.##", "#..#.##", "#.#..##", "#.#.##." });
        yield return new TestCaseData(7, new[] { 1, 2, 1 }, new[] { "#.##.#.", "#..##.#", "#.##..#", ".#.##.#" });
        yield return new TestCaseData(7, new[] { 3, 1, 1 }, new[] { "###.#.#" });
    }

    [TestCaseSource(nameof(GetMaskData))]
    public static void GetMasks(int lineLength, int[] places, string[] expected)
    {
        var actual = Day12.GetMasks(lineLength, places);
        
        Assert.That(actual, Is.EquivalentTo(expected));
    }
    
    [TestCase("# 1", 1)]
    [TestCase("? 1", 1)]
    [TestCase("?. 1", 1)]
    [TestCase(".? 1", 1)]
    [TestCase("?? 1", 2)]
    [TestCase("??? 1", 3)]
    [TestCase("??? 2", 2)]
    [TestCase("??? 3", 1)]
    [TestCase("??? 1,1", 1)]
    [TestCase("???.### 1,1,3", 1)]
    [TestCase(".??..??...?##. 1,1,3", 4)]
    [TestCase("?#?#?#?#?#?#?#? 1,3,1,6", 1)]
    [TestCase("????.#...#... 4,1,1", 1)]
    [TestCase("????.######..#####. 1,6,5", 4)]
    [TestCase("?###???????? 3,2,1", 10)]
    [TestCase("?.??.??###??.#.?? 1,1,4,1,1", 16)]
    [TestCase("??????#?#?#?..???#?. 6,2", 4)]
    [TestCase("?.#????????.???? 1,5,2", 14)]
    [TestCase("????#??.?????#? 2,5", 4)]
    [TestCase("??.?.#?.????? 1,2,1", 18)]
    [TestCase("?#??????.? 2,1", 11)]
    public static void SmallerChunks(string input, int expected)
    {
        var actual = Day12.SumDamageCombinations([input]);
        
        Assert.That(actual, Is.EqualTo(expected));
    }
}