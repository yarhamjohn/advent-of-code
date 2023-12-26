namespace AdventOfCode2023.Day17;

public static class Day17
{
    public static long CountEnergyLost(string[] input)
    {
        var grid = input.Select(row => row.Select(col => col - '0').ToArray()).ToArray();
        // PrintGrid(grid);
        
        // max is sum of every position
        num = grid.Select(row => row.Sum()).Sum();
        Console.WriteLine(num);
        
        NaiveSolution(0, (0, 0, [""]), grid, []);

        return num;
    }

    private static long num;

    private static void NaiveSolution(long score, (int row, int col, List<string> directions) position, int[][] grid, List<(int row, int col)> visitedPositions)
    {
        // reached the final position
        if (position.row == grid.Length - 1 && position.col == grid.First().Length - 1)
        {
            // Console.WriteLine(score + ": " + string.Join(",", visitedPositions));
            // Console.WriteLine();
            
            // if its a new lower value, set num
            var finalScore = grid[position.row][position.col] + score;
            if (finalScore < num)
            {
                num = finalScore;
                Console.WriteLine(num);
            }
            
            return;
        }
        
        // if the score is already higher, then abort
        if (score >= num)
        {
            // Console.WriteLine("Score too high");
            return;
        }

        // if we have already visited the position, then abort
        if (visitedPositions.Contains((position.row, position.col)))
        {
            // Console.WriteLine("Already seen");
            return;
        }
        
        // if going up the sides, abort
        if ((position.col == 0 || position.col == grid.First().Length - 1) && position.directions.Last() == "up")
        {
            // Console.WriteLine("Going Up");
            return;
        }
        
        // if going left on the top/bottom, abort
        if ((position.row == 0 || position.row == grid.Length - 1) && position.directions.Last() == "left")
        {
            // Console.WriteLine("Going Left");
            return;
        }
        
        // create new visited positions
        visitedPositions = visitedPositions[..];
        visitedPositions.Add((position.row, position.col));
        
        // increase the score (unless its the first position)
        if (position.row != 0 || position.col != 0)
        {
            score += grid[position.row][position.col];
        }
        
        // get the next positions
        var nextPositions = GetNextPositions(position, grid);
        // Console.WriteLine(string.Join(";", nextPositions));
        
        // loop over the next positions
        foreach (var pos in nextPositions)
        {
            // Console.WriteLine(pos);
            
            // ensure only the last 3 directions are remembered
            var newDirections = position.directions.Count == 3 ? position.directions[1..] : position.directions[0..];
            newDirections.Add(pos.direction);

            // recurse
            NaiveSolution(score, (pos.row, pos.col, newDirections), grid, visitedPositions);
        }
    }

    private static List<(int row, int col, string direction)> GetNextPositions((int row, int col, List<string> directions) position, int[][] grid)
    {
        var result = new List<(int row, int col, string direction)>();

        switch (position.directions.Last())
        {
            case "right":
            {
                if (position.row < grid.Length - 1)
                {
                    result.Add((position.row + 1, position.col, "down"));
                }

                if (position.row > 0)
                {
                    result.Add((position.row - 1, position.col, "up"));
                }

                if ((position.directions.Any(x => x != "right") || position.directions.Count < 3) && position.col < grid.First().Length - 1)
                {
                    result.Add((position.row, position.col + 1, "right"));
                }

                break;
            }
            case "left":
            {
                if (position.row < grid.Length - 1)
                {
                    result.Add((position.row + 1, position.col, "down"));
                }

                if (position.row > 0)
                {
                    result.Add((position.row - 1, position.col, "up"));
                }

                if ((position.directions.Any(x => x != "left") || position.directions.Count < 3) && position.col > 0)
                {
                    result.Add((position.row, position.col - 1, "left"));
                }

                break;
            }
            case "down":
            {
                if (position.col < grid.First().Length - 1)
                {
                    result.Add((position.row, position.col + 1, "right"));
                }

                if (position.col > 0)
                {
                    result.Add((position.row, position.col - 1, "left"));
                }

                if ((position.directions.Any(x => x != "down") || position.directions.Count < 3) && position.row < grid.Length - 1)
                {
                    result.Add((position.row + 1, position.col, "down"));
                }

                break;
            }
            case "up":
            {
                if (position.col < grid.First().Length - 1)
                {
                    result.Add((position.row, position.col + 1, "right"));
                }

                if (position.col > 0)
                {
                    result.Add((position.row, position.col - 1, "left"));
                }

                if ((position.directions.Any(x => x != "up") || position.directions.Count < 3) && position.row > 0)
                {
                    result.Add((position.row - 1, position.col, "up"));
                }

                break;
            }
            
            // The first time this method is called with (0, 0)
            default:
                result.Add((0, 1, "right"));
                result.Add((1, 0, "down"));
                break;
        }

        return result;
    }
    
    private static void PrintGrid(int[][] grid)
    {
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid.First().Length; j++)
            {
                Console.Write(grid[i][j].ToString().PadRight(2, ' '));
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static int[][] ScoreGrid(int[][] grid)
    {
        var result = Enumerable.Range(0, grid.Length)
            .Select(_ => Enumerable.Range(0, grid.First().Length).Select(_ => -1).ToArray()).ToArray();

        result[0][0] = 0;

        var nextMoves = new List<(int heatLoss, (int row, int col) next, List<string> directions)>()
        {
            (0, (0, 1), ["right"]),
            (0, (1, 0), ["down"])
        };

        while (nextMoves.Count != 0)
        {
            var move = nextMoves.First();
            nextMoves.RemoveAt(0);

            // update the heat loss for the position if it can be
            var newHeatLoss = move.heatLoss + grid[move.next.row][move.next.col];
            if (result[move.next.row][move.next.col] == -1 || newHeatLoss < result[move.next.row][move.next.col])
            {
                result[move.next.row][move.next.col] = newHeatLoss;
            }
            else
            {
                continue;
            }

            var nextLocations = GetNextLocations(move, grid);
            foreach (var location in nextLocations)
            {
                // add the next direction and ensure there are only 3
                var newDirections = move.directions.Count == 3 ? move.directions[1..] : move.directions[0..];
                newDirections.Add(location.direction);
                
                // if the next location has yet to be set, we can add the move
                if (result[location.row][location.col] == -1)
                {
                    nextMoves.Add((newHeatLoss, (location.row, location.col), newDirections));
                    continue;
                }

                // if the next location will be settable, we can add the move 
                if (result[location.row][location.col] > newHeatLoss + grid[location.row][location.col])
                {
                    nextMoves.Add((newHeatLoss, (location.row, location.col), newDirections));
                }
            }

            // PrintGrid(result);
            // foreach (var m in nextMoves)
            // {
            //     Console.WriteLine(m.heatLoss + " - " + m.next + " - " + string.Join(",", m.directions));
            // }
        }
        
        return result;
    }

    private static List<(int row, int col, string direction)> GetNextLocations((int heatLoss, (int row, int col) next, List<string> directions) move, int[][] grid)
    {
        var result = new List<(int row, int col, string direction)>();

        switch (move.directions.Last())
        {
            case "right":
            {
                if (move.next.row < grid.Length - 1)
                {
                    result.Add((move.next.row + 1, move.next.col, "down"));
                }

                if (move.next.row > 0)
                {
                    result.Add((move.next.row - 1, move.next.col, "up"));
                }

                if ((move.directions.Any(x => x != "right") || move.directions.Count < 3) && move.next.col < grid.First().Length - 1)
                {
                    result.Add((move.next.row, move.next.col + 1, "right"));
                }

                break;
            }
            case "left":
            {
                if (move.next.row < grid.Length - 1)
                {
                    result.Add((move.next.row + 1, move.next.col, "down"));
                }

                if (move.next.row > 0)
                {
                    result.Add((move.next.row - 1, move.next.col, "up"));
                }

                if ((move.directions.Any(x => x != "left") || move.directions.Count < 3) && move.next.col > 0)
                {
                    result.Add((move.next.row, move.next.col - 1, "left"));
                }

                break;
            }
            case "down":
            {
                if (move.next.col < grid.First().Length - 1)
                {
                    result.Add((move.next.row, move.next.col + 1, "right"));
                }

                if (move.next.col > 0)
                {
                    result.Add((move.next.row, move.next.col - 1, "left"));
                }

                if ((move.directions.Any(x => x != "down") || move.directions.Count < 3) && move.next.row < grid.Length - 1)
                {
                    result.Add((move.next.row + 1, move.next.col, "down"));
                }

                break;
            }
            case "up":
            {
                if (move.next.col < grid.First().Length - 1)
                {
                    result.Add((move.next.row, move.next.col + 1, "right"));
                }

                if (move.next.col > 0)
                {
                    result.Add((move.next.row, move.next.col - 1, "left"));
                }

                if ((move.directions.Any(x => x != "up") || move.directions.Count < 3) && move.next.row > 0)
                {
                    result.Add((move.next.row - 1, move.next.col, "up"));
                }

                break;
            }
        }

        return result;
    }
}