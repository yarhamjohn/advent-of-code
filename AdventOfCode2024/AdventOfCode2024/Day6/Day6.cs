namespace AdventOfCode2024.Day6;

public static class Day6
{
    public static int Part1(string[] input)
    {
        var position = GetPosition(input);

        List<(int row, int col)> visitedLocations = [(position.row, position.col)];
        
        while (true)
        {
            if (position.direction == "^")
            {
                var nextPos = (position.row - 1, position.col);

                if (nextPos.Item1 < 0)
                {
                    break;
                }

                if (input[nextPos.Item1][nextPos.Item2] == '#')
                {
                    if (nextPos.Item2 + 1 == input.First().Length)
                    {
                        break;
                    }
                    
                    position = (">", position.row, position.col);

                    continue;
                }

                position = ("^", nextPos.Item1, nextPos.Item2);
            }
            else if (position.direction == ">")
            {
                var nextPos = (position.row, position.col + 1);

                if (nextPos.Item2 == input.First().Length)
                {
                    break;
                }

                if (input[nextPos.Item1][nextPos.Item2] == '#')
                {
                    if (nextPos.Item1 + 1 == input.Length)
                    {
                        break;
                    }
                    
                    position = ("v", position.row, position.col);

                    continue;
                }

                position = (">", nextPos.Item1, nextPos.Item2);
            }
            else if (position.direction == "v")
            {
                var nextPos = (position.row + 1, position.col);

                if (nextPos.Item1 == input.Length)
                {
                    break;
                }

                if (input[nextPos.Item1][nextPos.Item2] == '#')
                {
                    if (nextPos.Item2 < 0)
                    {
                        break;
                    }
                    
                    position = ("<", position.row, position.col);

                    continue;
                }

                position = ("v", nextPos.Item1, nextPos.Item2);
            }
            else 
            {
                var nextPos = (position.row, position.col - 1);

                if (nextPos.Item1 < 0)
                {
                    break;
                }

                if (input[nextPos.Item1][nextPos.Item2] == '#')
                {
                    if (nextPos.Item1 < 0)
                    {
                        break;
                    }
                    
                    position = ("^", position.row, position.col);

                    continue;
                }

                position = ("<", nextPos.Item1, nextPos.Item2);
            }
            
            visitedLocations.Add((position.row, position.col));
        }
        
        return visitedLocations.Distinct().Count();
    }

    public static int Part2(string[] input)
    {
        return 0;
    }
    
    private static (string direction, int row, int col) GetPosition(string[] input)
    {
        string[] options = ["^", ">", "v", "<"];

        for (var row = 0; row < input.Length; row++)
        {
            if (options.All(x => !input[row].Contains(x)))
            {
                continue;
            }

            var location = options
                .Select((x, i) => (direction: options[i], col: input[row].IndexOf(x, StringComparison.Ordinal)))
                .MaxBy(x => x.Item2);
                
            return (location.direction, row, location.col);
        }
        
        throw new InvalidOperationException("No starting position found");
    }
}