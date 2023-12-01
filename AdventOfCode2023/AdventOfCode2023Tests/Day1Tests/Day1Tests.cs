using AdventOfCode2023.Day1;

namespace AdventOfCode2023Tests.Day1Tests;

[TestFixture]
public class Day1Tests
{
    [Test]
    public void GetCalibrationTotal()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day1.GetCalibrationSum(input);
        Assert.That(result, Is.EqualTo(142));
    }
    
    [Test]
    public void GetCalibrationTotalPart2()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1Tests/Input/example2.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day1.GetCalibrationSum(input);
        Assert.That(result, Is.EqualTo(281));
    }
    
    [Test]
    public void GetCalibrationTotalOneNumber()
    {
        var input = new List<string> { "abc1" };
        
        var result = Day1.GetCalibrationSum(input);
        Assert.That(result, Is.EqualTo(11));
    }
    
    [Test]
    public void GetCalibrationTotalTwoNumbers()
    {
        var input = new List<string> { "a2bc1" };
        
        var result = Day1.GetCalibrationSum(input);
        Assert.That(result, Is.EqualTo(21));
    }
    
    [Test]
    public void GetCalibrationTotalOneTextNumber()
    {
        var input = new List<string> { "abonec" };
        
        var result = Day1.GetCalibrationSum(input);
        Assert.That(result, Is.EqualTo(11));
    }
    
    [Test]
    public void GetCalibrationTotalTwoTextNumbers()
    {
        var input = new List<string> { "abonecthreede" };
        
        var result = Day1.GetCalibrationSum(input);
        Assert.That(result, Is.EqualTo(13));
    }
    
    [Test]
    public void GetCalibrationTotalOneRealAndOneTextNumber()
    {
        var input = new List<string> { "abonecthreed4e" };
        
        var result = Day1.GetCalibrationSum(input);
        Assert.That(result, Is.EqualTo(14));
    }
    
    [Test]
    public void RandomTest()
    {
        var input = new List<string> { "236twoknbxlczgd" };
        
        var result = Day1.GetCalibrationSum(input);
        Assert.That(result, Is.EqualTo(22));
    }
    
    [Test]
    public void RandomTest2()
    {
        var input = new List<string> { "fourbsqr7bktkbqbdlpfour" };
        
        var result = Day1.GetCalibrationSum(input);
        Assert.That(result, Is.EqualTo(44));
    }
}