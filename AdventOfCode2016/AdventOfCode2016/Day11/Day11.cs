namespace AdventOfCode2016.Day10;

public static class Day10
{
    public static long GetComparerBotNumber(string[] input, int valueOne, int valueTwo)
    {
        var bots = CreateBots(input.Where(x => x[..3] == "bot"));

        return 0;
    }

    private static Dictionary<int, Target> CreateBots(IEnumerable<string> input)
    {
        var targets = new Dictionary<int, Target>();
        foreach (var line in input)
        {
            var segments = line.Split(" ");
            Instruction lowInstruction = segments[5] == "bot"
                ? new ToBot(Convert.ToInt32(segments[6]))
                : new ToOutput(Convert.ToInt32(segments[6]));
            
            Instruction highInstruction = segments[10] == "bot"
                ? new ToBot(Convert.ToInt32(segments[11]))
                : new ToOutput(Convert.ToInt32(segments[11]));

            targets.Add(Convert.ToInt32(segments[1]), new Bot(lowInstruction, highInstruction));

            if (lowInstruction is ToOutput toOutputLow)
            {
                targets.Add(Convert.ToInt32(toOutputLow.BinId), new Output());
            }
            
            if (highInstruction is ToOutput toOutputHigh)
            {
                targets.Add(Convert.ToInt32(toOutputHigh.BinId), new Output());
            }
        }

        return targets;
    }

    private interface Target
    {
    }

    private class Bot : Target
    {
        private readonly List<int> _value = new();
        private readonly Instruction _lowInstruction;
        private readonly Instruction _highInstruction;

        public Bot(Instruction lowInstructionInstruction, Instruction highInstructionInstruction)
        {
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

        public (Instruction instruction, int value)[] GetInstructions()
        {
            return new[] { (_lowInstruction, _value.Min()), (_highInstruction, _value.Max()) };
        }
    }

    private class Output : Target
    {
    }

    private record Instruction;

    private record ToOutput(int BinId) : Instruction;

    private record ToBot(int BotId) : Instruction;
}