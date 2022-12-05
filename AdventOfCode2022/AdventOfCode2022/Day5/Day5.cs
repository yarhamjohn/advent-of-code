namespace AdventOfCode2022.Day5;

public static class Day5
{
    public static string GetTopCrates9000(IEnumerable<string> stackInput, IEnumerable<string> movementInput)
    {
        var stacks = GetStacks(stackInput.ToArray());

        foreach (var move in movementInput)
        {
            MoveCrates9000(move, stacks);
        }

        return string.Join("", stacks.Select(x => x.Value.Peek()));
    }

    public static string GetTopCrates9001(IEnumerable<string> stackInput, IEnumerable<string> movementInput)
    {
        var stacks = GetStacks(stackInput.ToArray());

        foreach (var move in movementInput)
        {
            MoveCrates9001(move, stacks);
        }
        
        return string.Join("", stacks.Select(x => x.Value.Peek()));
    }
    
    private static void MoveCrates9000(string move, Dictionary<int, Stack<string>> stacks)
    {
        var (numToMove, sourceStack, targetStack) = ParseMovement(move);

        var numMoved = 0;
        while (numMoved < numToMove)
        {
            stacks[targetStack].Push(stacks[sourceStack].Pop());
            numMoved++;
        }
    }

    private static void MoveCrates9001(string move, Dictionary<int, Stack<string>> stacks)
    {
        var (numToMove, sourceStack, targetStack) = ParseMovement(move);

        var tempStack = new Stack<string>();
        while (tempStack.Count < numToMove) 
        {
            tempStack.Push(stacks[sourceStack].Pop());
        }

        while (tempStack.Any())
        {
            stacks[targetStack].Push(tempStack.Pop());
        }
    }
    
    private static (int numToMove, int sourceStack, int targetStack) ParseMovement(string move)
    {
        var segments = move.Split(" ");
        return (numToMove: Convert.ToInt32(segments[1]),
            sourceStack: Convert.ToInt32(segments[3]),
            targetStack: Convert.ToInt32(segments[5]));
    }
    
    private static Dictionary<int, Stack<string>> GetStacks(IReadOnlyList<string> stackInput)
    {
        var numStacks = stackInput[^1].Count(x => x == '[');

        var stacks = Enumerable.Range(1, numStacks).ToDictionary(x => x, _ => new Stack<string>());
        for (var i = stackInput.Count - 1; i >= 0; i--)
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