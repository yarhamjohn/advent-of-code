namespace AdventOfCode2016.Day12;

public static class Day12
{
    public static long GetValueOfRegister(string[] input, string register)
    {
        var registers = new Dictionary<string, long> { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 } };

        var position = 0;
        while (position < input.Length)
        {
            var instruction = input[position].Split(" ");
            switch (instruction[0])
            {
                case "cpy":
                    ExecuteCopy(registers, instruction[2], instruction[1]);
                    break;
                case "inc":
                    ExecuteIncrement(registers, instruction[1]);
                    break;
                case "dec":
                    ExecuteDecrement(registers, instruction[1]);
                    break;
                case "jnz":
                    position += ExecuteJump(registers, instruction[1], Convert.ToInt32(instruction[2]));
                    continue;
            }

            position++;
        }
        
        return registers[register];
    }

    private static int ExecuteJump(Dictionary<string, long> registers, string indicator, int distance)
        => CanJump(registers, indicator) ? distance : 1;

    private static bool CanJump(IReadOnlyDictionary<string, long> registers, string indicator)
        => registers.ContainsKey(indicator) ? registers[indicator] != 0 : indicator != "0";
    
    private static void ExecuteDecrement(IDictionary<string, long> registers, string register)
        => registers[register]--;

    private static void ExecuteIncrement(IDictionary<string, long> registers, string register) 
        => registers[register]++;

    private static void ExecuteCopy(Dictionary<string, long> registers, string register, string nextValue)
        => registers[register] = ((IReadOnlyDictionary<string, long>)registers).ContainsKey(nextValue) 
            ? registers[nextValue]
            : Convert.ToInt32(nextValue);
}