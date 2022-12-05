using AdventOfCode2022.Day5;

namespace AdventOfCode2022Tests.Day5Tests;

[TestFixture]
public class Day5Tests
{
    [Test]
    public void GetContainedPairs()
    {
        var stackInputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/stack.txt");
        var stackInput = File.ReadLines(stackInputPath);

        var movementInputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/movement.txt");
        var movementInput = File.ReadLines(movementInputPath);

        var result = Day5.GetTopCrates(stackInput, movementInput);
        Assert.That(result, Is.EqualTo("CMZ"));
    }
}