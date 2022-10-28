using System.IO;
using AdventOfCode2015.Day24;

namespace AdventOfCode2015Tests.Day24Tests;

[TestFixture]
public class Day24Tests
{
    [Test]
    public static void GetQuantumEntanglementForThreeCompartments()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day24Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day24.CalculateQuantumEntanglement(input, 3);
        
        Assert.That(result, Is.EqualTo(99));
    }

    [Test]
    public static void GetQuantumEntanglementForFourCompartments()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day24Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day24.CalculateQuantumEntanglement(input, 4);
        
        Assert.That(result, Is.EqualTo(44));
    }
}