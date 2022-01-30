using AdventOfCode2015.Day3;

namespace AdventOfCode2015Tests.Day3Tests;

[TestFixture]
public class Day3Tests
{
    [TestCase(">", 2)]
    [TestCase("^>v<", 4)]
    [TestCase("^v^v^v^v^v", 2)]
    public void GetNumberOfHouses_Returns_NumberVisited(string input, int numHouses)
    {
        var result = Day3.GetNumberOfHouses(input);
        Assert.That(result, Is.EqualTo(numHouses));
    }
    
    [TestCase("^v", 3)]
    [TestCase("^>v<", 3)]
    [TestCase("^v^v^v^v^v", 11)]
    public void GetNumberOfHousesWithRoboSanta_Returns_NumberVisited(string input, int numHouses)
    {
        var result = Day3.GetNumberOfHousesWithRoboSanta(input);
        Assert.That(result, Is.EqualTo(numHouses));
    }
}