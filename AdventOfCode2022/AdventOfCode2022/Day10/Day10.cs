namespace AdventOfCode2022.Day10;

public static class Day10
{
    public static long CalculateSignalStrength(IEnumerable<string> input)
    {
        return GetSignalStrengths(input)
            .Where(x => new [] {20, 60, 100, 140, 180, 220}.Contains(x.Key))
            .Sum(y => y.Key * y.Value);
    }

    private static Dictionary<int, int> GetSignalStrengths(IEnumerable<string> input)
    {
        var cycles = 1;

        var cycleSignalStrength = new Dictionary<int, int> { { 1, 1 } };

        foreach (var line in input)
        {
            cycles++;

            cycleSignalStrength[cycles] = cycleSignalStrength[cycles - 1];

            if (line == "noop") continue;

            cycles++;

            cycleSignalStrength[cycles] = cycleSignalStrength[cycles - 1] + Convert.ToInt32(line.Split(" ")[1]);
        }

        return cycleSignalStrength;
    }

    public static void GetMessage(IEnumerable<string> input)
    {
        var screen = GetScreen();

        var signalStrengths = GetSignalStrengths(input);

        for (var cycle = 1; cycle <= 240; cycle++)
        {
            var row = (cycle - 1) / 40;
            var column = (cycle - 1) % 40;
            
            var spritePosition = signalStrengths[cycle];
            if (new[] { spritePosition - 1, spritePosition, spritePosition + 1 }.Contains(column))
            {
                screen[row][column] = "#";
            }
        }
        
        PrintScreen(screen);
    }

    private static string[][] GetScreen()
    {
        return Enumerable.Range(0, 6).Select(_ => Enumerable.Range(0, 40).Select(_ => ".").ToArray()).ToArray();
    }

    private static void PrintScreen(string[][] screen)
    {
        foreach (var row in screen)
        {
            Console.WriteLine(string.Join("", row));
        }
    }
}