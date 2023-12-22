namespace AdventOfCode2023.Day16;

public static class Day16
{
    public static long CountEnergizedTiles(string[] input)
    {
        var grid = input.Select(line => line.ToCharArray().Select(x => new Tile(x.ToString())).ToArray()).ToList();
        // PrintGrid(grid);

        var lightPositions = new List<(int row, int col, string direction)> { (0, 0, "right") };
        var seenPositions = new List<(int row, int col, string direction)> { (0, 0, "right") };

        while (lightPositions.Count > 0)
        {
            var nextPosition = lightPositions.First();
            lightPositions.RemoveAt(0);
            
            var nextPositions = grid[nextPosition.row][nextPosition.col].Energise(nextPosition, grid);
            foreach (var position in nextPositions)
            {
                if (!seenPositions.Contains(position))
                {
                    lightPositions.Add(position);
                    seenPositions.Add(position);
                }
            }
        }

        // PrintGridFinal(grid);
        
        return grid.Sum(x => x.Count(y => y.IsEnergised));
    }

    public static long MaxEnergizedTiles(string[] input)
    {
        var grid = input.Select(line => line.ToCharArray().Select(x => new Tile(x.ToString())).ToArray()).ToList();
        // PrintGrid(grid);

        var maxScore = 0L;

        var startingPositions = GetStartingPositions(grid);

        foreach (var position in startingPositions) {

            var lightPositions = new List<(int row, int col, string direction)> { position };
            var seenPositions = new List<(int row, int col, string direction)> { position };

            while (lightPositions.Count > 0)
            {
                var nextPosition = lightPositions.First();
                lightPositions.RemoveAt(0);

                var nextPositions = grid[nextPosition.row][nextPosition.col].Energise(nextPosition, grid);
                foreach (var pos in nextPositions)
                {
                    if (!seenPositions.Contains(pos))
                    {
                        lightPositions.Add(pos);
                        seenPositions.Add(pos);
                    }
                }
            }

            // PrintGridFinal(grid);

            var score = grid.Sum(x => x.Count(y => y.IsEnergised));
            if (score > maxScore)
            {
                maxScore = score;
            }

            for (var row = 0; row < grid.Count(); row++)
            {
                for (var col = 0; col < grid.First().Length; col++)
                {
                    grid[row][col].DeEnergise();
                }
            }
        }
        
        return maxScore;
    }

    private static IEnumerable<(int row, int col, string direction)> GetStartingPositions(IReadOnlyCollection<Tile[]> grid)
    {
        for (var i = 0; i < grid.Count; i++)
        {
            yield return (i, 0, "right");
            yield return (i, grid.First().Length - 1, "left");
        }
        
        for (var i = 0; i < grid.First().Length; i++)
        {
            yield return (0, i, "down");
            yield return (grid.Count - 1, i, "up");
        }
    }

    private static void PrintGrid(IReadOnlyList<Tile[]> grid)
    {
        foreach (var row in grid)
        {
            for (var col = 0; col < grid[0].Length; col++)
            {
                Console.Write(row[col].Contents);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static void PrintGridFinal(IReadOnlyList<Tile[]> grid)
    {
        foreach (var row in grid)
        {
            for (var col = 0; col < grid[0].Length; col++)
            {
                Console.Write(row[col].IsEnergised ? "#" : ".");
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
    
    private class Tile(string contents)
    {
        public readonly string Contents = contents;
        public bool IsEnergised;

        public IEnumerable<(int row, int col, string direction)> Energise(
            (int row, int col, string currentDirection) position,
            IReadOnlyCollection<Tile[]> grid)
        {
            IsEnergised = true;

            var nextPositions = position.currentDirection switch
            {
                "right" => MoveRight(position),
                "left" => MoveLeft(position),
                "up" => MoveUp(position),
                "down" => MoveDown(position),
                _ => throw new ArgumentException(position.currentDirection)
            };
            
            foreach (var pos in nextPositions)
            {
                if (pos is { row: >= 0, col: >= 0 } && pos.row < grid.Count && pos.col < grid.First().Length)
                {
                    yield return pos;
                }
            }
        }

        private List<(int row, int col, string direction)> MoveRight((int row, int col, string currentDirection) position)
        {
            return Contents switch
            {
                "." or "-" => [(position.row, position.col + 1, "right")],
                "|" => [(position.row - 1, position.col, "up"), (position.row + 1, position.col, "down")],
                "\\" => [(position.row + 1, position.col, "down")],
                "/" => [(position.row - 1, position.col, "up")],
                _ => throw new ArgumentException($"Invalid contents: {Contents}")
            };
        }
        
        private List<(int row, int col, string direction)> MoveLeft((int row, int col, string currentDirection) position)
        {
            return Contents switch
            {
                "." or "-" => [(position.row, position.col - 1, "left")],
                "|" => [(position.row - 1, position.col, "up"), (position.row + 1, position.col, "down")],
                "\\" => [(position.row - 1, position.col, "up")],
                "/" => [(position.row + 1, position.col, "down")],
                _ => throw new ArgumentException($"Invalid contents: {Contents}")
            };
        }
        
        private List<(int row, int col, string direction)> MoveUp((int row, int col, string currentDirection) position)
        {
            return Contents switch
            {
                "." or "|" => [(position.row - 1, position.col, "up")],
                "-" => [(position.row, position.col - 1, "left"), (position.row, position.col + 1, "right")],
                "\\" => [(position.row, position.col - 1, "left")],
                "/" => [(position.row, position.col + 1, "right")],
                _ => throw new ArgumentException($"Invalid contents: {Contents}")
            };
        }
        
        private List<(int row, int col, string direction)> MoveDown((int row, int col, string currentDirection) position)
        {
            return Contents switch
            {
                "." or "|" => [(position.row + 1, position.col, "down")],
                "-" => [(position.row, position.col - 1, "left"), (position.row, position.col + 1, "right")],
                "\\" => [(position.row, position.col + 1, "right")],
                "/" => [(position.row, position.col - 1, "left")],
                _ => throw new ArgumentException($"Invalid contents: {Contents}")
            };
        }

        public void DeEnergise()
        {
            IsEnergised = false;
        }
    }
}