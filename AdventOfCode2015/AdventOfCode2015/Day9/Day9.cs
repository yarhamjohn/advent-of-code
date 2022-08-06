using System.Text;
using System.Threading.Tasks.Sources;
using Microsoft.VisualBasic;

namespace AdventOfCode2015.Day9
{
    public static class Day9
    {
        public static int GetDistance(string[] input)
        {
            var graph = BuildGraph(input);
            var nodes = graph
                .SelectMany(x => new[] { x.NodeOne, x.NodeTwo })
                .GroupBy(x => x.Name)
                .Select(x => x.First());

            Console.WriteLine(string.Join(", ", nodes.Select(x => x.Name)));

            var score = int.MaxValue;

            foreach (var node in nodes)
            {
                var runningScore = 0;
                
                // traverse routes using a stack

                if (runningScore < score)
                {
                    score = runningScore;
                }
            }
            
            return score;
        }

        private static List<Edge> BuildGraph(string[] input)
        {
            var lines = input.Select(x => x.Split(" ")).ToArray();

            return lines
                .Select(line => new Edge(
                    Convert.ToInt32(line[4]), 
                    new Node(line[0]), 
                    new Node(line[2])))
                .ToList();
        }

        private record Edge(int Distance, Node NodeOne, Node NodeTwo)
        {
            public override string ToString()
            {
                return $"Distance: {Distance}, NodeOne: {NodeOne.Name}, NodeTwo: {NodeTwo.Name}";
            }
        }

        private class Node
        {
            public readonly string Name;

            public Node(string name)
            {
                Name = name;
            }
        }
    }
}