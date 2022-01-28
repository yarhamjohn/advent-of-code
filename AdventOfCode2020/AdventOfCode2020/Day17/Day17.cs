using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace AdventOfCode
{
    public static partial class Day17
    {
        public static long CountActiveCubes(List<string> input)
        {
            var dimension = new Dimension(input);
            Console.WriteLine("Input: ");
            dimension.PrintLayers();

            for (var i = 0; i < 6; i++)
            {
                Console.WriteLine($"Turn {i}: ");
                DoTheMagic(dimension);
                dimension.PrintLayers();
                
                Console.WriteLine();
                Console.WriteLine("-----------------------------");
                Console.WriteLine();
            }
            
            dimension.PrintLayers();
            return dimension.CountActiveCubes();
        }

        private static void DoTheMagic(Dimension dimension)
        {
            dimension.ExpandDimension();
            // Update new dimension
        }
    }
}