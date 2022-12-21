using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;

namespace AdventOfCode2022.Day18;

public static class Day18
{
    public static long CalculateSurfaceArea(string[] input)
    {
        var cubes = ParseCubes(input);

        return cubes
            .Select(GetNeighbourCubes)
            .Select(neighbouringCubes => 6 - cubes.Intersect(neighbouringCubes).Count())
            .Sum();
    }

    public static long CalculateExternalSurfaceArea(string[] input)
    {
        var cubes = ParseCubes(input);
        var matrix = ConstructMatrix(cubes);

        MarkLavaCubes(cubes, matrix);
        MarkExternalCubes(matrix, (0, 0, 0));
        
        PrintMatrix(matrix);

        return CalculateSurfaceArea(input) - GetInternalSurfaceArea(matrix);
    }

    private static (int x, int y, int z)[] ParseCubes(string[] input)
        => input
            .Select(line => line.Split(",").Select(coord => Convert.ToInt32(coord)).ToArray())
            .Select(coordinates => (x: coordinates[0], y: coordinates[1], z: coordinates[2]))
            .ToArray();

    private static string[][][] ConstructMatrix((int x, int y, int z)[] cubes)
    {
        var matrix =
            Enumerable.Range(0, cubes.Max(c => c.x) + 1)
                .Select(_ => Enumerable.Range(0, cubes.Max(c => c.y) + 1)
                    .Select(_ => Enumerable.Range(0, cubes.Max(c => c.z) + 1).Select(_ => ".").ToArray()).ToArray())
                .ToArray();
        return matrix;
    }

    private static void MarkLavaCubes((int x, int y, int z)[] cubes, string[][][] matrix)
    {
        foreach (var cube in cubes)
        {
            matrix[cube.x][cube.y][cube.z] = "#";
        }
    }

    private static void MarkExternalCubes(string[][][] matrix, (int x, int y, int z) cube)
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
            MarkExternalCubes(matrix, neighbour);
        }
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

    private static bool IsExternal(string[][][] matrix, int x, int y, int z) =>
        !IsInternalInX(matrix, x, y, z) || !IsInternalInY(matrix, x, y, z) || !IsInternalInZ(matrix, x, y, z);

    private static bool IsInternalInX(string[][][] matrix, int x, int y, int z)
        => Enumerable.Range(0, x).Any(i => matrix[i][y][z] == "#") &&
            Enumerable.Range(x, matrix.Length - x).Any(i => matrix[i][y][z] == "#");

    private static bool IsInternalInY(string[][][] matrix, int x, int y, int z)
        => Enumerable.Range(0, y).Any(i => matrix[x][i][z] == "#") &&
           Enumerable.Range(y, matrix[x].Length - y).Any(i => matrix[x][i][z] == "#");

    private static bool IsInternalInZ(string[][][] matrix, int x, int y, int z)
        => Enumerable.Range(0, z).Any(i => matrix[x][y][i] == "#") &&
           Enumerable.Range(z, matrix[x][y].Length - z).Any(i => matrix[x][y][i] == "#");
    
    private static int CountNeighbours(string[][][] matrix, int x, int y, int z) 
        => GetNeighbourCubes((x, y, z))
            .Count(c => InMatrix(matrix, c) && matrix[c.Item1][c.Item2][c.Item3] == "#");
    
    private static (int, int, int)[] GetNeighbourCubes((int x, int y, int z) cube)
        => new[]
        {
            (cube.x + 1, cube.y, cube.z),
            (cube.x, cube.y + 1, cube.z),
            (cube.x, cube.y, cube.z + 1),
            (cube.x - 1, cube.y, cube.z),
            (cube.x, cube.y - 1, cube.z),
            (cube.x, cube.y, cube.z - 1)
        };

    private static bool InMatrix(string[][][] matrix, (int x, int y, int z) cube)
        => cube.x >= 0
           && cube.x < matrix.Length
           && cube.y >= 0
           && cube.y < matrix[cube.x].Length
           && cube.z >= 0
           && cube.z < matrix[cube.x][cube.y].Length;
    
    private static void PrintMatrix(string[][][] matrix)
    {
        foreach (var x in matrix)
        {
            foreach (var y in x)
            {
                foreach (var z in y)
                {
                    Console.Write(z);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}