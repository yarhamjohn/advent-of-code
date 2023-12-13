namespace AdventOfCode2023.Day10;

public static class Day10
{
    public static long CountSteps(string[] input)
    {
        var grid = ParseInput(input);
        PrintGrid(grid);
        Console.WriteLine();

        var start = GetStartPosition(grid);
        Console.WriteLine($"Start position: {start}");
        Console.WriteLine();
        
        var scoredGrid = ScoreGrid(grid, start);
        PrintGrid(scoredGrid);
        Console.WriteLine();
        
        return GetMaxScore(scoredGrid);
    }

    public static long CountInternalSpaces(string[] input)
    {
        var grid = ParseInput(input);
        PrintGrid(grid);
        Console.WriteLine();

        var start = GetStartPosition(grid);
        Console.WriteLine($"Start position: {start}");
        Console.WriteLine();

        var scoredGrid = ScoreGrid(grid, start);
        PrintGrid(scoredGrid);
        Console.WriteLine();

        var labelledGrid = GetLabelledGrid(start, grid);
        PrintGrid(labelledGrid);
        Console.WriteLine();

        // Put first O values in round edges
        var locations = new List<(int row, int col)>();
        for (var row = 0; row < scoredGrid.Length; row++)
        {
            for (var col = 0; col < scoredGrid.Length; col++)
            {
                if (scoredGrid[row][col] == ".")
                {
                    if (row == 0 || row == grid.Length - 1 || col == 0 || col == grid.Length - 1)
                    {
                        labelledGrid[row][col] = "O";
                        locations.Add((row, col));
                    }
                }
            }
        }

        PrintGrid(labelledGrid);
        Console.WriteLine();

        // Label all neighbouring 0s
        while (locations.Count != 0)
        {
            var location = locations.First();
            if (location.row > 0 && labelledGrid[location.row - 1][location.col] == ".")
            {
                labelledGrid[location.row - 1][location.col] = "O";
                locations.Add((location.row - 1, location.col));
            }

            if (location.row < grid.Length - 1 && labelledGrid[location.row + 1][location.col] == ".")
            {
                labelledGrid[location.row + 1][location.col] = "O";
                locations.Add((location.row + 1, location.col));
            }

            if (location.col > 0 && labelledGrid[location.row][location.col - 1] == ".")
            {
                labelledGrid[location.row][location.col - 1] = "O";
                locations.Add((location.row, location.col - 1));
            }

            if (location.col < grid.Length - 1 && labelledGrid[location.row][location.col + 1] == ".")
            {
                labelledGrid[location.row][location.col + 1] = "O";
                locations.Add((location.row, location.col + 1));
            }

            locations.Remove(location);
        }

        PrintGrid(labelledGrid);
        Console.WriteLine();

        // foreach remaining ".", check up for ^, down for v, left for < and right for >
        // If found, it must be outside, so level "x" and add to list
        // while there are elements in the list, check all neighbours for "."s, then update and add to array again
        var locs = new List<(int row, int col)>();
        for (var row = 0; row < labelledGrid.Length; row++)
        {
            for (var col = 0; col < labelledGrid.Length; col++)
            {
                if (labelledGrid[row][col] == ".")
                {
                    locs.Add((row, col));
                }
            }
        }

        var len = locs.Count;
        while (locs.Count != 0)
        {
            var (row, col) = locs.First();
            if (labelledGrid[row - 1][col] is "v" or "O" ||
                labelledGrid[row + 1][col] is "^" or "O" ||
                labelledGrid[row][col - 1] is ">" or "O" ||
                labelledGrid[row][col + 1] is "<" or "O")
            {
                labelledGrid[row][col] = "O";
                locs.Remove((row, col));
                len--;
                continue;
            }
            
            if (labelledGrid[row - 1][col] is "^" or "I" ||
                labelledGrid[row + 1][col] is "v" or "I" ||
                labelledGrid[row][col - 1] is "<" or "I" ||
                labelledGrid[row][col + 1] is ">" or "I")
            {
                labelledGrid[row][col] = "I";
                locs.Remove((row, col));
                continue;
            }
            
            if (labelledGrid[row - 1][col].Contains("v") ||
                labelledGrid[row + 1][col].Contains("^") ||
                labelledGrid[row][col - 1].Contains(">") ||
                labelledGrid[row][col + 1].Contains("<"))
            {
                labelledGrid[row][col] = "O";
                locs.Remove((row, col));
                continue;
            }
            
            if (labelledGrid[row - 1][col].Contains("^") ||
                labelledGrid[row + 1][col].Contains("v") ||
                labelledGrid[row][col - 1].Contains("<") ||
                labelledGrid[row][col + 1].Contains("^"))
            {
                labelledGrid[row][col] = "I";
                locs.Remove((row, col));
                continue;
            }
        }
        
        PrintGrid(labelledGrid);
        Console.WriteLine();
        
        return labelledGrid.SelectMany(x => x.Where(y => y == "I")).Count();
    }

    private static string[][] GetLabelledGrid((int row, int col) start, char[][] grid)
    {
        var labelledGrid = CreateLabelledGrid(grid, start);

        return RelabelGrid(labelledGrid);
    }

    private static string[][] RelabelGrid(string[][] labelledGrid)
    {
        // convert to ^ v < > symbols (and corners)
        (int row, int col) topFlatPipe = (0, 0);
        bool found = false;
        for (var row = 0; row < labelledGrid.Length; row++)
        {
            for (var col = 0; col < labelledGrid.Length; col++)
            {
                if (labelledGrid[row][col] == "-")
                {
                    topFlatPipe = (row, col);
                    found = true;
                    break;
                }
            }
            
            if (found)
            {            
                break;
            }
        }

        labelledGrid[topFlatPipe.row][topFlatPipe.col] = "^";
        var externalDirection = "up";
        var lastMove = "right";
        var nextLocation = topFlatPipe with { col = topFlatPipe.col + 1 };
        while (labelledGrid[nextLocation.row][nextLocation.col] != "^")
        {
            var nextSymbol = labelledGrid[nextLocation.row][nextLocation.col];
            if (nextSymbol == ".")
            {
                throw new ArgumentException("something went wrong - not in the loop any more");
            }
            
            if (externalDirection == "up")
            {
                if (nextSymbol == "-")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "^";
                    nextLocation = lastMove == "right" ? nextLocation with { col = nextLocation.col + 1 } : nextLocation with { col = nextLocation.col - 1 };
                    continue;
                }
                
                if (nextSymbol == "7")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "^>";
                    nextLocation = (row: nextLocation.row + 1, col: nextLocation.col);
                    externalDirection = "right";
                    lastMove = "down";
                    continue;
                }
                
                if (nextSymbol == "J")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "^<";
                    nextLocation = (row: nextLocation.row - 1, col: nextLocation.col);
                    externalDirection = "left";
                    lastMove = "up";
                    continue;
                }
                
                if (nextSymbol == "L")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "^>";
                    nextLocation = (row: nextLocation.row - 1, col: nextLocation.col);
                    externalDirection = "right";
                    lastMove = "up";
                    continue;
                }
                
                if (nextSymbol == "F")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "^<";
                    nextLocation = (row: nextLocation.row + 1, col: nextLocation.col);
                    externalDirection = "left";
                    lastMove = "down";
                    continue;
                }
            }

            if (externalDirection == "down")
            {
                if (nextSymbol == "-")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "v";
                    nextLocation = lastMove == "right" ? nextLocation with { col = nextLocation.col + 1 } : nextLocation with { col = nextLocation.col - 1 };
                    continue;
                }
                
                if (nextSymbol == "7")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "v<";
                    nextLocation = (row: nextLocation.row + 1, col: nextLocation.col);
                    externalDirection = "left";
                    lastMove = "down";
                    continue;
                }
                
                if (nextSymbol == "J")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "v>";
                    nextLocation = (row: nextLocation.row - 1, col: nextLocation.col);
                    externalDirection = "right";
                    lastMove = "up";
                    continue;
                }
                
                if (nextSymbol == "L")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "v<";
                    nextLocation = (row: nextLocation.row - 1, col: nextLocation.col);
                    externalDirection = "left";
                    lastMove = "up";
                    continue;
                }
                
                if (nextSymbol == "F")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "v>";
                    nextLocation = (row: nextLocation.row + 1, col: nextLocation.col);
                    externalDirection = "right";
                    lastMove = "down";
                    continue;
                }
            }
            
            if (externalDirection == "right")
            {
                if (nextSymbol == "|")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = ">";
                    nextLocation = lastMove == "down" ? nextLocation with { row = nextLocation.row + 1 } : nextLocation with { row = nextLocation.row - 1 };
                    continue;
                }
                
                if (nextSymbol == "7")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = ">^";
                    nextLocation = (row: nextLocation.row, col: nextLocation.col - 1);
                    externalDirection = "up";
                    lastMove = "left";
                    continue;
                }
                
                if (nextSymbol == "J")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = ">v";
                    nextLocation = (row: nextLocation.row, col: nextLocation.col - 1);
                    externalDirection = "down";
                    lastMove = "left";
                    continue;
                }
                
                if (nextSymbol == "L")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = ">^";
                    nextLocation = (row: nextLocation.row, col: nextLocation.col + 1);
                    externalDirection = "up";
                    lastMove = "right";
                    continue;
                }
                
                if (nextSymbol == "F")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = ">v";
                    nextLocation = (row: nextLocation.row, col: nextLocation.col + 1);
                    externalDirection = "down";
                    lastMove = "right";
                    continue;
                }
            }
            
            if (externalDirection == "left")
            {
                if (nextSymbol == "|")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "<";
                    nextLocation = lastMove == "down" ? nextLocation with { row = nextLocation.row + 1 } : nextLocation with { row = nextLocation.row - 1 };
                    continue;
                }
                
                if (nextSymbol == "7")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "<v";
                    nextLocation = (row: nextLocation.row, col: nextLocation.col - 1);
                    externalDirection = "down";
                    lastMove = "left";
                    continue;
                }
                
                if (nextSymbol == "J")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "<^";
                    nextLocation = (row: nextLocation.row, col: nextLocation.col - 1);
                    externalDirection = "up";
                    lastMove = "left";
                    continue;
                }
                
                if (nextSymbol == "L")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "<v";
                    nextLocation = (row: nextLocation.row, col: nextLocation.col + 1);
                    externalDirection = "down";
                    lastMove = "right";
                    continue;
                }
                
                if (nextSymbol == "F")
                {
                    labelledGrid[nextLocation.row][nextLocation.col] = "<^";
                    nextLocation = (row: nextLocation.row, col: nextLocation.col + 1);
                    externalDirection = "up";
                    lastMove = "right";
                    continue;
                }
            }
        }

        return labelledGrid;
    }

    private static string GetStartSymbol((int row, int col) start, char[][] grid)
    {
        char? above = start.row > 0 ? grid[start.row - 1][start.col] : null;
        char? below = start.row < grid.Length - 1 ? grid[start.row + 1][start.col] : null;
        char? left = start.col > 0 ? grid[start.row][start.col - 1] : null;
        char? right = start.col < grid.Length - 1 ? grid[start.row][start.col + 1] : null;
        
        if (left == '-' && right is '-' or '7' or 'J')
        {
            return "-";
        }
        
        if (right == '-' && left is '-' or 'F' or 'L')
        {
            return "-";
        }
        
        if (above == '|' && below is '|' or 'J' or 'L')
        {
            return "|";
        }
        
        if (below == '|' && above is '|' or 'F' or '7')
        {
            return "|";
        }
        
        if (above is '|' or 'F' or '7' && right is '-' or 'J' or '7')
        {
            return "L";
        }
        
        if (above is '|' or 'F' or '7' && left is '-' or 'F' or 'L')
        {
            return "J";
        }
        
        if (below is '|' or 'L' or 'J' && right is '-' or 'J' or '7')
        {
            return "F";
        }
        
        if (below is '|' or 'L' or 'J' && left is '-' or 'F' or 'L')
        {
            return "7";
        }
        
        throw new ArgumentException("Invalid start position");
    }
    
    private static string[][] CreateLabelledGrid(char[][] grid, (int row, int col) start)
    {
        var labelledGrid = new string[grid.Length][];
        for (var row = 0; row < grid.Length; row++)
        {
            labelledGrid[row] = Enumerable.Range(0, grid.Length).Select(x => ".").ToArray();
        }

        labelledGrid[start.row][start.col] = GetStartSymbol(start, grid);
        
        var nextLocations = GetNextLocations(grid, labelledGrid, start);
        while (nextLocations.Count != 0)
        {
            var locationsToAdd = new List<(int, int)>();
            foreach (var location in nextLocations)
            {
                labelledGrid[location.row][location.col] = grid[location.row][location.col].ToString();
                locationsToAdd.AddRange(GetNextLocations(grid, labelledGrid, location));
            }

            nextLocations = locationsToAdd;
        }

        return labelledGrid;
    }

    private static string[][] ScoreGrid(char[][] grid, (int row, int col) start)
    {
        var scoredGrid = new string[grid.Length][];
        for (var row = 0; row < grid.Length; row++)
        {
            scoredGrid[row] = Enumerable.Range(0, grid.Length).Select(x => ".").ToArray();
        }

        var distance = 0;
        scoredGrid[start.row][start.col] = distance.ToString();
        
        var nextLocations = GetNextLocations(grid, scoredGrid, start);
        while (nextLocations.Count != 0)
        {
            var locationsToAdd = new List<(int, int)>();
            foreach (var location in nextLocations)
            {
                scoredGrid[location.row][location.col] = (distance + 1).ToString();
                locationsToAdd.AddRange(GetNextLocations(grid, scoredGrid, location));
            }

            distance++;
            nextLocations = locationsToAdd;
        }

        return scoredGrid;
    }

    private static List<(int row, int col)> GetNextLocations(char[][] grid, string[][] scoredGrid, (int row, int col) location)
    {
        var nextLocations = new List<(int, int)>();
        
        var possibleLocations = new List<(int row, int col)>
        {
            (location.row - 1, location.col),
            (location.row + 1, location.col),
            (location.row, location.col - 1),
            (location.row, location.col + 1)
        };

        foreach (var loc in possibleLocations)
        {
            if (loc.row < 0 || loc.row >= grid.Length || loc.col < 0 || loc.col >= grid.Length)
            {
                continue;
            }
            
            if (scoredGrid[loc.row][loc.col] != ".")
            {
                continue;
            }
            
            var targetCh = grid[loc.row][loc.col];
            var startCh = grid[location.row][location.col];
            if (loc.row < location.row)
            {
                if (startCh is 'S' or '|' or 'J' or 'L')
                {
                    if (targetCh is '|' or '7' or 'F')
                    {
                        nextLocations.Add(loc);
                    }
                }
            }
            
            if (loc.row > location.row)
            {
                if (startCh is 'S' or '|' or 'F' or '7')
                {
                    if (targetCh is '|' or 'J' or 'L')
                    {
                        nextLocations.Add(loc);
                    }
                }
            }
            
            if (loc.col < location.col)
            {
                if (startCh is 'S' or '-' or 'J' or '7')
                {
                    if (targetCh is '-' or 'L' or 'F')
                    {
                        nextLocations.Add(loc);
                    }
                }
            }
            
            if (loc.col > location.col)
            {
                if (startCh is 'S' or '-' or 'F' or 'L')
                {
                    if (targetCh is '-' or 'J' or '7')
                    {
                        nextLocations.Add(loc);
                    }
                }
            }
        }

        return nextLocations;
    }

    private static long GetMaxScore(string[][] scoredGrid)
    {
        var maxScore = 0;
        for (var row = 0; row < scoredGrid.Length; row++)
        {
            for (var col = 0; col < scoredGrid.Length; col++)
            {
                if (int.TryParse(scoredGrid[row][col], out var num) && num > maxScore)
                {
                    maxScore = num;
                }
            }
        }

        return maxScore;
    }

    private static (int row, int col) GetStartPosition(char[][] grid)
    {
        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid.Length; col++)
            {
                if (grid[row][col] == 'S')
                {
                    return (row, col);
                }
            }
        }

        Console.WriteLine("Something went wrong: ");
        Console.WriteLine();
        PrintGrid(grid);
        Console.WriteLine();
        throw new ArgumentException("Grid had no start position");
    }

    private static char[][] ParseInput(string[] input)
    {
        return input.Select(x => x.ToCharArray()).ToArray();
    }
    
    private static void PrintGrid(char[][] grid)
    {
        foreach (var row in grid)
        {
            foreach (var col in row)
            {
                Console.Write(col);
            }
            Console.WriteLine();
        }
    }
    
    private static void PrintGrid(string[][] grid)
    {
        foreach (var row in grid)
        {
            foreach (var col in row)
            {
                Console.Write(col);
            }
            Console.WriteLine();
        }
    }
}