using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        long targetNum;
        IMonkey sideNotDone;
        
        try
        {
            targetNum = monkeys[((RootMonkey)monkeys["root"]).MonkeyOneId].GetNumber(monkeys);
            sideNotDone = monkeys[((RootMonkey)monkeys["root"]).MonkeyTwoId];
        }
        catch (Exception _)
        {
            sideNotDone = monkeys[((RootMonkey)monkeys["root"]).MonkeyOneId];
            targetNum = monkeys[((RootMonkey)monkeys["root"]).MonkeyTwoId].GetNumber(monkeys);
        }

        var formula = sideNotDone.GetJson(monkeys);

        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, MaxDepth = 128 };
        var json = JsonConvert.DeserializeObject<JArray>(formula, settings);

        targetNum = EvaluateJson(json, targetNum);

        return targetNum;
    }

    private static long EvaluateJson(JArray json, long targetNum)
    {
        if (json[0].Type is JTokenType.Integer &&
            json[2].Type is JTokenType.Array)
        {
            targetNum = ResolveLeftSideNumber((string)json[1], (long)json[0], targetNum);
            return EvaluateJson((JArray)json[2], targetNum);
        }

        if (json[0].Type is JTokenType.Array &&
            json[2].Type is JTokenType.Integer)
        {
            targetNum = ResolveRightSideNumber((string)json[1], (long)json[2], targetNum);
            return EvaluateJson((JArray)json[0], targetNum);
        }

        if (json[0].Type is JTokenType.Array &&
            json[2].Type is JTokenType.Array)
        {
            if (string.Join("", (JArray)json[0]).Contains("\"humn\""))
            {
                var num = SolveJson((JArray)json[2]);

                targetNum = ResolveRightSideNumber((string)json[1], num, targetNum);

                return EvaluateJson((JArray)json[0], targetNum);
            }

            if (string.Join("", (JArray)json[2]).Contains("\"humn\""))
            {
                var num = SolveJson((JArray)json[0]);

                targetNum = ResolveLeftSideNumber((string)json[1], num, targetNum);

                return EvaluateJson((JArray)json[2], targetNum);
            }
        }

        if (json[0].Type is JTokenType.String && (string)json[0] == "humn" && json[2].Type is JTokenType.Integer)
        {
            return ResolveRightSideNumber((string)json[1], (long)json[2], targetNum);
        }

        if (json[2].Type is JTokenType.String && (string)json[2] == "humn" && json[0].Type is JTokenType.Integer)
        {
            return ResolveLeftSideNumber((string)json[1], (long)json[0], targetNum);
        }

        if (json[0].Type is JTokenType.String && (string)json[0] == "humn" && json[2].Type is JTokenType.Array)
        {
            var num = SolveJson((JArray)json[2]);
            return ResolveRightSideNumber((string)json[1], num, targetNum);
        }

        if (json[2].Type is JTokenType.String && (string)json[2] == "humn" && json[0].Type is JTokenType.Array)
        {
            var num = SolveJson((JArray)json[0]);
            return ResolveLeftSideNumber((string)json[1], num, targetNum);
        }

        throw new InvalidOperationException("Interesting...");
    }

    private static long SolveJson(JArray json)
    {
        if (json[0].Type is JTokenType.Integer &&
            json[2].Type is JTokenType.Integer)
        {
            return (string) json[1] switch
            {
                "*" => (long) json[0] * (long) json[2],
                "+" => (long) json[0] + (long) json[2],
                "-" => (long) json[0] - (long) json[2],
                "/" => (long) json[0] / (long) json[2],
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        if (json[0].Type is JTokenType.Integer &&
            json[2].Type is JTokenType.Array)
        {
            return (string) json[1] switch
            {
                "*" => (long) json[0] * SolveJson((JArray) json[2]),
                "+" => (long) json[0] + SolveJson((JArray) json[2]),
                "-" => (long) json[0] - SolveJson((JArray) json[2]),
                "/" => (long) json[0] / SolveJson((JArray) json[2]),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        if (json[0].Type is JTokenType.Array &&
            json[2].Type is JTokenType.Integer)
        {
            return (string) json[1] switch
            {
                "*" => SolveJson((JArray) json[0]) * (long) json[2],
                "+" => SolveJson((JArray) json[0]) + (long) json[2],
                "-" => SolveJson((JArray) json[0]) - (long) json[2],
                "/" => SolveJson((JArray) json[0]) / (long) json[2],
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        if (json[0].Type is JTokenType.Array &&
            json[2].Type is JTokenType.Array)
        {
            return (string) json[1] switch
            {
                "*" => SolveJson((JArray) json[0]) * SolveJson((JArray) json[2]),
                "+" => SolveJson((JArray) json[0]) + SolveJson((JArray) json[2]),
                "-" => SolveJson((JArray) json[0]) - SolveJson((JArray) json[2]),
                "/" => SolveJson((JArray) json[0]) / SolveJson((JArray) json[2]),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    
        throw new InvalidOperationException();
    }

    private static long ResolveRightSideNumber(string operation, long rightSide, long targetNum)
    {
        switch (operation)
        {
            case "/":
                targetNum *= rightSide;
                break;
            case "*":
                targetNum /= rightSide;
                break;
            case "+":
                targetNum -= rightSide;
                break;
            case "-":
                targetNum += rightSide;
                break;
        }

        return targetNum;
    }

    private static long ResolveLeftSideNumber(string operation, long leftSide, long targetNum)
    {
        switch (operation)
        {
            case "/":
                targetNum = leftSide / targetNum;
                break;
            case "*":
                targetNum /= leftSide;
                break;
            case "+":
                targetNum -= leftSide;
                break;
            case "-":
                targetNum = leftSide - targetNum;
                break;
        }

        return targetNum;
    }

    private static Dictionary<string, IMonkey> GetMonkeys(string[] input, bool newRootOperator = false)
    {
        var monkeys = new Dictionary<string, IMonkey>();
        foreach (var line in input)
        {
            var segments = line.Split(": ");
            var id = segments[0];

            if (id == "humn" && newRootOperator)
            {
                monkeys[id] = new HumanMonkey();
            }
            else if (long.TryParse(segments[1], out var num))
            {
                monkeys[id] = new NumberMonkey(num);
            }
            else
            {
                var formulaSegments = segments[1].Split(" ");

                if (id == "root" && newRootOperator)
                {
                    monkeys[id] = new RootMonkey(formulaSegments[0], formulaSegments[2]);
                }
                else
                {
                    monkeys[id] = new FormulaMonkey(formulaSegments[0], formulaSegments[2], formulaSegments[1], newRootOperator);
                }
            }
        }

        return monkeys;
    }

    public interface IMonkey
    {
        long GetNumber(Dictionary<string, IMonkey> monkeys);
        string GetJson(Dictionary<string, IMonkey> monkeys);
    }

    public class NumberMonkey : IMonkey
    {
        private readonly long _num;

        public NumberMonkey(long num)
        {
            _num = num;
        }

        public long GetNumber(Dictionary<string, IMonkey> _) => _num;

        public string GetJson(Dictionary<string, IMonkey> monkeys)
        {
            return _num.ToString();
        }
    }

    public class HumanMonkey : IMonkey
    {
        public long GetNumber(Dictionary<string, IMonkey> _) => -1;

        public string GetJson(Dictionary<string, IMonkey> monkeys)
        {
            return "\"humn\"";
        }
    }
    
    public class FormulaMonkey : IMonkey
    {
        private readonly string _monkeyOneId;
        private readonly string _monkeyTwoId;
        private readonly string _operation;
        private readonly bool _newRootOperator;

        public bool InvolvesHumn { get; }

        public FormulaMonkey(string monkeyOneId, string monkeyTwoId, string operation, bool newRootOperator = false)
        {
            _monkeyOneId = monkeyOneId;
            _monkeyTwoId = monkeyTwoId;
            _operation = operation;
            _newRootOperator = newRootOperator;

            if (_monkeyOneId == "humn" || _monkeyTwoId == "humn")
            {
                InvolvesHumn = true;
            }
        }

        public long GetNumber(Dictionary<string, IMonkey> monkeys)
        {
            if (InvolvesHumn && _newRootOperator)
            {
                throw new Exception("Involves Humn");
            }
            
            return _operation switch
            {
                "*" => monkeys[_monkeyOneId].GetNumber(monkeys) * monkeys[_monkeyTwoId].GetNumber(monkeys),
                "+" => monkeys[_monkeyOneId].GetNumber(monkeys) + monkeys[_monkeyTwoId].GetNumber(monkeys),
                "-" => monkeys[_monkeyOneId].GetNumber(monkeys) - monkeys[_monkeyTwoId].GetNumber(monkeys),
                "/" => monkeys[_monkeyOneId].GetNumber(monkeys) / monkeys[_monkeyTwoId].GetNumber(monkeys),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public (IMonkey left, string operation, IMonkey right) Evaluate(Dictionary<string, IMonkey> monkeys)
        {
            return (monkeys[_monkeyOneId], _operation, monkeys[_monkeyTwoId]);
        }

        public string GetJson(Dictionary<string, IMonkey> monkeys)
        {
            var (left, operation, right) = Evaluate(monkeys);
            return $"[{left.GetJson(monkeys)},\"{operation}\",{right.GetJson(monkeys)}]";
        }
    }

    public class RootMonkey : IMonkey
    {
        public string MonkeyOneId { get; }
        public string MonkeyTwoId { get; }

        public RootMonkey(string monkeyOneId, string monkeyTwoId)
        {
            MonkeyOneId = monkeyOneId;
            MonkeyTwoId = monkeyTwoId;
        }

        public long GetNumber(Dictionary<string, IMonkey> monkeys) =>
            monkeys[MonkeyOneId].GetNumber(monkeys) == monkeys[MonkeyTwoId].GetNumber(monkeys) ? 1 : 0;

        public string GetJson(Dictionary<string, IMonkey> monkeys) => $"{monkeys[MonkeyOneId].GetJson(monkeys)} == {monkeys[MonkeyTwoId].GetJson(monkeys)}";
    }
}