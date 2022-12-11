namespace AdventOfCode2022.Day11;

public static class Day11
{
    public static long CalculateMonkeyBusiness(string[] input)
    {
        var monkeys = ParseInput(input);
        for (var i = 0; i < 20; i++)
        {
            foreach (var (_, monkey) in monkeys)
            {
                while (monkey.StillHasItems())
                {
                    var (targetMonkey, worry) = monkey.InspectNextWorry();
                    monkeys[targetMonkey].AddWorry(worry);
                }
            }
        }
        
        return GetMonkeyBusiness(monkeys.Select(x => x.Value));
    }

    private static int GetMonkeyBusiness(IEnumerable<Monkey> monkeys)
    {
        return monkeys
            .Select(x => x.ItemsInspected)
            .OrderDescending()
            .Take(2)
            .Aggregate(1, (current, next) => current * next);
    }

    private static Dictionary<int, Monkey> ParseInput(string[] input)
    {
        var monkeys = new Dictionary<int, Monkey>();
        for (var line = 0; line < input.Count(); line += 7)
        {
            var items = input[line + 1].Split(":")[1].Split(",").Select(x => Convert.ToInt32(x.Trim())).ToList();
            var op = input[line + 2].Split("=")[1].Trim().Split(" ")[1];
            var val = input[line + 2].Split("=")[1].Trim().Split(" ")[2];
            var divisor = Convert.ToInt32(input[line + 3].Split(" ").Last());
            var trueMonkey = Convert.ToInt32(input[line + 4].Split(" ").Last());
            var falseMonkey = Convert.ToInt32(input[line + 5].Split(" ").Last());

            monkeys[line / 7] = new Monkey(items,
                val == "old"
                    ? GetOperation(op)
                    : GetOperation(op, Convert.ToInt32(val)),
                GetTest(divisor, trueMonkey, falseMonkey));
        }

        return monkeys;
    }

    private static Func<int,int> GetTest(int divisor, int trueMonkey, int falseMonkey)
    {
        return (x) => x % divisor == 0 ? trueMonkey : falseMonkey;
    }

    private static Func<int,int> GetOperation(string op, int val)
    {
        return op == "*" ? x => x * val : x => x + val;
    }

    private static Func<int,int> GetOperation(string op)
    {
        return op == "*" ? x => x * x : x => x + x;
    }
    
    private class Monkey
    {
        private readonly Func<int, int> _operation;
        private readonly Func<int, int> _test;
        private readonly Queue<int> _worryLevels = new();
        public int ItemsInspected;

        public Monkey(List<int> worryLevels, Func<int, int> operation, Func<int, int> test )
        {
            _operation = operation;
            _test = test;

            foreach (var worry in worryLevels)
            {
                _worryLevels.Enqueue(worry);
            }
        }

        public bool StillHasItems() => _worryLevels.Any();

        public (int targetMonkey, int worry) InspectNextWorry()
        {
            var worry = _worryLevels.Dequeue();
            worry = _operation(worry);
            worry /= 3;

            ItemsInspected++;
            
            return (_test(worry), worry);
        }

        public void AddWorry(int worry) => _worryLevels.Enqueue(worry);
    }
}