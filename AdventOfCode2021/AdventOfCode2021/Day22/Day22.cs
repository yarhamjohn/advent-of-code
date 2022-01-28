namespace AdventOfCode2021.Day22;

public static class Day22
{
    public static long CalculateOnCubesSmall(string[] input)
    {
        var instructions = GetInstructions(input);
        var cubes = new State[101, 101, 101];

        var filteredInstructions = instructions.Where(i => i.Start.X is <= 50 and >= -50);
        ApplyInstructions(filteredInstructions, cubes);

        return CountOnCubes(cubes);
    }

    public static long CalculateOnCubes(string[] input)
    {
        var instructions = GetInstructions(input);

        var onCubes = GetAllOnCubes(instructions); // Contains each instruction cube
        var overlapCubes = new List<(Coordinate start, Coordinate end)>(); // Contains each instance of an overlap

        return CountOnCubes(onCubes);
    }

    private static IEnumerable<(Coordinate start, Coordinate end)> GetAllOnCubes(Instruction[] instructions)
    {
        throw new NotImplementedException();
    }

    private static List<(Coordinate start, Coordinate end)> ProcessInstructions(
        Instruction instruction, 
        List<(Coordinate start, Coordinate end)> onCubes)
    {
        if (instruction.State is State.ON)
        {
            if (InstructionFullyContained(instruction, onCubes))
            {
                return onCubes;
            }

            if (InstructionContained(instruction, onCubes))
            {
                onCubes = TurnOnExtraCubes(instruction, onCubes);
                return onCubes;
            }

            onCubes = RemoveAnyFullyContainedCubes(instruction, onCubes);
            
            onCubes.Add((instruction.Start, instruction.End));
        }
        else
        {
            if (InstructionContained(instruction, onCubes))
            {
                onCubes = TurnOffOverlappingCubes(instruction, onCubes);
                return onCubes;
            }
            
            onCubes = RemoveAnyFullyContainedCubes(instruction, onCubes);
            return onCubes;
        }

        throw new NotImplementedException();
    }

    private static List<(Coordinate start, Coordinate end)> TurnOffOverlappingCubes(Instruction instruction, List<(Coordinate start, Coordinate end)> onCubes)
    {
        throw new NotImplementedException();
    }

    private static List<(Coordinate start, Coordinate end)> TurnOnExtraCubes(Instruction instruction, List<(Coordinate start, Coordinate end)> onCubes)
    {
        throw new NotImplementedException();
    }

    private static bool InstructionContained(
        Instruction instruction,
        List<(Coordinate start, Coordinate end)> onCubes)
    {
        return onCubes
            .Any(cube => CubeOverlapsX(instruction, cube) &&
                         CubeOverlapsY(instruction, cube) &&
                         CubeOverlapsZ(instruction, cube));
    }

    private static bool InstructionFullyContained(
        Instruction instruction,
        List<(Coordinate start, Coordinate end)> onCubes)
    {
        return onCubes
            .Any(cube => CubeContainsX(instruction, cube) &&
                         CubeContainsY(instruction, cube) &&
                         CubeContainsZ(instruction, cube));
    }

    private static List<(Coordinate start, Coordinate end)> RemoveAnyFullyContainedCubes(
        Instruction instruction,
        List<(Coordinate start, Coordinate end)> onCubes)
    {
        return onCubes.Where(cube => 
            !(InstructionContainsX(instruction, cube) && 
              InstructionContainsY(instruction, cube) &&
              InstructionContainsZ(instruction, cube)))
            .ToList();
    }

    private static bool CubeContainsX(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return cube.start.X <= instruction.Start.X && cube.end.X >= instruction.End.X;
    }

    private static bool CubeOverlapsX(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return cube.start.X <= instruction.Start.X && cube.end.X >= instruction.Start.X || 
               cube.start.X <= instruction.End.X && cube.end.X >= instruction.End.X;
    }
    
    private static bool CubeContainsY(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return cube.start.Y <= instruction.Start.Y && cube.end.Y >= instruction.End.Y;
    }
    
    private static bool CubeOverlapsY(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return cube.start.Y <= instruction.Start.Y && cube.end.Y >= instruction.Start.Y || 
               cube.start.Y <= instruction.End.Y && cube.end.Y >= instruction.End.Y;
    }
    
    private static bool CubeContainsZ(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return cube.start.Z <= instruction.Start.Z && cube.end.Z >= instruction.End.Z;
    }
    
    private static bool CubeOverlapsZ(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return cube.start.Z <= instruction.Start.Z && cube.end.Z >= instruction.Start.Z || 
               cube.start.Z <= instruction.End.Z && cube.end.Z >= instruction.End.Z;
    }
    
    private static bool InstructionContainsX(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return instruction.Start.X <= cube.start.X &&  instruction.End.X >= cube.end.X;
    }

    private static bool InstructionOverlapsX(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return instruction.Start.X <= cube.start.X && instruction.End.X >= cube.start.X || 
               instruction.Start.X <= cube.end.X && instruction.End.X >= cube.end.X;
    }
    
    private static bool InstructionContainsY(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return instruction.Start.Y <= cube.start.Y &&  instruction.End.Y >= cube.end.Y;
    }
    
    private static bool InstructionOverlapsY(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return instruction.Start.Y <= cube.start.Y && instruction.End.Y >= cube.start.Y || 
               instruction.Start.Y <= cube.end.Y && instruction.End.Y >= cube.end.Y;
    }
    
    private static bool InstructionContainsZ(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return instruction.Start.Z <= cube.start.Z &&  instruction.End.Z >= cube.end.Z;
    }
    
    private static bool InstructionOverlapsZ(Instruction instruction, (Coordinate start, Coordinate end) cube)
    {
        return instruction.Start.Z <= cube.start.Z && instruction.End.Z >= cube.start.Z || 
               instruction.Start.Z <= cube.end.Z && instruction.End.Z >= cube.end.Z;
    }

    private static int CountOnCubes(IEnumerable<(Coordinate start, Coordinate end)> onCubes)
    {
        return onCubes.Aggregate(0, 
            (current, next) => 
                current + Math.Abs(next.start.X - next.end.X) * Math.Abs(next.start.Y - next.end.Y) * Math.Abs(next.start.Y - next.end.Y));
    }

    private static IEnumerable<(int x, int y, int z)> GetTuples(Instruction instruction)
    {
        for (var x = instruction.Start.X; x <= instruction.End.X; x++)
        {
            for (var y = instruction.Start.Y; y <= instruction.End.Y; y++)
            {
                for (var z = instruction.Start.Z; z <= instruction.End.Z; z++)
                {
                    yield return (x, y, z);
                }
            }
        }
    }

    private static State[,,] CreateCubes(Instruction[] instructions)
    {
        var xBoundaries = instructions.SelectMany(i => new[] { i.Start.X, i.End.X }).ToArray();
        var yBoundaries = instructions.SelectMany(i => new[] { i.Start.Y, i.End.Y }).ToArray();
        var zBoundaries = instructions.SelectMany(i => new[] { i.Start.Z, i.End.Z }).ToArray();

        return new State[GetRangeRequired(xBoundaries), GetRangeRequired(yBoundaries), GetRangeRequired(zBoundaries)];
    }

    private static int GetRangeRequired(int[] boundaries)
    {
        return boundaries.Max() - boundaries.Min() + 1;
    }

    private static void ApplyInstructions(IEnumerable<Instruction> instructions, State[,,] cubes)
    {
        foreach (var instruction in instructions)
        {
            ApplyInstruction(cubes, instruction);
        }
    }

    private static void ApplyInstruction(State[,,] cubes, Instruction instruction)
    {
        for (var x = instruction.Start.X + 50; x <= instruction.End.X + 50; x++)
        {
            for (var y = instruction.Start.Y + 50; y <= instruction.End.Y + 50; y++)
            {
                for (var z = instruction.Start.Z + 50; z <= instruction.End.Z + 50; z++)
                {
                    cubes[x, y, z] = instruction.State;
                }
            }
        }
    }

    private static int CountOnCubes(State[,,] cubes)
    {
        var count = 0;
        for (var x = 0; x < cubes.GetLength(0); x++)
        {
            for (var y = 0; y < cubes.GetLength(1); y++)
            {
                for (var z = 0; z < cubes.GetLength(2); z++)
                {
                    if (cubes[x, y, z] is State.ON)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    private static Instruction[] GetInstructions(string[] input)
    {
        var instructions = new List<Instruction>();
        foreach (var line in input)
        {
            var spaceSplit = line.Split(" ");
            var state = spaceSplit[0] == "on" ? State.ON : State.OFF;

            var commaSplit = spaceSplit[1].Split(",");
            var xSplit = commaSplit[0].Split("=")[1].Split("..").Select(x => Convert.ToInt32(x)).ToArray();
            var ySplit = commaSplit[1].Split("=")[1].Split("..").Select(x => Convert.ToInt32(x)).ToArray();
            var zSplit = commaSplit[2].Split("=")[1].Split("..").Select(x => Convert.ToInt32(x)).ToArray();

            var startCoordinate = new Coordinate(xSplit[0], ySplit[0], zSplit[0]);
            var endCoordinate = new Coordinate(xSplit[1], ySplit[1], zSplit[1]);
            
            instructions.Add(new Instruction(state, startCoordinate, endCoordinate));
        }

        return instructions.ToArray();
    }

    private enum State { OFF, ON }

    private record Instruction(State State, Coordinate Start, Coordinate End);
    
    private record Coordinate(int X, int Y, int Z);
}