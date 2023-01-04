using System.Text;

namespace AdventOfCode2022.Day22;

public static class Day22
{
    public static long CalculateCubePassword(string[] input)
    {
        var grid = GetGrid(input.TakeWhile(x => x != "").ToArray());
        var instructions = GetInstructions(input.Last());

        var currentLocation = GetStartLocation(grid);
        var currentDirection = ">";
        foreach (var (distance, direction) in instructions)
        {
            (currentLocation, currentDirection) = DoCubeInstruction(distance, grid, currentLocation, currentDirection);

            if (direction != "")
            {
                currentDirection = GetNextDirection(direction, currentDirection);
            }
        }

        return Calculate(currentLocation, currentDirection);
    }
    
    private static ((int x, int y), string direction) DoCubeInstruction(int distance, char[][] grid, (int x, int y) currentLocation, string currentDirection)
    {
        var placesMoved = 0;
        while (true)
        {
            if (placesMoved == distance)
            {
                break;
            }

            var (nextPosition, nextDirection) = GetNextCubePositionAndDirection(grid, currentLocation, currentDirection);

            if (grid[nextPosition.x][nextPosition.y] == '#')
            {
                break;
            }

            grid[currentLocation.x][currentLocation.y] = currentDirection.ToCharArray().Single();

            currentLocation = nextPosition;
            currentDirection = nextDirection;
            
            placesMoved++;
        }

        return (currentLocation, currentDirection);
    }

    private static ((int x, int y), string direction) GetNextCubePositionAndDirection(char[][] grid, (int x, int y) currentLocation, string currentDirection)
    {
        var nextLocation = GetNextLocation(currentLocation, currentDirection);

        if (nextLocation.x < 0 || currentDirection == "^" && grid[nextLocation.x][nextLocation.y] == ' ')
        {
            return FoldUp(currentLocation.y);
        }

        if (nextLocation.x >= grid.Length || currentDirection == "v" && grid[nextLocation.x][nextLocation.y] == ' ')
        {
            return FoldDown(currentLocation.y);
        }
        
        if (nextLocation.y < 0 || currentDirection == "<" && grid[nextLocation.x][nextLocation.y] == ' ')
        {
            return FoldLeft(currentLocation.x);
        }
        
        if (nextLocation.y >= grid.First().Length || currentDirection == ">" && grid[nextLocation.x][nextLocation.y] == ' ')
        {
            return FoldRight(currentLocation.x);
        }

        return (nextLocation, currentDirection);
    }

    private static (int x, int y) GetNextLocation((int x, int y) currentLocation, string currentDirection)
    {
        return currentDirection switch
        {
            ">" => (currentLocation.x, y: currentLocation.y + 1),
            "<" => (currentLocation.x, y: currentLocation.y - 1),
            "^" => (x: currentLocation.x - 1, currentLocation.y),
            "v" => (x: currentLocation.x + 1, currentLocation.y),
            _ => currentLocation
        };
    }

    private static ((int, int), string) FoldUp(int col)
    {
        return col switch
        {
            < 50 => ((50 + col, 50), ">"),
            >= 50 and < 100 => ((100 + col, 0), ">"),
            >= 100 => ((199, col - 100), "^")
        };
    }

    private static ((int, int), string) FoldDown(int col)
    {
        return col switch
        {
            < 50 => ((0, col + 100), "v"),
            >= 50 and < 100 => ((100 + col, 49), "<"),
            >= 100 => ((col - 50, 99), "<")
        };
    }

    private static ((int, int), string) FoldLeft(int row)
    {
        return row switch
        {
            < 50 => ((149 - row, 0), ">"),
            >= 50 and < 100 => ((100, row - 50), "v"),
            >= 100 and < 150 => ((149 - row, 50), ">"),
            >= 150 => ((0, row - 100), "v")
        };
    }

    private static ((int, int), string) FoldRight(int row)
    {
        return row switch
        {
            < 50 => ((149 - row, 99), "<"),
            >= 50 and < 100 => ((49, row + 50), "^"),
            >= 100 and < 150 => ((149 - row, 149), "<"),
            >= 150 => ((149, row - 100), "^")
        };
    }

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
                "^" => ">",
                _ => throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, null)
            };
        }

        return currentDirection switch
        {
            ">" => "^",
            "v" => ">",
            "<" => "v",
            "^" => "<",
            _ => throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, null)
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

        switch (currentDirection)
        {
            case ">":
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
            case "v":
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
            case "<":
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
            case "^":
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
                   "^" => 3,
                   _ => throw new ArgumentOutOfRangeException(nameof(lastDirection), lastDirection, null)
               };
    }
}