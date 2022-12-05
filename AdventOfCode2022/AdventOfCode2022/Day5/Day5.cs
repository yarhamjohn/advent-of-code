namespace AdventOfCode2022.Day5;

public static class Day5
{
    public static string GetTopCrates(IEnumerable<string> stackInput, IEnumerable<string> movementInput)
    {
        var stacks = GetStacks(stackInput.ToArray());

        foreach (var move in movementInput)
        {
            var segments = move.Split(" ");
            for (var numCrates = 0; numCrates < Convert.ToInt32(segments[1]); numCrates++)
            {
                var crate = stacks[Convert.ToInt32(segments[3])].Pop();
                stacks[Convert.ToInt32(segments[5])].Push(crate);
            }
        }
        
        return string.Join("", stacks.Select(x => x.Value.Peek()));
    }

    private static Dictionary<int, Stack<string>> GetStacks(string[] stackInput)
    {
        var numStacks = stackInput.Last().Count(x => x == '[');

        var stacks = Enumerable.Range(1, numStacks).ToDictionary(x => x, _ => new Stack<string>());
        for (var i = stackInput.Length - 1; i >= 0; i--)
        {
            foreach (var (key, _) in stacks)
            {
                var item = stackInput[i][(key - 1) * 4 + 1].ToString();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    stacks[key].Push(item);
                }
            }
        }
        
        return stacks;
    }
}