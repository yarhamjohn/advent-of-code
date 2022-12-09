namespace AdventOfCode2022.Day9;

public static class Day9
{
    public static long CalculatePositionsVisited(string[] input, int length)
    {
        var positions = Enumerable.Range(0, length)
            .ToDictionary(x => x, _ => (currentPosition: (0, 0), visitedPositions: new List<(int, int)> {(0, 0)}));
     
        foreach (var instruction in input.Select(x => x.Split(" ")))
        {
            for (var step = 0; step < Convert.ToInt32(instruction[1]); step++)
            {
                var newHeadPosition = GetNewHeadPosition(positions[0].currentPosition, instruction[0]);
                positions[0].visitedPositions.Add(newHeadPosition);
                positions[0] = (currentPosition: newHeadPosition, positions[0].visitedPositions);

                for (var knot = 1; knot < length; knot++)
                {
                    if (NotAdjacent(positions[knot - 1].currentPosition, positions[knot].currentPosition))
                    {
                        var newTailPosition = GetNewTailPosition(positions[knot].currentPosition, positions[knot - 1].currentPosition);
                        positions[knot].visitedPositions.Add(newTailPosition);
                        positions[knot] = (currentPosition: newTailPosition, positions[knot].visitedPositions);
                    }
                }

                // if (instruction[0] == "L" && instruction[1] == "8")
                // {
                //     PrintGrid(positions, instruction);
                // }
            }

            // PrintGrid(positions, instruction);
        }
        
        // PrintPath(positions[length - 1].visitedPositions);

        return positions[length - 1].visitedPositions.Distinct().Count();
    }

    private static (int x, int y) GetNewTailPosition((int x, int y) tailPosition, (int x, int y) headPosition)
    {
        var newXPosition = tailPosition.x == headPosition.x 
            ? tailPosition.x 
            : tailPosition.x > headPosition.x 
                ? tailPosition.x - 1 
                : tailPosition.x + 1;
        
        var newYPosition = tailPosition.y == headPosition.y 
            ? tailPosition.y 
            : tailPosition.y < headPosition.y 
                ? tailPosition.y + 1 
                : tailPosition.y - 1;
        
        return (newXPosition, newYPosition);
    }

    private static (int x, int y) GetNewHeadPosition((int x, int y) headPosition, string direction)
        => direction switch
        {
            "U" => (headPosition.x - 1, headPosition.y),
            "D" => (headPosition.x + 1, headPosition.y),
            "L" => (headPosition.x, headPosition.y - 1),
            "R" => (headPosition.x, headPosition.y + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

    private static bool NotAdjacent((int x, int y) currentHeadPosition, (int x, int y) currentTailPosition)
        => Math.Abs(currentHeadPosition.x - currentTailPosition.x) >= 2
           || Math.Abs(currentHeadPosition.y - currentTailPosition.y) >= 2;
    
    private static void PrintPath(List<(int, int)> visitedPositions)
    {
        var grid = GetGrid();

        foreach (var position in visitedPositions)
        {
            grid[position.Item1 + 15][position.Item2 + 11] = "#";
        }

        Console.WriteLine();
        Console.WriteLine("--------------------------------------");
        Console.WriteLine();

        foreach (var line in grid)
        {
            Console.WriteLine(string.Join("", line.Select(x => x == "" ? "." : x)));
        }
    }
    
    private static void PrintGrid(Dictionary<int, ((int, int) currentPosition, List<(int, int)> visitedPositions)> positions, string[] instruction)
    {
        var grid = GetGrid();

        foreach (var knot in positions)
        {
            grid[knot.Value.currentPosition.Item1 + 16][knot.Value.currentPosition.Item2 + 11] = knot.Key.ToString();
        }

        Console.WriteLine();
        Console.WriteLine($"-----------------{string.Join(" ", instruction)}---------------------");
        Console.WriteLine();

        foreach (var line in grid)
        {
            Console.WriteLine(string.Join("", line.Select(x => x == "" ? "." : x)));
        }
    }

    private static string[][] GetGrid()
        => Enumerable.Range(0, 21).Select(_ => ".".PadLeft(26, '.').Split("").ToArray()).ToArray();
}