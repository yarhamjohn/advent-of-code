namespace AdventOfCode.Day2;

public static class Day2
{
    public static int CalculatePosition(IEnumerable<string> input)
    {
        var (horizontal, depth) = input.Aggregate((horizontal: 0, depth: 0), (current, next) =>
        {
            var (direction, distance) = Parse(next);
            return direction switch
            {
                "forward" => (current.horizontal += distance, current.depth),
                "up" => (current.horizontal, current.depth -= distance),
                "down" => (current.horizontal, current.depth += distance),
                _ => throw new Exception("Not a valid direction")
            };
        });

        return horizontal * depth;
    }
    
    public static int CalculatePositionWithAim(IEnumerable<string> input)
    {
        var (horizontal, depth, _) = input.Aggregate((horizontal: 0, depth: 0, aim: 0), (current, next) =>
        {
            var (direction, distance) = Parse(next);
            return direction switch
            {
                "forward" => (current.horizontal += distance, current.depth += current.aim * distance, current.aim),
                "up" => (current.horizontal, current.depth, current.aim -= distance),
                "down" => (current.horizontal, current.depth, current.aim += distance),
                _ => throw new Exception("Not a valid direction")
            };
        });

        return horizontal * depth;
    }
    
    private static (string direction, int distance) Parse(string input) =>
        (input.Split(" ").First(), Convert.ToInt32(input.Split(" ").Last()));
}
