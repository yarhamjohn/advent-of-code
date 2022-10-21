namespace AdventOfCode2015.Day23;

public static class Day23
{
    public static long CalculateRegisterB(IEnumerable<string> input, int registerASeed)
    {
        var instructions = ParseInstructions(input);
        var position = 0;

        var registers = new Dictionary<string, int> { { "a", registerASeed }, { "b", 0 } };
        
        while (position < instructions.Count)
        {
            Console.WriteLine($"position: {position}, Register A: {registers["a"]}, Register B: {registers["b"]}");
            
            var distanceToMove = ApplyInstruction(registers, instructions[position]);
            position += distanceToMove;
        }
        
        return registers["b"];
    }

    private static int ApplyInstruction(Dictionary<string,int> registers, IInstruction instruction)
    {
        var distanceToMove = 0;

        switch (instruction)
        {
            case Increment inc:
                registers[inc.Register] += 1;
                distanceToMove += 1;
                break;
            case Triple tpl:
                registers[tpl.Register] *= 3;
                distanceToMove += 1;
                break;
            case Half hlf:
                registers[hlf.Register] /= 2;
                distanceToMove += 1;
                break;
            case Jump jmp:
                distanceToMove += jmp.Offset;
                break;
            case JumpIfEven jie:
                distanceToMove += registers[jie.Register] % 2 == 0 ? jie.Offset : 1;
                break;
            case JumpIfOne jio:
                distanceToMove += registers[jio.Register] == 1 ? jio.Offset : 1;
                break;
        }
        
        return distanceToMove;
    }

    private static List<IInstruction> ParseInstructions(IEnumerable<string> input)
    {
        var instructions = new List<IInstruction>();
        
        var splitInstructions = input.Select(x => x.Split(" "));
        foreach (var split in splitInstructions)
        {
            var type = split[0];

            IInstruction instruction = type switch
            {
                "inc" => new Increment(split[1]),
                "tpl" => new Triple(split[1]),
                "hlf" => new Half(split[1]),
                "jmp" => new Jump(CalculateOffset(split[1])),
                "jie" => new JumpIfEven(split[1].Replace(",", ""), CalculateOffset(split[2])),
                "jio" => new JumpIfOne(split[1].Replace(",", ""), CalculateOffset(split[2])),
                _ => throw new Exception("unknown instruction")
            };
            
            instructions.Add(instruction);
        }

        return instructions;
    }

    private static int CalculateOffset(string split)
    {
        return split[0] == '+' 
            ? Convert.ToInt32(split.Replace("+", "")) 
            : Convert.ToInt32(split.Replace("-", "")) * -1;
    }

    private interface IInstruction { }

    private record Increment(string Register) : IInstruction;
    
    private record Half(string Register) : IInstruction;

    private record Triple(string Register) : IInstruction;

    private record Jump(int Offset) : IInstruction;
    
    private record JumpIfEven(string Register, int Offset) : IInstruction;
    
    private record JumpIfOne(string Register, int Offset) : IInstruction;
}