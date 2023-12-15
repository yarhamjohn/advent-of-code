namespace AdventOfCode2023.Day11;

public static class Day11
{
    public static long SumPathLengths(string[] input)
    {
        foreach (var line in input)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine();
        
        var grid = ExpandGridRows(input);

        foreach (var line in grid)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine();

        var finalGrid = ExpandGridColumns(input, grid);

        foreach (var line in finalGrid)
        {
            Console.WriteLine(string.Join("", line.Select(x => x.ToString())));
        }
        Console.WriteLine();

        var galaxyLocations = GetGalaxyLocations(finalGrid).ToArray();
        var pairs = new List<((int row, int col) from, (int row, int col) to)>();
        for (var i = 0; i < galaxyLocations.Length; i++)
        {
            var targets = galaxyLocations[(i + 1)..];
            var start = Enumerable.Range(0, targets.Length).Select(_ => galaxyLocations[i]);
            pairs.AddRange(start.Zip(targets));
        }

        foreach (var x in pairs)
        {
            Console.WriteLine(x);
        }
        Console.WriteLine();

        var result = 0;
        foreach (var location in galaxyLocations)
        {
            var scoredGrid = GetScoredGrid(finalGrid, location);
            var scoredLocations = pairs.Where(x => x.from == location);
            foreach (var l in scoredLocations)
            {
                result += (int) scoredGrid[l.to.row][l.to.col]!;
            }
        }

        return result;
    }

    private static int?[][] GetScoredGrid(List<char>[] finalGrid, (int row, int col) location)
    {
        var scoredGrid = finalGrid.Select(x => x.Select(_ => (int?) null).ToArray()).ToArray();

        var count = 0;
        scoredGrid[location.row][location.col] = count;
        
        var nextLocations = GetNextLocations(scoredGrid, location);
        while (nextLocations.Any())
        {
            count++;
            foreach (var loc in nextLocations)
            {
                scoredGrid[loc.row][loc.col] = count;
            }

            nextLocations = GetNextLocations(scoredGrid, location);
        }

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

        if (location.col < scoredGrid[0].Length)
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