namespace AdventOfCode2022.Day14;

public static class Day14
{
    public static long CalculateUnitsOfSand(string[] input)
    {
        var wallCoordinates = GetWallCoordinates(input);
        var caveLayout = GetCaveLayout(wallCoordinates);

        PrintCave(caveLayout);
        Console.WriteLine();

        var numberUnits = ReleaseTheSand(caveLayout);
        
        PrintCave(caveLayout);
        Console.WriteLine();
        
        return numberUnits;
    }

    private static long ReleaseTheSand(char[][] cave)
    {
        var unitsOfSand = 0;

        while (true)
        {
            var finalPosition = GetFinalPosition(cave);

            if (finalPosition.row == cave.Length - 1)
            {
                break;
            }

            cave[finalPosition.row][finalPosition.col] = 'o';
            
            unitsOfSand++;
        }
        
        return unitsOfSand;
    }

    private static (int row, int col) GetFinalPosition(char[][] cave)
    {
        var col = Array.IndexOf(cave.First(), '+');
        var row = 0;
        
        while (true)
        {
            // Next place down is blocked
            if (cave[row + 1][col] == '#' || cave[row + 1][col] == 'o')
            {
                // Down and to the left is blocked
                if (cave[row + 1][col - 1] == '#' || cave[row + 1][col - 1] == 'o')
                {
                    // Down and to the right is blocked
                    if (cave[row + 1][col + 1] == '#' || cave[row + 1][col + 1] == 'o')
                    {
                        break;
                    }

                    col++;
                    row++;
                }
                else
                {
                    col--;
                    row++;
                }
            }
            else
            {
                row++;
            }

            if (row == cave.Length - 1)
            {
                break;
            }
        }

        return (row, col);
    }

    private static void PrintCave(char[][] caveLayout)
    {
        foreach (var row in caveLayout)
        {
            foreach (var col in row)
            {
                Console.Write(col);
            }

            Console.WriteLine();
        }
    }

    private static char[][] GetCaveLayout(HashSet<(int row, int col)> wallCoordinates)
    {
        var leftEdge = wallCoordinates.Min(x => x.col) - 1;
        var rightEdge = wallCoordinates.Max(x => x.col) + 1;
        var bottomEdge = wallCoordinates.Max(x => x.row) + 1;

        var cave = Enumerable.Range(0, bottomEdge + 1)
            .Select(_ => Enumerable.Range(0, rightEdge - leftEdge + 1)
                .Select(_ => '.')
                .ToArray())
            .ToArray();

        cave[0][500 - leftEdge] = '+';

        foreach (var (row, col) in wallCoordinates)
        {
            cave[row][col - leftEdge] = '#';
        }

        return cave;
    }

    private static HashSet<(int row, int col)> GetWallCoordinates(string[] input)
    {
        var coordinates = new HashSet<(int row, int col)>();
        foreach (var line in input)
        {
            var points = line
                .Split(" -> ")
                .Select(x => x
                    .Split(",")
                    .Select(y => Convert.ToInt32(y))
                    .ToArray())
                .Select(z => (row: z[1], col: z[0]))
                .ToArray();

            for (var i = 0; i < points.Length - 1; i++)
            {
                if (points[i].row == points[i + 1].row)
                {
                    if (points[i].col < points[i + 1].col)
                    {
                        for (var x = points[i].col; x <= points[i + 1].col; x++)
                        {
                            coordinates.Add((points[i].row, x));
                        }
                    }
                    else
                    {
                        for (var x = points[i + 1].col; x <= points[i].col; x++)
                        {
                            coordinates.Add((points[i].row, x));
                        }
                    }
                }
                else
                {
                    if (points[i].row < points[i + 1].row)
                    {
                        for (var y = points[i].row; y <= points[i + 1].row; y++)
                        {
                            coordinates.Add((y, points[i].col));
                        }
                    }
                    else
                    {
                        for (var y = points[i + 1].row; y <= points[i].row; y++)
                        {
                            coordinates.Add((y, points[i].col));
                        }
                    }
                }
            }
        }

        return coordinates;
    }
}