using System.Runtime.CompilerServices;

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

        SetExternalSurfaceArea(matrix, (0, 0, 0));
        
        PrintMatrix(matrix);
        
        var internalSurfaceArea = GetInternalSurfaceArea(matrix);
        
        return surfaceArea - internalSurfaceArea;
    }

    private static void SetExternalSurfaceArea(string[][][] matrix, (int x, int y, int z) cube)
    {
        var neighbours = GetNeighbourCubes(cube);
        
        if (IsExternal(matrix, cube.x, cube.y, cube.z) || neighbours.Any(c => matrix[c.Item1][c.Item2][c.Item3] == "~"))
        {
            matrix[cube.x][cube.y][cube.z] = "~";
        }
        
        var nextNeighbours = neighbours.Where(c => InMatrix(matrix, c) && matrix[c.Item1][c.Item2][c.Item3] == ".").ToArray();

        if (!nextNeighbours.Any())
        {
            return;
        }

        foreach (var neighbour in nextNeighbours)
        {
            SetExternalSurfaceArea(matrix, neighbour);
        }
    }

    private static bool InMatrix(string[][][] matrix, (int, int, int) c)
    {
        return c.Item1 >= 0 && c.Item2 >= 0 && c.Item3 >= 0 && c.Item1 < matrix.Length && c.Item2 < matrix[c.Item1].Length && c.Item3 < matrix[c.Item1][c.Item2].Length;
    }

    private static int GetInternalSurfaceArea(string[][][] matrix)
    {
        var internalSurfaceArea = 0;
        for (var x = 0; x < matrix.Length; x++)
        {
            for (var y = 0; y < matrix[x].Length; y++)
            {
                for (var z = 0; z < matrix[x][y].Length; z++)
                {
                    if (matrix[x][y][z] == ".")
                    {
                        internalSurfaceArea += CountNeighbours(matrix, x, y, z);
                    }
                }
            }
        }

        return internalSurfaceArea;
    }

    private static bool IsExternal(string[][][] matrix, int x, int y, int z)
    {
        return !IsInternalInX(matrix, x, y, z) || !IsInternalInY(matrix, x, y, z) || !IsInternalInZ(matrix, x, y, z);
    }

    private static bool IsInternalInX(string[][][] matrix, int x, int y, int z)
    {
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
        return GetNeighbourCubes((x, y, z)).Where(c => InMatrix(matrix, c)).Count(c => matrix[c.Item1][c.Item2][c.Item3] == "#");
    }

    private static void PrintMatrix(string[][][] matrix)
    {
        for (var x = 0; x < matrix.Length; x++)
        {
            for (var y = 0; y < matrix[x].Length; y++)
            {
                for (var z = 0; z < matrix[x][y].Length; z++)
                {
                    Console.Write(matrix[x][y][z]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}