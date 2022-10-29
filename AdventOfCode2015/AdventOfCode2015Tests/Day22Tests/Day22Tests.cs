using AdventOfCode2015.Day22;

namespace AdventOfCode2015Tests.Day22Tests;

[TestFixture]
public class Day22Tests
{
    [TestCase(10, 250, 13, 8, 226)]
    [TestCase(10, 250, 14, 8, 641)]
    public static void CorrectlyCountDistinctMolecules(int playerHitPoints, int playerMana, int bossHitPoints, int bossDamage, int expected)
    {
        var result = Day22.CalculateMinimumMana(playerHitPoints, playerMana,bossHitPoints, bossDamage);
        
        Assert.That(result, Is.EqualTo(expected));
    }
}