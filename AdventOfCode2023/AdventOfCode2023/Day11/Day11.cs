namespace AdventOfCode2023.Day11;

public static class Day11
{
    public static long SumPathLengths(string[] input)
    {
        // foreach (var line in input)
        // {
        //     Console.WriteLine(line);
        // }
        // Console.WriteLine();
        
        var grid = ExpandGridRows(input);

        // foreach (var line in grid)
        // {
        //     Console.WriteLine(line);
        // }
        // Console.WriteLine();

        var finalGrid = ExpandGridColumns(input, grid);

        // foreach (var line in finalGrid)
        // {
        //     Console.WriteLine(string.Join("", line.Select(x => x.ToString())));
        // }
        // Console.WriteLine();

        var galaxyLocations = GetGalaxyLocations(finalGrid).ToArray();
        Console.WriteLine($"Galaxy locations: {galaxyLocations.Length}");
        
        var pairs = GetGalaxyPairs(galaxyLocations);
        Console.WriteLine($"Galaxy pairs: {pairs.Count}");
        
        return SumDistances(galaxyLocations, finalGrid, pairs);
    }

    private static int SumDistances((int row, int col)[] galaxyLocations, List<char>[] finalGrid, List<((int row, int col) from, (int row, int col) to)> pairs)
    {
        var result = 0;
        for (var i = 0; i < galaxyLocations.Length; i++)
        {
            var scoredGrid = GetScoredGrid(finalGrid, galaxyLocations[i]);
            // foreach (var line in scoredGrid)
            // {
            //     Console.WriteLine(string.Join("", line.Select(x => x?.ToString() ?? "N")));
            // }
            // Console.WriteLine();
            
            var scoredLocations = pairs.Where(x => x.from == galaxyLocations[i]);
            Console.WriteLine($"Calculating distance for galaxy #{i}: {galaxyLocations[i]}. There are {scoredLocations.Count()} locations. Current score: {result}");
            result += scoredLocations.Sum(l => (int)scoredGrid[l.to.row][l.to.col]!);
        }

        return result;
    }

    private static List<((int row, int col) from, (int row, int col) to)> GetGalaxyPairs((int row, int col)[] galaxyLocations)
    {
        var pairs = new List<((int row, int col) from, (int row, int col) to)>();
        for (var i = 0; i < galaxyLocations.Length; i++)
        {
            var targets = galaxyLocations[(i + 1)..];
            var start = Enumerable.Range(0, targets.Length).Select(_ => galaxyLocations[i]);
            pairs.AddRange(start.Zip(targets));
        }

        return pairs;
    }

    private static int?[][] GetScoredGrid(List<char>[] finalGrid, (int row, int col) location)
    {
        var scoredGrid = finalGrid.Select(x => x.Select(_ => (int?) null).ToArray()).ToArray();

        var count = 0;
        scoredGrid[location.row][location.col] = count;

        // Go right and down
        for (var startRow = location.row; startRow < scoredGrid.Length; startRow++)
        {
            for (var startCol = location.col; startCol < scoredGrid[0].Length; startCol++)
            {
                scoredGrid[startRow][startCol] = startCol - location.col + count;
            }

            count++;
        }

        count = 0;
        // Go right and up
        for (var startRow = location.row; startRow >= 0; startRow--)
        {
            for (var startCol = location.col; startCol < scoredGrid[0].Length; startCol++)
            {
                scoredGrid[startRow][startCol] = startCol - location.col + count;
            }

            count++;
        }
        
        count = 0;
        // Go left and up
        for (var startRow = location.row; startRow >= 0; startRow--)
        {
            for (var startCol = location.col; startCol >= 0; startCol--)
            {
                scoredGrid[startRow][startCol] = location.col - startCol + count;
            }

            count++;
        }
        
        count = 0;
        // Go left and down
        for (var startRow = location.row; startRow < scoredGrid.Length; startRow++)
        {
            for (var startCol = location.col; startCol >= 0; startCol--)
            {
                scoredGrid[startRow][startCol] = location.col - startCol + count;
            }

            count++;
        }
        //
        // var nextLocations = GetNextLocations(scoredGrid, location);
        // while (nextLocations.Any())
        // {
        //     var locs = nextLocations.Select(x => x).ToArray();
        //     nextLocations.Clear();
        //     
        //     count++;
        //     foreach (var loc in locs)
        //     {
        //         scoredGrid[loc.row][loc.col] = count;
        //         nextLocations.AddRange(GetNextLocations(scoredGrid, loc));
        //     }
        //
        // }

        return scoredGrid;
    }

    private static List<(int row, int col)> GetNextLocations(int?[][] scoredGrid, (int row, int col) location)
    {
        var nextLocations = new List<(int row, int col)>();
        if (location.row > 0)
        {
            if (scoredGrid[location.row - 1][location.col] == null)
            {
                nextLocations.Add((location.row - 1, location.col));
            }
        }

        if (location.row < scoredGrid.Length - 1)
        {
            if (scoredGrid[location.row + 1][location.col] == null)
            {
                nextLocations.Add((location.row + 1, location.col));
            }
        }

        if (location.col > 0)
        {
            if (scoredGrid[location.row][location.col - 1] == null)
            {
                nextLocations.Add((location.row, location.col - 1));
            }
        }

        if (location.col < scoredGrid[0].Length - 1)
        {
            if (scoredGrid[location.row][location.col + 1] == null)
            {
                nextLocations.Add((location.row, location.col + 1));
            }
        }

        return nextLocations;
    }

    private static IEnumerable<(int row, int col)> GetGalaxyLocations(List<char>[] finalGrid)
    {
        for (var row = 0; row < finalGrid.Length; row++)
        {
            for (var col = 0; col < finalGrid[0].Count; col++)
            {
                if (finalGrid[row][col] == '#')
                {
                    yield return (row, col);
                }
            }
        }
    }

    private static List<char>[] ExpandGridColumns(string[] input, List<string> grid)
    {
        var finalGrid = Enumerable.Range(0, grid.Count).Select(_ => new List<char>()).ToArray();

        // expand cols
        for (var col = 0; col < input.Length; col++)
        {
            var wholeCol = grid.Select(row => row[col]).ToArray();
            for (var row = 0; row < grid.Count; row++)
            {
                finalGrid[row].Add(grid[row][col]);

                if (wholeCol.All(x => x == '.'))
                {
                    finalGrid[row].Add('.');
                }
            }
        }

        return finalGrid;
    }

    private static List<string> ExpandGridRows(string[] input)
    {
        var grid = new List<string>();

        // expand rows
        foreach (var line in input)
        {
            grid.Add(line);
            
            if (line.All(x => x == '.'))
            {
                grid.Add(line);
            }
        }

        return grid;
    }
}