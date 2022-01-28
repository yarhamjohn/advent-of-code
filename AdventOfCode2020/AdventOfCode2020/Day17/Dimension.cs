using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static partial class Day17
    {
        private class Dimension
        {
            private List<string[,]> _layers;

            public Dimension(List<string> input)
            {
                _layers = new List<string[,]> {GetInputLayer(input)};
            }

            public void ExpandDimension()
            {
                var newLayers = new List<string[,]>();
                newLayers.AddRange(ExpandLayers());

                var newWidth = newLayers.First().GetLength(0);
                var emptyRow = ".".PadRight(newWidth, '.');
                var emptyLayer = Enumerable.Repeat(emptyRow, newWidth).ToList();
                var newLayer = GetInputLayer(emptyLayer);

                newLayers.Insert(0, newLayer);
                newLayers.Insert(newLayers.Count, newLayer);
                
                _layers = newLayers;
            }

            private IEnumerable<string[,]> ExpandLayers()
            {
                foreach (var layer in _layers)
                {
                    var currentSize = layer.GetLength(0);
                    var newSize = currentSize + 2;
                    var newLayer = new string[newSize, newSize];
                    for (var x = 0; x < newSize; x++)
                    {
                        for (var y = 0; y < newSize; y++)
                        {
                            if (x == 0 || y == 0 || x == newSize - 1 || y == newSize - 1)
                            {
                                newLayer[x, y] = ".";
                            }
                            else
                            {
                                newLayer[x, y] = layer[x - 1, y - 1];
                            }
                        }
                    }

                    yield return newLayer;
                }
            }

            private string[,] GetInputLayer(List<string> input)
            {
                var inputLayer = new string[input.Count, input.Count];
                for (var i = 0; i < input.Count; i++)
                {
                    var line = input[i].ToCharArray();
                    for (var j = 0; j < line.Length; j++)
                    {
                        inputLayer[i, j] = line[j].ToString();
                    }
                }
            
                return inputLayer;
            }

            public void PrintLayers()
            {
                foreach (var layer in _layers)
                {
                    for (var x = 0; x < layer.GetLength(0); x++)
                    {
                        for (var y = 0; y < layer.GetLength(1); y++)
                        {
                            Console.Write(layer[x, y]);
                        }

                        Console.WriteLine();
                    }
                    
                    Console.WriteLine();
                }
            }

            public int CountActiveCubes()
            {
                var count = 0;
                foreach (var layer in _layers)
                {
                    for (var x = 0; x < layer.GetLength(0); x++)
                    {
                        for (var y = 0; y < layer.GetLength(1); y++)
                        {
                            if (layer[x, y] == "#")
                            {
                                count++;
                            }
                        }
                    }
                }

                return count;
            }
        }
    }
}