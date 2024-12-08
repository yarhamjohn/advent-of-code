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
        var position = GetPosition(input);
        var blockers = GetBlockers(input);

        List<(string direction, int row, int col)> visitedLocations = [position];
        List<(int row, int col)> blockPositions = [];
        
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

                // If crossing a row/col where to the right we have been before (before a block is reached)
                // Not necessarily that we have been to that block itself before.
                var loopableLocations = visitedLocations.Where(loc =>
                    loc.direction == ">" && loc.row == position.row && loc.col >= position.col);

                var b = blockers.Where(b => b.row == position.row && b.col > position.col);
                var firstBlocker = b.Any() ? b.MinBy(z => z.col).col : input.First().Length;
                
                if (loopableLocations.Any(abc => abc.col < firstBlocker))
                {
                    if (position.row > 0)
                    {
                        blockPositions.Add((position.row - 1, position.col));
                    }
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

                var loopableLocations = visitedLocations.Where(loc =>
                    loc.direction == "v" && loc.row >= position.row && loc.col == position.col);
                
                var b = blockers.Where(b => b.row > position.row && b.col == position.col);
                var firstBlocker = b.Any() ? b.MinBy(z => z.row).row : input.Length;
                
                if (loopableLocations.Any(abc => abc.row < firstBlocker))
                {
                    if (position.col < input.First().Length - 1)
                    {
                        blockPositions.Add((position.row, position.col + 1));
                    }
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

                var loopableLocations = visitedLocations.Where(loc =>
                    loc.direction == "<" && loc.row == position.row && loc.col <= position.col);
                
                var b = blockers.Where(b => b.row == position.row && b.col < position.col);
                var firstBlocker = b.Any() ? b.MaxBy(z => z.col).col : -1;
                
                if (loopableLocations.Any(abc => abc.col > firstBlocker))
                {
                    if (position.row < input.Length - 1)
                    {
                        blockPositions.Add((position.row + 1, position.col));
                    }
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

                var loopableLocations = visitedLocations.Where(loc =>
                    loc.direction == "^" && loc.row <= position.row && loc.col == position.col);
                
                var b = blockers.Where(b => b.row > position.row && b.col == position.col);
                var firstBlocker = b.Any() ? b.MaxBy(z => z.row).row : -1;
                
                if (loopableLocations.Any(abc => abc.row > firstBlocker))
                {
                    if (position.col > 0)
                    {
                        blockPositions.Add((position.row, position.col - 1));
                    }
                }

                position = ("<", nextPos.Item1, nextPos.Item2);
            }
            
            visitedLocations.Add(position);
        }
        
        return blockPositions.Distinct().Count();
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
    
    private static List<(int row, int col)> GetBlockers(string[] input)
    {
        var blockers = new List<(int row, int col)>();
        
        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] == '#')
                {
                    blockers.Add((row, col));
                }
            }
        }

        return blockers;
    }
}