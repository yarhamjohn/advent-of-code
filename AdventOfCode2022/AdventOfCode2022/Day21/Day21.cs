namespace AdventOfCode2022.Day21;

public static class Day21
{
    public static long NumberYelledByRoot(string[] input)
    {
        var monkeys = GetMonkeys(input);
        return monkeys["root"].GetNumber(monkeys);
    }
    
    public static long NumberYelledByRootPartTwo(string[] input)
    {
        var monkeys = GetMonkeys(input, true);

        var numShouted = 0;
        while (true)
        {
            ((NumberMonkey) monkeys["humn"]).UpdateNum(numShouted);
            
            if (monkeys["root"].GetNumber(monkeys) == 1)
            {
                break;
            }
            
            numShouted++;
        }

        return numShouted;
    }

    private static Dictionary<string, IMonkey> GetMonkeys(string[] input, bool newRootOperator = false)
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

                if (id == "root" && newRootOperator)
                {
                    monkeys[id] = new FormulaMonkey(formulaSegments[0], formulaSegments[2], "=");
                }
                else
                {
                    monkeys[id] = new FormulaMonkey(formulaSegments[0], formulaSegments[2], formulaSegments[1]);
                }
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
        private int _num;

        public NumberMonkey(int num)
        {
            _num = num;
        }

        public long GetNumber(Dictionary<string, IMonkey> _) => _num;

        public void UpdateNum(int num) => _num = num;
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
                "=" => monkeys[_monkeyOneId].GetNumber(monkeys) == monkeys[_monkeyTwoId].GetNumber(monkeys) ? 1 : 0,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }   
}