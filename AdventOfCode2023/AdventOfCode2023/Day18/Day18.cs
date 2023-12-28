using System.Security.Principal;

namespace AdventOfCode2023.Day18;

public static class Day18
{
    public static long CalculateLagoonSize(string[] input)
    {
        var parsedInput = ParseInput(input);
        
        var edgePositions = GetRelativeEdgePositions(parsedInput);
        
        edgePositions = NormalisePositions(edgePositions);
        // Console.WriteLine($"{edgePositions.Length}: {edgePositions.Select(x => x.row).Distinct().Count()}");

        return Calculate2(edgePositions);
    }

    public static long CalculateGiantLagoonSize(string[] input)
    {
        var parsedInput = ParseGiantInput(input);
        
        var edgePositions = GetRelativeEdgePositions(parsedInput);
        
        edgePositions = NormalisePositions(edgePositions);
        // Console.WriteLine($"{edgePositions.Length} positions: {edgePositions.Select(x => x.row).Distinct().Count()} rows");

        return Calculate2(edgePositions);
    }

    private static long Calculate2((int row, int col)[] edgePositions)
    {
        var num = 0L;

        var rows = edgePositions.Select(x => x.row).Distinct().ToArray();
        for (var i = 0; i < rows.Length; i++)
        {
            if (rows.Length % 50000 == 0)
            {
                Console.WriteLine($"{rows.Length - i} rows remaining");
            }
            
            var inGrp = false;
            var startGrp = false;
            var multiEdge = false;
            var startJoin = "";
            
            var runningTotal = 0L;
            
            var cols = edgePositions.Where(x => x.row == rows[i]).Select(x => x.col).ToArray();
            // Console.WriteLine($"{i}: {rows.Length} rows, {cols.Length} cols");

            var startCol = 0;
            for (var j = 0; j < cols.Length; j++)
            {
                if (j == cols.Length - 1)
                {
                    runningTotal += Math.Abs(cols[j] - (startCol - 1));
                    break;
                }
                
                // if not previously in a group, its the start of a block
                if (!inGrp)
                {
                    startCol = cols[j];
                    inGrp = true;
                    startGrp = false;

                    // If its a multi-thick wall
                    if (cols[j + 1] == cols[j] + 1)
                    {
                        multiEdge = true;
                        if (edgePositions.Contains((i + 1, j)))
                        {
                            startJoin = "D";
                        }
                        else if (edgePositions.Contains((i - 1, j)))
                        {
                            startJoin = "U";
                        }
                    }

                    continue;
                }

                // Leaving a multi-thick wall
                if (multiEdge && cols[j + 1] != cols[j] + 1)
                {
                    multiEdge = false;
                    
                    // Determine whether we are still in a group or not
                    if (edgePositions.Contains((i + 1, j)) && startJoin == "D")
                    {
                        inGrp = startGrp;
                    }
                    else if (edgePositions.Contains((i - 1, j)) && startJoin == "U")
                    {
                        inGrp = startGrp;
                    }
                    else
                    {
                        inGrp = !startGrp;
                    }

                    // Add block length to running total
                    if (!inGrp)
                    {
                        runningTotal += Math.Abs(cols[j] - (startCol - 1));
                    }
                    
                    continue;
                }
                
                // Leaving a single-thick wall
                if (cols[j + 1] != cols[j] + 1)
                {
                    inGrp = false;
                    runningTotal += Math.Abs(cols[j] - (startCol - 1));
                    continue;
                }
                
                // Entering a multi-thick wall whilst in group
                if (cols[j + 1] == cols[j] + 1)
                {
                    startGrp = true;
                    multiEdge = true;
                    if (edgePositions.Contains((i + 1, j)))
                    {
                        startJoin = "D";
                    }
                    else if (edgePositions.Contains((i - 1, j)))
                    {
                        startJoin = "U";
                    }
                }
            }
            
            num += runningTotal;
            // Console.WriteLine(num);
        }
        
        return num;
    }

    private static (int row, int col)[] NormalisePositions((int row, int col)[] edgePositions)
    {
        edgePositions = edgePositions.Order().ToArray();

        // Assumes min is negative and add buffer so top/bottom row and left/right cols are all empty
        var minRow = edgePositions.Min(x => x.row) - 1; 
        var minCol = edgePositions.Min(x => x.col) - 1;
        
        return edgePositions.Select(x => (x.row - minRow, x.col - minCol)).ToArray();
    }

    private static (int row, int col)[] GetRelativeEdgePositions(IEnumerable<(string direction, long count)> parsedInput)
    {
        var positions = new List<(int row, int col)>();
        var currentPosition = (row: 0, col: 0);
        foreach (var x in parsedInput)
        {
            for (var i = 0; i < x.count; i++)
            {
                switch (x.direction)
                {
                    case "R":
                    {
                        currentPosition = (currentPosition.row, currentPosition.col + 1);
                        positions.Add(currentPosition);
                        break;
                    }
                    case "L":
                    {
                        currentPosition = (currentPosition.row, currentPosition.col - 1);
                        positions.Add(currentPosition);
                        break;
                    }
                    case "U":
                    {
                        currentPosition = (currentPosition.row - 1, currentPosition.col);
                        positions.Add(currentPosition);
                        break;
                    }
                    case "D":
                    {
                        currentPosition = (currentPosition.row + 1, currentPosition.col);
                        positions.Add(currentPosition);
                        break;
                    }
                }
            }
        }

        return positions.ToArray();
    }

    private static long Calculate((int row, int col)[] orderedPositions)
    {
        var num = 0L;
        
        var min = (orderedPositions.Min(x => x.row), orderedPositions.Min(x => x.col));
        var max = (orderedPositions.Max(x => x.row), orderedPositions.Max(x => x.col));

        for (var i = min.Item1; i <= max.Item1; i++)
        {
            var inGrp = false;
            var startGrp = false;
            var multiEdge = false;
            var startJoin = "";
            
            for (var j = min.Item2; j <= max.Item2; j++)
            {
                if (orderedPositions.Contains((i, j)))
                {
                    num++;
                }
                else if (inGrp)
                {
                    num++;
                }
                else
                {
                    // do nothing
                }

                // Not previously in a group
                if (!inGrp)
                {
                    // Not currently on a wall
                    if (!orderedPositions.Contains((i, j)))
                    {
                        continue;
                    }

                    // Now on a single-thick wall
                    if (orderedPositions.Contains((i, j)) && !orderedPositions.Contains((i, j + 1)))
                    {
                        inGrp = true;
                        continue;
                    }

                    // Now on a multi-thick wall
                    multiEdge = true;
                    if (orderedPositions.Contains((i + 1, j)))
                    {
                        startJoin = "D";
                    }
                    else if (orderedPositions.Contains((i - 1, j)))
                    {
                        startJoin = "U";
                    }

                    inGrp = true;
                    startGrp = false;
                    
                    continue;
                }
                
                if (multiEdge)
                {
                    // still in a multi-thick wall
                    if (orderedPositions.Contains((i, j)) && orderedPositions.Contains((i, j + 1)))
                    {
                        continue;
                    }
                    
                    // leaving a multi-thick wall
                    if (orderedPositions.Contains((i, j)) && !orderedPositions.Contains((i, j + 1)))
                    {
                        multiEdge = false;
                        
                        // Determine whether we are still in a group or not
                        if (orderedPositions.Contains((i + 1, j)) && startJoin == "D")
                        {
                            inGrp = startGrp;
                        }
                        else if (orderedPositions.Contains((i - 1, j)) && startJoin == "U")
                        {
                            inGrp = startGrp;
                        }
                        else
                        {
                            inGrp = !startGrp;
                        }   
                        
                        continue;
                    }
                }

                // In group and now on a single-thick wall
                if (orderedPositions.Contains((i, j)) && !orderedPositions.Contains((i, j + 1)))
                {
                    inGrp = false;
                    continue;
                }
                
                // In group and now on a multi-thick wall
                if (orderedPositions.Contains((i, j)) && orderedPositions.Contains((i, j + 1)))
                {
                    startGrp = true;
                    multiEdge = true;
                    if (orderedPositions.Contains((i + 1, j)))
                    {
                        startJoin = "D";
                    }
                    else if (orderedPositions.Contains((i - 1, j)))
                    {
                        startJoin = "U";
                    }
                }
            }
        }
        
        return num;
    }
    
    private static IEnumerable<(string direction, long count)> ParseInput(string[] input)
    {
        return input
            .Select(line => line.Split(" "))
            .Select(splitLine => (
                splitLine[0], 
                long.Parse(splitLine[1])));
    }
    
    private static IEnumerable<(string direction, long count)> ParseGiantInput(string[] input)
    {
        var enumerable = input
            .Select(line => line.Split(" ")[2].Replace("(", "").Replace(")", ""));
        
        return enumerable
            .Select(hex =>
            {
                var direction = hex.Last() == '0' ? "R" : hex.Last() == '1' ? "D" : hex.Last() == '2' ? "L" : "U";
                var int64 = Convert.ToInt64(hex[1..^1], 16);
                return (
                    direction,
                    int64);
            });
    }
}