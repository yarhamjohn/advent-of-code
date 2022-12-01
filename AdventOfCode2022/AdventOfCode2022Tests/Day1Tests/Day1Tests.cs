using AdventOfCode2022.Day1;

namespace AdventOfCode2022Tests.Day1Tests;

[TestFixture]
public class Day1Tests
{
    [Test]
    public void GetCaloriesCarriedByElfWithTheMostCalories()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day1.GetTotalCalories(input, 1);
        Assert.That(result, Is.EqualTo(24000));
    }
    
    [Test]
    public void GetCaloriesCarriedByElvesWithTheMostCalories()
    {
        var inputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day1Tests/Input/example.txt");
        var input = File.ReadLines(inputPath);
        
        var result = Day1.GetTotalCalories(input, 3);
        Assert.That(result, Is.EqualTo(45000));
    }
}