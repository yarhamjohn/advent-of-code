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
        return CalculateAllPathSteps(input.First(), ParseNodes(input))
            .Select(CalculatePrimeFactor)
            .SelectMany(primeFactors => primeFactors
                .GroupBy(factor => factor)
                .Select(group => (group.Key, group.Count())))
            .GroupBy(y => y.Key)
            .Select(grp => (grp.Key, grp.Max(x => x.Item2)))
            .Aggregate(1L, (x, y) => x * (long)Math.Pow(y.Key, y.Item2));
    }

    private static IEnumerable<int> CalculateAllPathSteps(string instructions, Dictionary<string, (string left, string right)> nodes)
    {
        foreach (var node in nodes.Keys.Where(x => x.EndsWith('A')))
        {
            var fullCycles = 0;
            var nextNode = node;
            while (!nextNode.EndsWith('Z'))
            {
                nextNode = instructions.Aggregate(nextNode, (current, opt) => opt == 'L'
                    ? nodes[current].left
                    : nodes[current].right);

                fullCycles++;
            }

            yield return fullCycles * instructions.Length;
        }
    }

    private static IEnumerable<int> CalculatePrimeFactor(int number)
    {
        var factor = 2;
        while (number != 1)
        {
            if (number % factor != 0)
            {
                factor++;
                continue;
            }

            number /= factor;

            yield return factor;
        }
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