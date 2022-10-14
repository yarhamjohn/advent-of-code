using System.IO;
using AdventOfCode2015.Day19;

namespace AdventOfCode2015Tests.Day19Tests;

[TestFixture]
public class Day19Tests
{
    [TestCase("HOH", 4)]
    [TestCase("HOHOHO", 7)]
    public static void CorrectlyCountDistinctMolecules(string molecule, int expected)
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day19.CountDistinctMolecules(input, molecule);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    
    [TestCase("HOH", 3)]
    [TestCase("HOHOHO", 6)]
    // [TestCase("CRnSiRnCaPTiMgYCaPTiRnFArSiThFArCaSiThSiThPBCaCaSiRnSiRnTiTiMgArPBCaPMgYPTiRnFArFArCaSiRnBPMgArPRnCaPTiRnFArCaSiThCaCaFArPBCaCaPTiTiRnFArCaSiRnSiAlYSiThRnFArArCaSiRnBFArCaCaSiRnSiThCaCaCaFYCaPTiBCaSiThCaSiThPMgArSiRnCaPBFYCaCaFArCaCaCaCaSiThCaSiRnPRnFArPBSiThPRnFArSiRnMgArCaFYFArCaSiRnSiAlArTiTiTiTiTiTiTiRnPMgArPTiTiTiBSiRnSiAlArTiTiRnPMgArCaFYBPBPTiRnSiRnMgArSiThCaFArCaSiThFArPRnFArCaSiRnTiBSiThSiRnSiAlYCaFArPRnFArSiThCaFArCaCaSiThCaCaCaSiRnPRnCaFArFYPMgArCaPBCaPBSiRnFYPBCaFArCaSiAl", 0)]
    public static void GetFewestSteps(string molecule, int expected)
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day19Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);

        var result = Day19.GetFewestSteps(input, molecule);
        
        Assert.That(result, Is.EqualTo(expected));
    }
}