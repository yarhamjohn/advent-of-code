using System.Text;

namespace AdventOfCode2022.Day22;

public static class Day22
{
    public static long CalculatePassword(string[] input)
    {
        var grid = GetGrid(input.TakeWhile(x => x != "").ToArray());
        var instructions = GetInstructions(input.Last());

        var currentLocation = GetStartLocation(grid);
        var currentDirection = ">";
        foreach (var (distance, direction) in instructions)
        {
            currentLocation = DoInstruction(distance, grid, currentLocation, currentDirection);
            if (direction != "")
            {
                currentDirection = GetNextDirection(direction, currentDirection);
            }
        }

        return Calculate(currentLocation, currentDirection);
    }

    private static string GetNextDirection(string direction, string currentDirection)
    {
        if (direction == "R")
        {
            return currentDirection switch
            {
                ">" => "v",
                "v" => "<",
                "<" => "^",
                "^" => ">"
            };
        }

        return currentDirection switch
        {
            ">" => "^",
            "v" => ">",
            "<" => "v",
            "^" => "<"
        };
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
        
        Console.WriteLine();
    }

    private static (int x, int y) DoInstruction(int distance, char[][] grid, (int x, int y) currentLocation, string currentDirection)
    {
        var placesMoved = 0;
        while (true)
        {
            if (placesMoved == distance)
            {
                break;
            }

            var nextPosition = GetNextPosition(grid, currentLocation, currentDirection);

            if (grid[nextPosition.x][nextPosition.y] == '#')
            {
                break;
            }

            grid[currentLocation.x][currentLocation.y] = currentDirection.ToCharArray().Single();

            currentLocation = nextPosition;
            
            placesMoved++;
        }

        return currentLocation;
    }

    private static (int x, int y) GetNextPosition(char[][] grid, (int x, int y) currentLocation, string currentDirection)
    {
        var nextLocation = (currentLocation.x, currentLocation.y);

        if (currentDirection == ">")
        {
            while (true)
            {
                nextLocation = (nextLocation.x, y: nextLocation.y + 1);

                if (nextLocation.y == grid[currentLocation.x].Length)
                {
                    nextLocation = (nextLocation.x, y: 0);
                }

                if (grid[nextLocation.x][nextLocation.y] != ' ')
                    {
                        return nextLocation;
                    }
            }
        }
        
        if (currentDirection == "v")
        {
            while (true)
            {
                nextLocation = (x: nextLocation.x + 1, nextLocation.y);

                if (nextLocation.x == grid.Length)
                {
                    nextLocation = (x: 0, nextLocation.y);
                }

                if (grid[nextLocation.x][nextLocation.y] != ' ')
                    {
                        return nextLocation;
                    }
            }
        }
        
        if (currentDirection == "<")
        {
            while (true)
            {
                nextLocation = (nextLocation.x, y: nextLocation.y - 1);

                if (nextLocation.y < 0)
                {
                    nextLocation = (nextLocation.x, y: grid[nextLocation.x].Length - 1);
                }

                if (grid[nextLocation.x][nextLocation.y] != ' ')
                    {
                        return nextLocation;
                    }
            }
        }
        
        if (currentDirection == "^")
        {
            while (true)
            {
                nextLocation = (x: nextLocation.x - 1, nextLocation.y);

                if (nextLocation.x < 0)
                {
                    nextLocation = (x: grid.Length - 1, nextLocation.y);
                }

                if (grid[nextLocation.x][nextLocation.y] != ' ')
                    {
                        return nextLocation;
                }
            }
        }

        throw new InvalidOperationException($"current direction was: {currentDirection}, current location was: x:{currentLocation.x}, y: {currentLocation.y} ");
    }

    private static (int x, int y) GetStartLocation(char[][] grid)
    {
        return (0, Array.IndexOf(grid.First(), '.'));
    }

    private static List<(int distance, string direction)> GetInstructions(string input)
    {
        var result = new List<(int distance, string direction)>();
        
        var x = new StringBuilder();
        foreach (var element in input)
        {
            if (element is 'L' or 'R')
            {
                result.Add((Convert.ToInt32(x.ToString()), element.ToString()));
                x.Clear();
            }
            else
            {
                x.Append(element);
            }
        }
        
        result.Add((Convert.ToInt32(x.ToString()), ""));

        return result;
    }

    private static char[][] GetGrid(string[] input)
    {
        var rowLength = input.Max(x => x.Length);
        return input.Select(row =>
        {
            var fillRow = row.PadRight(rowLength, ' ');
            return Enumerable.Range(0, rowLength).Select(x => fillRow[x]).ToArray();
        }).ToArray();
    }

    private static long Calculate((int x, int y) currentLocation, string lastDirection)
    {
        return (currentLocation.x + 1) * 1000 + (currentLocation.y + 1) * 4 +
               lastDirection switch
               {
                   ">" => 0,
                   "v" => 1,
                   "<" => 2,
                   "^" => 3
               };
    }
}