namespace AdventOfCode2022.Day18;

public static class Day18
{
    public static long CalculateSurfaceArea(string[] input)
    {
        var cubes = input
            .Select(line => line.Split(",").Select(coord => Convert.ToInt32(coord)).ToArray())
            .Select(coordinates => (x: coordinates[0], y: coordinates[1], z: coordinates[2]))
            .ToArray();


        return cubes
            .Select(GetNeighbourCubes)
            .Select(neighbouringCubes => 6 - cubes.Intersect(neighbouringCubes).Count())
            .Sum();
    }

    private static (int, int, int)[] GetNeighbourCubes((int x, int y, int z) cube)
    {
        return new[]
        {
            (cube.x + 1, cube.y, cube.z),
            (cube.x, cube.y + 1, cube.z),
            (cube.x, cube.y, cube.z + 1),
            (cube.x - 1, cube.y, cube.z),
            (cube.x, cube.y - 1, cube.z),
            (cube.x, cube.y, cube.z - 1)
        };
    }

    public static long CalculateExternalSurfaceArea(string[] input)
    {
        var surfaceArea = CalculateSurfaceArea(input);

        var cubes = input
            .Select(line => line.Split(",").Select(coord => Convert.ToInt32(coord)).ToArray())
            .Select(coordinates => (x: coordinates[0], y: coordinates[1], z: coordinates[2]))
            .ToArray();

        var matrix =
            Enumerable.Range(0, cubes.Max(c => c.x) + 1)
                .Select(_ => Enumerable.Range(0, cubes.Max(c => c.y) + 1)
                    .Select(_ => Enumerable.Range(0, cubes.Max(c => c.z) + 1).Select(_ => ".").ToArray()).ToArray()).ToArray();

        foreach (var cube in cubes)
        {
            matrix[cube.x][cube.y][cube.z] = "#";
        }

        PrintMatrix(matrix);

        var internalSurfaceArea = 0;
        for (var x = 0; x < matrix.Length; x++)
        {
            for (var y = 0; y < matrix[x].Length; y++)
            {
                for (var z = 0; z < matrix[x][y].Length; z++)
                {
                    if (matrix[x][y][z] == ".")
                    {
                        if (IsInternal(matrix, x, y, z))
                        {
                            internalSurfaceArea += CountNeighbours(matrix, x, y, z);
                        }
                    }
                }
            }
        }

        return surfaceArea - internalSurfaceArea;
    }

    private static bool IsInternal(string[][][] matrix, int x, int y, int z)
    {
        // Needs to check if neighbours are external
        return IsInternalInX(matrix, x, y, z) && IsInternalInY(matrix, x, y, z) && IsInternalInZ(matrix, x, y, z);
    }

    private static bool IsInternalInX(string[][][] matrix, int x, int y, int z)
    {
        // return Enumerable.Range(0, x).Select((_, i) => matrix[i][y][z]).Any(c => c == "#") && Enumerable
            // .Range(x, matrix.Length - x).Select((_, i) => matrix[i][y][z]).Any(c => c == "#");

        var internalOne = false;
        for (var position = x; position >= 0; position--)
        {
            if (matrix[position][y][z] == "#")
            {
                internalOne = true;
            }
        }
        
        var internalTwo = false;
        for (var position = x; position < matrix.Length; position++)
        {
            if (matrix[position][y][z] == "#")
            {
                internalTwo = true;
            }
        }

        return internalOne && internalTwo;
    }

    private static bool IsInternalInY(string[][][] matrix, int x, int y, int z)
    {
                // return Enumerable.Range(0, y).Select((_, i) => matrix[x][i][z]).Any(c => c == "#") && Enumerable
                    // .Range(y, matrix[x].Length - y).Select((_, i) => matrix[x][i][z]).Any(c => c == "#");
                    
        var internalOne = false;
        for (var position = y; position >= 0; position--)
        {
            if (matrix[x][position][z] == "#")
            {
                internalOne = true;
            }
        }
        
        
        var internalTwo = false;
        for (var position = y; position < matrix[x].Length; position++)
        {
            if (matrix[x][position][z] == "#")
            {
                internalTwo = true;
            }
        }

        return internalOne && internalTwo;
    }

    private static bool IsInternalInZ(string[][][] matrix, int x, int y, int z)
    {
        // return Enumerable.Range(0, z).Select((_, i) => matrix[x][y][i]).Any(c => c == "#") && Enumerable
            // .Range(z, matrix[x][y].Length - z).Select((_, i) => matrix[x][y][i]).Any(c => c == "#");
        
        var internalOne = false;
        for (var position = z; position >= 0; position--)
        {
            if (matrix[x][y][position] == "#")
            {
                internalOne = true;
            }
        }
        
        
        var internalTwo = false;
        for (var position = z; position < matrix[x][y].Length; position++)
        {
            if (matrix[x][y][position] == "#")
            {
                internalTwo = true;
            }
        }

        return internalOne && internalTwo;
    }
    
    private static int CountNeighbours(string[][][] matrix, int x, int y, int z)
    {
        return GetNeighbourCubes((x, y, z)).Count(c => matrix[c.Item1][c.Item2][c.Item3] == "#");
    }

    private static void PrintMatrix(string[][][] matrix)
    {
        for (var x = 0; x < matrix.Length; x++)
        {
            for (var y = 0; y < matrix[x].Length; y++)
            {
                for (var z = 0; z < matrix[x][y].Length; z++)
                {
                    if (matrix[x][y][z] == "#")
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        var isInternal = IsInternalInX(matrix, x, y, z) && IsInternalInY(matrix, x, y, z) &&
                                         IsInternalInZ(matrix, x, y, z);
                        Console.Write(isInternal ? "~" : ".");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}