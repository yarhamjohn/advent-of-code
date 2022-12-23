namespace AdventOfCode2022.Day21;

public static class Day21
{
    public static long NumberYelledByRoot(string[] input)
    {
        var monkeys = GetMonkeys(input);
        return monkeys["root"].GetNumber(monkeys);
    }

    private static Dictionary<string, IMonkey> GetMonkeys(string[] input)
    {
        var monkeys = new Dictionary<string, IMonkey>();
        foreach (var line in input)
        {
            var segments = line.Split(": ");
            var id = segments[0];

            if (int.TryParse(segments[1], out var num))
            {
                monkeys[id] = new NumberMonkey(num);
            }
            else
            {
                var formulaSegments = segments[1].Split(" ");
                monkeys[id] = new FormulaMonkey(formulaSegments[0], formulaSegments[2], formulaSegments[1]);
            }
        }

        return monkeys;
    }

    public interface IMonkey
    {
        long GetNumber(Dictionary<string, IMonkey> monkeys);
    }

    public class NumberMonkey : IMonkey
    {
        private readonly int _num;

        public NumberMonkey(int num)
        {
            _num = num;
        }

        public long GetNumber(Dictionary<string, IMonkey> _) => _num;
    }
    
    public class FormulaMonkey : IMonkey
    {
        private readonly string _monkeyOneId;
        private readonly string _monkeyTwoId;
        private readonly string _operation;

        public FormulaMonkey(string monkeyOneId, string monkeyTwoId, string operation)
        {
            _monkeyOneId = monkeyOneId;
            _monkeyTwoId = monkeyTwoId;
            _operation = operation;
        }

        public long GetNumber(Dictionary<string, IMonkey> monkeys)
        {
            return _operation switch
            {
                "*" => monkeys[_monkeyOneId].GetNumber(monkeys) * monkeys[_monkeyTwoId].GetNumber(monkeys),
                "+" => monkeys[_monkeyOneId].GetNumber(monkeys) + monkeys[_monkeyTwoId].GetNumber(monkeys),
                "-" => monkeys[_monkeyOneId].GetNumber(monkeys) - monkeys[_monkeyTwoId].GetNumber(monkeys),
                "/" => monkeys[_monkeyOneId].GetNumber(monkeys) / monkeys[_monkeyTwoId].GetNumber(monkeys),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}