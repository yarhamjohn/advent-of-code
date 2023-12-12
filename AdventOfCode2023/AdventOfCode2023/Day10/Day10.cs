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

        var labelledGrid = new string[grid.Length][];
        for (var row = 0; row < grid.Length; row++)
        {
            labelledGrid[row] = Enumerable.Range(0, grid.Length).Select(x => ".").ToArray();
        }        
        
        // replace each number with ^ < > v to indicate the edge (pointing outwards). Do I need to care about corners or can they be *?
        // if num is on edge, must point outwards. Somehow then follow it round...?
        for (var row = 0; row < scoredGrid.Length; row++)
        {
            for (var col = 0; col < scoredGrid.Length; col++)
            {
                if (int.TryParse(scoredGrid[row][col], out _))
                {
                    labelledGrid[row][col] = "*";
                }
            }
        }        
        
        PrintGrid(labelledGrid);
        Console.WriteLine();
        
        // Put first x values in round edges
        var locations = new List<(int row, int col)>();
        for (var row = 0; row < scoredGrid.Length; row++)
        {
            for (var col = 0; col < scoredGrid.Length; col++)
            {
                if (scoredGrid[row][col] == ".")
                {
                    if (row == 0 || row == grid.Length - 1 || col == 0 || col == grid.Length - 1)
                    {
                        labelledGrid[row][col] = "x";
                        locations.Add((row, col));
                    }
                }
            }
        }        
        
        PrintGrid(labelledGrid);
        Console.WriteLine();

        // Label all neighbouring xs
        while (locations.Count != 0)
        {
            var location = locations.First();
            if (location.row > 0 && labelledGrid[location.row - 1][location.col] == ".")
            {
                labelledGrid[location.row - 1][location.col] = "x";
                locations.Add((location.row - 1, location.col));
            }
            
            if (location.row < grid.Length - 1 && labelledGrid[location.row + 1][location.col] == ".")
            {
                labelledGrid[location.row + 1][location.col] = "x";
                locations.Add((location.row + 1, location.col));
            }
            
            if (location.col > 0 && labelledGrid[location.row][location.col - 1] == ".")
            {
                labelledGrid[location.row][location.col - 1] = "x";
                locations.Add((location.row, location.col - 1));
            }
            
            if (location.col < grid.Length - 1 && labelledGrid[location.row][location.col + 1] == ".")
            {
                labelledGrid[location.row][location.col + 1] = "x";
                locations.Add((location.row, location.col + 1));
            }

            locations.Remove(location);
        }
        
        PrintGrid(labelledGrid);
        Console.WriteLine();
        
        // foreach remaining ".", check up for ^, down for v, left for < and right for >
        // If found, it must be outside, so level "x" and add to list
        // while there are elements in the list, check all neighbours for "."s, then update and add to array again
        
        
        return labelledGrid.SelectMany(x => x.Where(y => y == ".")).Count();
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