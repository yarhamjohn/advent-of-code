﻿namespace AdventOfCode2023.Day21;

public static class Day21
{
    public static long CountGardenPlots(string[] input, int steps)
    {
        var (grid, currentPositions) = ParseInput(input);

        for (var i = 0; i < steps; i++)
        {
            currentPositions = currentPositions.SelectMany(x => GetNextPositions(x, grid)).ToHashSet();
        }
        
        return currentPositions.Count;
    }

    public static long CountGardenPlotsBig(string[] input, long steps)
    {
        var (grid, currentPositions) = ParseInputBig(input);

        for (var i = 0; i < steps; i++)
        {
            // TODO: Need to improve performance dramatically
            // key is which grid (starting grid is (0,0)) and values are the positions on that grid
            var gridMapping = new Dictionary<(int x, int y), HashSet<(int x, int y)>>();
            // Then get relative next positions for all the values (this can be calculated once and stored)
            // Apply the relative positioning to each value - this may mean moving an element from one grid to another
            foreach (var (key, value) in currentPositions)
            {
                // Next position contains a (row, col) which represents relative grid location and (x, y) which represents in-grid location
                var nextPositions = value.SelectMany(x => GetNextPositionsBig(x, grid));
                foreach (var pos in nextPositions)
                {
                    var newKey = (key.x + pos.row, key.y + pos.col);
                    if (!gridMapping.ContainsKey(newKey))
                    {
                        gridMapping[newKey] = [];
                    }
                    
                    gridMapping[newKey].Add((pos.x, pos.y));
                }
            }

            currentPositions = gridMapping;
        }
        
        return currentPositions.Values.SelectMany(x => x).Count();
    }
    
    private static (List<List<Element>> grid, HashSet<(int x, int y)> currentPositions) ParseInput(string[] input)
    {
        var grid = new List<List<Element>>();
        var currentPositions = new HashSet<(int x, int y)>();
        
        for (var i = 0; i < input.Length; i++) 
        {
            var row = new List<Element>();
            for (var j = 0; j < input[0].Length; j++)
            {
                row.Add(input[i][j] == '#' ? Element.Rock : Element.Garden);

                if (input[i][j] == 'S')
                {
                    currentPositions.Add((i, j));
                }
            }
            
            grid.Add(row);
        }

        return (grid, currentPositions);
    }

    private static (List<List<Element>> grid, Dictionary<(int x, int y), HashSet<(int x, int y)>> currentPositions) ParseInputBig(string[] input)
    {
        var grid = new List<List<Element>>();
        var currentPositions = new Dictionary<(int x, int y), HashSet<(int x, int y)>>();
        
        for (var i = 0; i < input.Length; i++) 
        {
            var row = new List<Element>();
            for (var j = 0; j < input[0].Length; j++)
            {
                row.Add(input[i][j] == '#' ? Element.Rock : Element.Garden);

                if (input[i][j] == 'S')
                {
                    currentPositions[(0, 0)] = [(i, j)];
                }
            }
            
            grid.Add(row);
        }

        return (grid, currentPositions);
    }

    private static IEnumerable<(int x, int y)> GetNextPositions((int x, int y) position, List<List<Element>> grid)
    {
        if (position.x > 0 && grid[position.x - 1][position.y] == Element.Garden)
        {
            yield return (position.x - 1, position.y);
        }
        
        if (position.y > 0 && grid[position.x][position.y - 1] == Element.Garden)
        {
            yield return (position.x, position.y - 1);
        }
        
        if (position.x < grid.Count - 1 && grid[position.x + 1][position.y] == Element.Garden)
        {
            yield return (position.x + 1, position.y);
        }
        
        if (position.y < grid[0].Count - 1 && grid[position.x][position.y + 1] == Element.Garden)
        {
            yield return (position.x, position.y + 1);
        }
    }

    private static readonly Dictionary<(int x, int y), List<(int row, int col, int x, int y)>> NextPositions = [];
    
    private static IEnumerable<(int row, int col, int x, int y)> GetNextPositionsBig((int x, int y) position, List<List<Element>> grid)
    {
        if (NextPositions.TryGetValue(position, out var positions))
        {
            return positions;
        }

        NextPositions[position] = [];
        
        var numRows = (decimal) grid.Count;
        var numCols = (decimal) grid[0].Count;

        var xMod = (int)(position.x >= 0 ? position.x % numRows : GetMod(position.x, numRows));
        var yMod = (int)(position.y >= 0 ? position.y % numCols : GetMod(position.y, numCols));
        
        var xModAbove = (int) (position.x > 0 ? (position.x - 1) % numRows : GetMod(position.x - 1, numRows));
        var xModBelow = (int) (position.x >= 0 ? (position.x + 1) % numRows : GetMod(position.x + 1, numRows));
        
        var yModLeft = (int) (position.y > 0 ? (position.y - 1) % numCols : GetMod(position.y - 1, numCols));
        var yModRight = (int) (position.y >= 0 ? (position.y + 1) % numCols : GetMod(position.y + 1, numCols));
        
        if (grid[xModAbove][yMod] == Element.Garden)
        {
            NextPositions[position].Add((position.x == 0 ? -1 : 0, 0, position.x - 1, position.y));
        }

        if (grid[xMod][yModLeft] == Element.Garden)
        {
            NextPositions[position].Add((0, position.y == 0 ? -1 : 0, position.x, position.y - 1));
        }
        
        if (grid[xModBelow][yMod] == Element.Garden)
        {
            NextPositions[position].Add((position.x == numRows - 1 ? 1 : 0, 0, position.x + 1, position.y));
        }
        
        if (grid[xMod][yModRight] == Element.Garden)
        {
            NextPositions[position].Add((0, position.y == numCols - 1 ? 1 : 0, position.x, position.y + 1));
        }
        
        return NextPositions[position];
    }

    private static decimal GetMod(long position, decimal modulo)
    {
        return position - modulo * Math.Floor(position / modulo);
    }

    private enum Element
    {
        Garden,
        Rock
    };
}
