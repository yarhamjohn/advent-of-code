namespace AdventOfCode2023.Day2;

public static class Day2
{
    private static readonly Dictionary<string, int> Limits = new()
    {
        { "red", 12 },
        { "green", 13 },
        { "blue", 14 }
    };
    
    public static int GetGameIdSum(IEnumerable<string> input)
    {
        return ParseInput(input).Sum(game => IsValidGame(game.Value) ? game.Key : 0);
    }

    private static bool IsValidGame(IEnumerable<Dictionary<string, int>> draws)
    {
        return draws.All(draw => draw.All(cube => Limits[cube.Key] >= cube.Value));
    }

    public static int GetPowerCubeSum(IEnumerable<string> input)
    {
        var parsedInput = ParseInput(input);

        var result = 0;
        foreach (var (_, draws) in parsedInput)
        {
            var minRed = 0;
            var minGreen = 0;
            var minBlue = 0;

            foreach (var draw in draws)
            {
                if (draw.TryGetValue("red", out var redCount) && redCount > minRed)
                {
                    minRed = redCount;
                }

                if (draw.TryGetValue("green", out var greenCount) && greenCount > minGreen)
                {
                    minGreen = greenCount;
                }

                if (draw.TryGetValue("blue", out var blueCount) && blueCount > minBlue)
                {
                    minBlue = blueCount;
                }
            }

            result += minRed * minGreen * minBlue;
        }
        
        return result;
    }

    private static Dictionary<int, List<Dictionary<string, int>>> ParseInput(IEnumerable<string> input)
    {
        var parsedInput = new Dictionary<int, List<Dictionary<string, int>>>();

        foreach (var line in input)
        {
            var gameIdSplit = line.Split(":").Select(x => x.Trim()).ToArray();
            
            var gameNum = int.Parse(gameIdSplit[0].Split(" ")[1].Trim());
            parsedInput.Add(gameNum, []);

            var drawSplits = gameIdSplit[1].Split(";").Select(x => x.Trim());
            foreach (var draw in drawSplits)
            {
                var drawDict = new Dictionary<string, int>();
                var cubeSplit = draw.Split(",").Select(x => x.Trim());
                foreach (var cube in cubeSplit)
                {
                    var elements = cube.Split(" ").Select(x => x.Trim()).ToArray();
                    var count = int.Parse(elements[0]);
                    var colour = elements[1];
                    
                    drawDict.Add(colour, count);
                }
                
                parsedInput[gameNum].Add(drawDict);
            }
        }

        return parsedInput;
    }
}