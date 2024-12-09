using System.Text;

namespace AdventOfCode2024.Day6;

public static class Day6
{
    public static int Part1(string[] input)
    {
        return GetVisitedLocations(input, GetStartPosition(input))
            .Select(x => (x.row, x.col))
            .Distinct()
            .Count();
    }

    public static int Part2(string[] input)
    {
        return GetLoopFormingBlockPositions(
            input, 
            GetVisitedLocations(input, GetStartPosition(input)), 
            GetStartPosition(input))
            .Count;
    }

    private static HashSet<(int row, int col)> GetLoopFormingBlockPositions(
        string[] input, HashSet<(string direction, int row, int col)> visitedLocations,
        (string direction, int row, int col) position)
    {
        var blockingPositions = new HashSet<(int row, int col)>();

        foreach (var location in visitedLocations)
        {
            if (location.row == position.row && location.col == position.col)
            {
                continue;
            }

            var newInput = new string[input.Length];
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < input.Length; i++)
            {
                if (i == location.row)
                {
                    stringBuilder = new StringBuilder(input[i]);
                    stringBuilder.Remove(location.col, 1);
                    stringBuilder.Insert(location.col, "#");
                    newInput[i] = stringBuilder.ToString();
                }
                else
                {
                    stringBuilder.Append(input[i]);
                    newInput[i] = stringBuilder.ToString();
                }
                
                stringBuilder.Clear();
            }
            
            if (FormsLoop(newInput, position))
            {
                blockingPositions.Add((location.row, location.col));
            }
        }

        return blockingPositions;
    }

    private static (string direction, int row, int col) GetStartPosition(string[] input)
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
    
    private static HashSet<(string direction, int row, int col)> GetVisitedLocations(string[] input, (string direction, int row, int col) position)
    {
        HashSet<(string direction, int row, int col)> visitedLocations = [position];

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
            
            visitedLocations.Add(position);
        }

        return visitedLocations;
    }

    private static bool FormsLoop(string[] input, (string direction, int row, int col) position)
    {
        HashSet<(string direction, int row, int col)> visitedLocations = [position];

        while (true)
        {
            switch (position.direction)
            {
                case "^":
                {
                    var nextPos = (position.row - 1, position.col);

                    if (nextPos.Item1 < 0)
                    {
                        return false;
                    }

                    if (input[nextPos.Item1][nextPos.Item2] == '#')
                    {
                        if (nextPos.Item2 + 1 == input.First().Length)
                        {
                            return false;
                        }
                    
                        position = (">", position.row, position.col);
                    }
                    else
                    {
                        position = ("^", nextPos.Item1, nextPos.Item2);
                    }

                    break;
                }
                case ">":
                {
                    var nextPos = (position.row, position.col + 1);

                    if (nextPos.Item2 == input.First().Length)
                    {
                        return false;
                    }

                    if (input[nextPos.Item1][nextPos.Item2] == '#')
                    {
                        if (nextPos.Item1 + 1 == input.Length)
                        {
                            return false;
                        }
                    
                        position = ("v", position.row, position.col);
                    }
                    else
                    {
                        position = (">", nextPos.Item1, nextPos.Item2);
                    }

                    break;
                }
                case "v":
                {
                    var nextPos = (position.row + 1, position.col);

                    if (nextPos.Item1 == input.Length)
                    {
                        return false;
                    }

                    if (input[nextPos.Item1][nextPos.Item2] == '#')
                    {
                        if (nextPos.Item2 < 0)
                        {
                            return false;
                        }
                    
                        position = ("<", position.row, position.col);
                    }
                    else
                    {
                        position = ("v", nextPos.Item1, nextPos.Item2);
                    }

                    break;
                }
                default:
                {
                    var nextPos = (position.row, position.col - 1);

                    if (nextPos.Item2 < 0)
                    {
                        return false;
                    }

                    if (input[nextPos.Item1][nextPos.Item2] == '#')
                    {
                        if (nextPos.Item1 < 0)
                        {
                            return false;
                        }
                    
                        position = ("^", position.row, position.col);
                    }
                    else
                    {
                        position = ("<", nextPos.Item1, nextPos.Item2);
                    }

                    break;
                }
            }
            
            if (visitedLocations.Contains(position) && visitedLocations.Count > 1)
            {
                return true;
            }

            visitedLocations.Add(position);
        }
    }
}