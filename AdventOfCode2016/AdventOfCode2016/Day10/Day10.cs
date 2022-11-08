namespace AdventOfCode2016.Day10;

public static class Day10
{
    public static long GetComparerBotNumber(string[] input, int valueOne, int valueTwo)
    {
        var (bots, outputs) = CreateTargets(input.Where(x => x[..3] == "bot"));

        ApplyInputs(input, bots);

        while (bots.Values.Any(x => x.Active()))
        {
            foreach (var bot in bots.Values.Where(x => x.Active()))
            {
                var instructions = bot.GetInstructions();
            
                if (!instructions.Select(x => x.value).Except(new [] {valueOne, valueTwo}).Any())
                {
                    return bot.Id;
                }

                ApplyInstructions(instructions, bots, outputs);
            }
        }

        throw new Exception("No bot compared provided values");
    }
    
    public static long GetOutputSummary(string[] input)
    {
        var (bots, outputs) = CreateTargets(input.Where(x => x[..3] == "bot"));

        ApplyInputs(input, bots);

        while (bots.Values.Any(x => x.Active()))
        {
            foreach (var bot in bots.Values.Where(x => x.Active()))
            {
                ApplyInstructions(bot.GetInstructions(), bots, outputs);
            }
        }

        return outputs
            .Where(x => x.Key is 0 or 1 or 2)
            .SelectMany(y => y.Value.Values)
            .Aggregate((x, y) => x * y);
    }

    private static void ApplyInstructions((Instruction instruction, int value)[] instructions, Dictionary<int, Bot> bots,
        Dictionary<int, Output> outputs)
    {
        foreach (var (instruction, value) in instructions)
        {
            switch (instruction)
            {
                case ToBot botInstruction:
                    bots[botInstruction.BotId].AddValue(value);
                    break;
                case ToOutput outputInstruction:
                    outputs[outputInstruction.BinId].AddValue(value);
                    break;
            }
        }
    }

    private static void ApplyInputs(IEnumerable<string> input, IReadOnlyDictionary<int, Bot> bots)
    {
        foreach (var line in input.Where(x => x[..5] == "value"))
        {
            var segments = line.Split(" ");
            bots[Convert.ToInt32(segments[5])].AddValue(Convert.ToInt32(segments[1]));
        }
    }

    private static (Dictionary<int, Bot>, Dictionary<int, Output>) CreateTargets(IEnumerable<string> input)
    {
        var bots = new Dictionary<int, Bot>();
        var outputs = new Dictionary<int, Output>();
        foreach (var line in input)
        {
            var segments = line.Split(" ");
            Instruction lowInstruction = segments[5] == "bot"
                ? new ToBot(Convert.ToInt32(segments[6]))
                : new ToOutput(Convert.ToInt32(segments[6]));
            
            Instruction highInstruction = segments[10] == "bot"
                ? new ToBot(Convert.ToInt32(segments[11]))
                : new ToOutput(Convert.ToInt32(segments[11]));

            bots.Add(Convert.ToInt32(segments[1]), new Bot(Convert.ToInt32(segments[1]), lowInstruction, highInstruction));

            if (lowInstruction is ToOutput toOutputLow)
            {
                outputs.Add(Convert.ToInt32(toOutputLow.BinId), new Output());
            }
            
            if (highInstruction is ToOutput toOutputHigh)
            {
                outputs.Add(Convert.ToInt32(toOutputHigh.BinId), new Output());
            }
        }

        return (bots, outputs);
    }

    private class Bot
    {
        public readonly int Id;
        private readonly List<int> _value = new();
        private readonly Instruction _lowInstruction;
        private readonly Instruction _highInstruction;

        public Bot(int id, Instruction lowInstruction, Instruction highInstruction)
        {
            Id = id;
            _lowInstruction = lowInstruction;
            _highInstruction = highInstruction;
        }
        
        public void AddValue(int value)
        {
            if (_value.Count > 2)
            {
                throw new InvalidOperationException("Bot cannot hold more than 2 value tokens");
            }
            
            _value.Add(value);
        }

        public bool Active() => _value.Count == 2;

        public (Instruction instruction, int value)[] GetInstructions()
        {
            var instructions = new[] { (_lowInstruction, _value.Min()), (_highInstruction, _value.Max()) };

            _value.RemoveAll(_ => true);
            
            return instructions;
        }
    }

    private class Output 
    {
        public readonly List<int> Values = new();

        public void AddValue(int value) => Values.Add(value);
    }

    private record Instruction;

    private record ToOutput(int BinId) : Instruction;

    private record ToBot(int BotId) : Instruction;
}