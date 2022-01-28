namespace AdventOfCode2021.Day24;

public static class Day24
{
    public static long CalculateLargestModelNumbers(string[] input)
    {
        var nums = new Dictionary<string, int> {{"w", 0}, {"x", 0}, {"y", 0}, {"z", 0}};

        var validNumber = 0L;
        foreach (var target in YieldNumbers())
        {
            if (ProcessInput(input, nums, target))
            {
                var num = Convert.ToInt64(target);
                if (num > validNumber)
                {
                    validNumber = num;
                }
            }
        }
        
        return validNumber;
    }

    private static bool ProcessInput(string[] input, Dictionary<string, int> nums, string target)
    {
        var indexInTarget = 0;
        
        foreach (var line in input)
        {
            var instructions = line.Split(" ");
            switch (instructions[0])
            {
                case "inp":
                    nums[instructions[1]] = Convert.ToInt32(target[indexInTarget]);
                    indexInTarget++;
                    break;
                case "add":
                    nums[instructions[1]] += GetValueFromSecondParameter(instructions[2], nums);
                    break;
                case "mul":
                    nums[instructions[1]] *= GetValueFromSecondParameter(instructions[2], nums);
                    break;
                case "div":
                    nums[instructions[1]] /= GetValueFromSecondParameter(instructions[2], nums);
                    break;
                case "mod":
                    nums[instructions[1]] %= GetValueFromSecondParameter(instructions[2], nums);
                    break;
                case "eql":
                    nums[instructions[1]] =
                        nums[instructions[1]] == GetValueFromSecondParameter(instructions[2], nums) ? 1 : 0;
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        return nums["z"] == 0;
    }

    private static int GetValueFromSecondParameter(string instruction, IReadOnlyDictionary<string, int> nums)
    {
        return instruction is "w" or "x" or "y" or "z" ? nums[instruction] : Convert.ToInt32(instruction);
    }

    private static IEnumerable<string> YieldNumbers()
    {
        for (var i = 11111111111222L; i >= 11111111111111L; i--)
        {
            var strNum = i.ToString();
            if (!strNum.Contains('0'))
            {
                yield return strNum;
            }
        }
    }
}
