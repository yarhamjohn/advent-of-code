using AdventOfCode2022.Day5;

namespace AdventOfCode2022Tests.Day5Tests;

[TestFixture]
public class Day5Tests
{
    [Test]
    public void GetTopCrates9000()
    {
        var stackInputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/stack.txt");
        var stackInput = File.ReadLines(stackInputPath);

        var movementInputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/movement.txt");
        var movementInput = File.ReadLines(movementInputPath);

        var result = Day5.GetTopCrates9000(stackInput, movementInput);
        Assert.That(result, Is.EqualTo("CMZ"));
    }
    
    [Test]
    public void GetTopCrates9001()
    {
        var stackInputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/stack.txt");
        var stackInput = File.ReadLines(stackInputPath);

        var movementInputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Day5Tests/Input/movement.txt");
        var movementInput = File.ReadLines(movementInputPath);

        var result = Day5.GetTopCrates9001(stackInput, movementInput);
        Assert.That(result, Is.EqualTo("MCD"));
    }
}