namespace AdventOfCode2023.Day11;

public static class Day11
{
    public static long SumPathLengths(string[] input, int howBig)
    {
        var grid = input.Select(line => line.ToCharArray()).ToArray();
        var galaxyLocations = GetGalaxyLocations(grid).ToArray();
        var galaxyPairs = GetGalaxyPairs(galaxyLocations, input);

        return SumDistances(galaxyLocations, grid, galaxyPairs, howBig);
    }

    private static IEnumerable<((int row, int col) from, (int row, int col) to, int numRows, int numCols)> GetGalaxyPairs(
        (int row, int col)[] galaxyLocations,
        IReadOnlyList<string> input)
    {
        var rowsToExpand = GetRowsToExpand(input);
        var colsToExpand = GetColsToExpand(input);
        
        foreach (var (from, to) in GetGalaxyPairs(galaxyLocations))
        {
            var numRows = rowsToExpand.Count(i => i > from.row && i < to.row || i < from.row && i > to.row);
            var numCols = colsToExpand.Count(i => i > from.col && i < to.col || i < from.col && i > to.col);
            
            yield return (from, to, numRows, numCols);
        }
    }

    private static List<int> GetColsToExpand(IReadOnlyCollection<string> input)
    {
        var result = new List<int>();
        for (var col = 0; col < input.Count; col++)
        {
            if (input.Select(row => row[col]).All(x => x == '.'))
            {
                result.Add(col);
            }
        }

        return result;
    }

    private static List<int> GetRowsToExpand(IReadOnlyList<string> input)
    {
        return input
            .Select((line, idx) => (line, idx))
            .Where(pair => pair.line.All(ch => ch == '.'))
            .Select(pair => pair.idx).ToList();
    }

    private static long SumDistances(
        IEnumerable<(int row, int col)> galaxyLocations,
        char[][] finalGrid,
        IEnumerable<((int row, int col) from, (int row, int col) to, int numRows, int numCols)> pairs,
        int howBig)
    {
        return galaxyLocations
            .Sum(location => pairs
                .Where(pair => pair.from == location)
                .Sum(pair => GetDistance(finalGrid, howBig, location, pair)));
    }

    private static long GetDistance(
        IEnumerable<char[]> finalGrid,
        int howBig,
        (int row, int col) location,
        ((int row, int col) from, (int row, int col) to, int numRows, int numCols) pair)
    {
        var extraRows = pair.numRows * (howBig - 1);
        var extraCols = pair.numCols * (howBig - 1);
        var distance = (long) GetScoredGrid(finalGrid, location)[pair.to.row][pair.to.col]!;
        
        return distance + extraRows + extraCols;
    }

    private static List<((int row, int col) from, (int row, int col) to)> GetGalaxyPairs(
        (int row, int col)[] galaxyLocations)
    {
        var pairs = new List<((int row, int col) from, (int row, int col) to)>();
        for (var i = 0; i < galaxyLocations.Length; i++)
        {
            var targets = galaxyLocations[(i + 1)..];
            
            // ReSharper disable once AccessToModifiedClosure
            var start = Enumerable.Range(0, targets.Length).Select(_ => galaxyLocations[i]);
            
            pairs.AddRange(start.Zip(targets));
        }

        return pairs;
    }

    private static int?[][] GetScoredGrid(IEnumerable<char[]> finalGrid, (int row, int col) location)
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

        return scoredGrid;
    }

    private static IEnumerable<(int row, int col)> GetGalaxyLocations(IReadOnlyList<char[]> finalGrid)
    {
        for (var row = 0; row < finalGrid.Count; row++)
        {
            for (var col = 0; col < finalGrid[0].Length; col++)
            {
                if (finalGrid[row][col] == '#')
                {
                    yield return (row, col);
                }
            }
        }
    }
}