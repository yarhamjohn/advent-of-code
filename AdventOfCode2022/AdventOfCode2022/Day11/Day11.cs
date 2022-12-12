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

    public static long CalculateMonkeyBusinessLarge(string[] input)
    {
        var monkeys = ParseInput(input);
        var stressRelief = monkeys.Aggregate(1, (c, n) => c * n.Value.Divisor);
        for (var i = 0; i < 10000; i++)
        {
            foreach (var (_, monkey) in monkeys)
            {
                while (monkey.StillHasItems())
                {
                    var (targetMonkey, worry) = monkey.InspectNextWorry(stressRelief);
                    monkeys[targetMonkey].AddWorry(worry);
                }
            }
        }

        return GetMonkeyBusiness(monkeys.Select(x => x.Value));
    }
    
    private static long GetMonkeyBusiness(IEnumerable<Monkey> monkeys)
        => monkeys
            .Select(x => x.ItemsInspected)
            .OrderDescending()
            .Take(2)
            .Aggregate(1L, (current, next) => current * next);

    private static Dictionary<int, Monkey> ParseInput(string[] input)
    {
        var monkeys = new Dictionary<int, Monkey>();
        for (var line = 0; line < input.Length; line += 7)
        {
            var items = input[line + 1].Split(":")[1].Split(",").Select(x => Convert.ToInt64(x.Trim())).ToList();
            var op = input[line + 2].Split("=")[1].Trim().Split(" ")[1];
            var val = input[line + 2].Split("=")[1].Trim().Split(" ")[2];
            var divisor = Convert.ToInt32(input[line + 3].Split(" ").Last());
            var trueMonkey = Convert.ToInt32(input[line + 4].Split(" ").Last());
            var falseMonkey = Convert.ToInt32(input[line + 5].Split(" ").Last());

            monkeys[line / 7] = new Monkey(items,
                val == "old"
                    ? GetOperation(op)
                    : GetOperation(op, Convert.ToInt32(val)),
                GetTest(divisor, trueMonkey, falseMonkey), divisor);
        }

        return monkeys;
    }

    private static Func<long,int> GetTest(int divisor, int trueMonkey, int falseMonkey)
        => worry => worry % divisor == 0 ? trueMonkey : falseMonkey;

    private static Func<long,long> GetOperation(string op, long val)
        => op == "*" ? old => old * val : old => old + val;

    private static Func<long,long> GetOperation(string op)
        => op == "*" ? old => old * old : old => old + old;
    
    private class Monkey
    {
        public int Divisor { get; }
        private readonly Func<long, long> _operation;
        private readonly Func<long, int> _test;
        private readonly Queue<long> _worryLevels = new();
        public int ItemsInspected;

        public Monkey(List<long> worryLevels, Func<long, long> operation, Func<long, int> test, int divisor)
        {
            Divisor = divisor;
            _operation = operation;
            _test = test;

            foreach (var worry in worryLevels)
            {
                _worryLevels.Enqueue(worry);
            }
        }

        public bool StillHasItems() => _worryLevels.Any();

        public (int targetMonkey, long worry) InspectNextWorry(long stressRelief)
        {
            var worry = _worryLevels.Dequeue();
            worry = _operation(worry) % stressRelief;

            ItemsInspected++;
            
            return (_test(worry), worry);
        }

        public (int targetMonkey, long worry) InspectNextWorry()
        {
            var worry = _worryLevels.Dequeue();
            worry = _operation(worry) / 3;

            ItemsInspected++;
            
            return (_test(worry), worry);
        }
        
        public void AddWorry(long worry) => _worryLevels.Enqueue(worry);
    }
}