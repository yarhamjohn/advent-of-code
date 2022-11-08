namespace AdventOfCode2016.Day10;

public static class Day10
{
    public static long GetComparerBotNumber(string[] input, int valueOne, int valueTwo)
    {
        var (bots, outputs) = CreateTargets(input.Where(x => x[..3] == "bot"));

        ApplyInputs(input, bots);

        while (bots.Values.Any(x => x.Active()))
        {
            
            foreach (var bot in bots)
            {
                Console.WriteLine($"Bot: {bot.Key}, {bot.Value.ToString()}");
            }
            foreach (var output in outputs)
            {
                Console.WriteLine($"Output: {output.Key}, {output.Value.ToString()}");
            }

            Console.WriteLine("---------------------------");

            var activeBots = bots.Values.Where(x => x.Active());
            foreach (var bot in activeBots)
            {
                var instructions = bot.GetInstructions();
            
                if (!instructions.Select(x => x.value).Except(new [] {valueOne, valueTwo}).Any())
                {
                    return bot.Id;
                }

                foreach (var instruction in instructions)
                {
                    if (instruction.instruction is ToBot botInstruction)
                    {
                        bots[botInstruction.BotId].AddValue(instruction.value);
                    }
                
                    if (instruction.instruction is ToOutput outputInstruction)
                    {
                        outputs[outputInstruction.BinId].AddValue(instruction.value);
                    }
                }
            }
        }

        foreach (var bot in bots)
        {
            Console.WriteLine($"Bot: {bot.Key}, {bot.Value.ToString()}");
        }
        foreach (var output in outputs)
        {
            Console.WriteLine($"Bot: {output.Key}, {output.Value.ToString()}");
        }
        
        return 0;
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
                outputs.Add(Convert.ToInt32(toOutputLow.BinId), new Output(Convert.ToInt32(toOutputLow.BinId)));
            }
            
            if (highInstruction is ToOutput toOutputHigh)
            {
                outputs.Add(Convert.ToInt32(toOutputHigh.BinId), new Output(Convert.ToInt32(toOutputHigh.BinId)));
            }
        }

        return (bots, outputs);
    }

    private interface Target
    {
    }

    private class Bot : Target
    {
        public int Id;
        private readonly List<int> _value = new();
        private readonly Instruction _lowInstruction;
        private readonly Instruction _highInstruction;

        public Bot(int id, Instruction lowInstructionInstruction, Instruction highInstructionInstruction)
        {
            Id = id;
            _lowInstruction = lowInstructionInstruction;
            _highInstruction = highInstructionInstruction;
        }
        
        public void AddValue(int value)
        {
            if (_value.Count > 2)
            {
                throw new InvalidOperationException("Bot cannot hold more than 2 value tokens");
            }
            
            _value.Add(value);
        }

        public bool Active()
        {
            return _value.Count == 2;
        }

        public (Instruction instruction, int value)[] GetInstructions()
        {
            var instructions = new[] { (_lowInstruction, _value.Min()), (_highInstruction, _value.Max()) };

            _value.RemoveAll(_ => true);
            
            return instructions;
        }

        public override string ToString()
        {
            return $"Values: {string.Join(",", _value)}";
        }
    }

    private class Output : Target
    {
        private readonly List<int> _value = new();
        public int Id;

        public Output(int id)
        {
            Id = id;
        }
        
        public void AddValue(int value)
        {
            _value.Add(value);
        }
        
        public override string ToString()
        {
            return $"Values: {string.Join(",", _value)}";
        }
    }

    private record Instruction;

    private record ToOutput(int BinId) : Instruction;

    private record ToBot(int BotId) : Instruction;
}