using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day17
    {
        public static long CountActiveCubes(List<string> input, int numDimensions)
        {
            var activeCubes = GetInitialActiveCubes(input, numDimensions);
            for (var i = 0; i < 6; i++)
            {
                UpdateActiveCubes(activeCubes, numDimensions);
            }

            return activeCubes.Count;
        }

        private static void UpdateActiveCubes(List<string> activeCubes, int numDimensions)
        {
            var (activeCubeNeighbours, inactiveCubeNeighbours) = CalculateNeighbourCubes(activeCubes, numDimensions);
            activeCubes.Clear();

            foreach (var (key, value) in activeCubeNeighbours)
            {
                if (value == 2 || value == 3)
                {
                    activeCubes.Add(key);
                }
            }

            foreach (var (key, value) in inactiveCubeNeighbours)
            {
                if (value == 3)
                {
                    activeCubes.Add(key);
                }
            }
        }

        private static (Dictionary<string, int> activeCubeNeighbours, Dictionary<string, int> inactiveCubeNeighbours)
            CalculateNeighbourCubes(List<string> activeCubes, int numDimensions)
        {
            var activeCubeNeighbours = new Dictionary<string, int>();
            var inactiveCubeNeighbours = new Dictionary<string, int>();
            foreach (var cube in activeCubes)
            {
                var neighbours = GetNeighbourCubes(cube, numDimensions);
                foreach (var neighbour in neighbours)
                {
                    if (activeCubes.Contains(neighbour))
                    {
                        // Add the target cube and increment its active neighbour count
                        if (activeCubeNeighbours.ContainsKey(cube))
                        {
                            activeCubeNeighbours[cube] += 1;
                        }
                        else
                        {
                            activeCubeNeighbours[cube] = 1;
                        }
                    }
                    else
                    {
                        // Add the neighbour and increment its active neighbour count
                        if (inactiveCubeNeighbours.ContainsKey(neighbour))
                        {
                            inactiveCubeNeighbours[neighbour] += 1;
                        }
                        else
                        {
                            inactiveCubeNeighbours[neighbour] = 1;
                        }
                    }
                }
            }

            return (activeCubeNeighbours, inactiveCubeNeighbours);
        }

        private static List<string> GetNeighbourCubes(string cube, int numDimensions)
        {
            var vector = cube.Split(";").Select(x => Convert.ToInt32(x)).ToList();
            return numDimensions == 3 ? Get3dNeighbourCubes(vector) : Get4dNeighbourCubes(vector);
        }

        private static List<string> Get3dNeighbourCubes(List<int> vector)
        {
            var neighbours = new List<string>();
            for (var x = vector[0] - 1; x <= vector[0] + 1; x++)
            {
                for (var y = vector[1] - 1; y <= vector[1] + 1; y++)
                {
                    for (var z = vector[2] - 1; z <= vector[2] + 1; z++)
                    {
                        if (x == vector[0] && y == vector[1] && z == vector[2])
                        {
                            continue;
                        }

                        neighbours.Add($"{x};{y};{z}");
                    }
                }
            }

            return neighbours;
        }

        private static List<string> Get4dNeighbourCubes(List<int> vector)
        {
            var neighbours = new List<string>();
            for (var x = vector[0] - 1; x <= vector[0] + 1; x++)
            {
                for (var y = vector[1] - 1; y <= vector[1] + 1; y++)
                {
                    for (var z = vector[2] - 1; z <= vector[2] + 1; z++)
                    {
                        for (var w = vector[3] - 1; w <= vector[3] + 1; w++)
                        {
                            if (x == vector[0] && y == vector[1] && z == vector[2] && w == vector[3])
                            {
                                continue;
                            }

                            neighbours.Add($"{x};{y};{z};{w}");
                        }
                    }
                }
            }

            return neighbours;
        }

        private static List<string> GetInitialActiveCubes(List<string> input, int numDimensions)
        {
            var activeCubes = new List<string>();
            for (var x = 0; x < input.Count; x++)
            {
                var line = input[x].ToCharArray();
                for (var y = 0; y < line.Length; y++)
                {
                    if (line[y] == '#')
                    {
                        var fullVector = numDimensions == 3 ? $"{x};{y};0" : $"{x};{y};0;0";
                        activeCubes.Add(fullVector);
                    }
                }
            }

            return activeCubes;
        }
    }
}