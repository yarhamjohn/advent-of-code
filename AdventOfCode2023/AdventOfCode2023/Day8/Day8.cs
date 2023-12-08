namespace AdventOfCode2023.Day8;

public static class Day8
{
    public static long CalculateNumSteps(string[] input)
    {
        var instructions = input.First();
        var nodes = ParseNodes(input);

        var count = 0;

        var nextNode = "AAA";
        var nextInstruction = 0;
        while (nextNode != "ZZZ")
        {
            count++;
            
            nextNode = instructions[nextInstruction] == 'L' ? nodes[nextNode].left : nodes[nextNode].right;
            
            nextInstruction++;
            if (nextInstruction == instructions.Length)
            {
                nextInstruction = 0;
            }
        }
        
        return count;
    }

    public static long CalculateNumStepsGhosts(string[] input)
    {
        var instructions = input.First();
        var nodes = ParseNodes(input);

        var count = 0;
        
        var nextInstruction = 0;
        var inPlayNodes = nodes.Keys.Where(x => x.EndsWith('A')).ToArray();
        while (inPlayNodes.Any(x => !x.EndsWith('Z')))
        {
            count++;
            
            for (var i = 0; i < inPlayNodes.Length; i++)
            {
                inPlayNodes[i] = instructions[nextInstruction] == 'L' ? nodes[inPlayNodes[i]].left : nodes[inPlayNodes[i]].right;
            }
            
            nextInstruction++;
            if (nextInstruction == instructions.Length)
            {
                nextInstruction = 0;
            }
        }
        
        return count;
    }

    private static Dictionary<string, (string left, string right)> ParseNodes(string[] input)
    {
        var nodes = new Dictionary<string, (string left, string right)>();
        foreach (var section in input[2..].Select(x => x.Split(" = ")))
        {
            var options = section[1].Replace("(", "").Replace(")", "").Split(", ");
            nodes[section[0]] = (options[0], options[1]);
        }

        return nodes;
    }
}