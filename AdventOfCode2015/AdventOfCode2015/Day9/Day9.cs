namespace AdventOfCode2015.Day9
{
    public static class Day9
    {
        public static int GetMinDistance(string[] input)
        {
            var graph = BuildGraph(input);

            var perms = GetNodePermutations(graph, graph.Count);

            var result = int.MaxValue;
            
            foreach (var perm in perms)
            {
                var distance = GetDistance(perm);
                
                Console.WriteLine($"{string.Join(" -> ", perm.Select(x => x.Name))}: {distance}");

                if (distance < result)
                {
                    result = distance;
                }
            }

            return result;
        }
        
        public static int GetMaxDistance(string[] input)
        {
            var graph = BuildGraph(input);

            var perms = GetNodePermutations(graph, graph.Count);

            var result = int.MinValue;
            
            foreach (var perm in perms)
            {
                var distance = GetDistance(perm);
                
                Console.WriteLine($"{string.Join(" -> ", perm.Select(x => x.Name))}: {distance}");

                if (distance > result)
                {
                    result = distance;
                }
            }

            return result;
        }

        private static int GetDistance(List<Node> perm)
        {
            var distance = 0;
            
            for (var i = 0; i < perm.Count - 1; i++)
            {
                distance += perm[i].Edges.Single(e => e.Child.Name == perm[i + 1].Name).Distance;
            }
            
            return distance;
        }

        private static IEnumerable<List<Node>> GetNodePermutations(List<Node> nodes, int length)
        {
            if (length == 1)
            {
                return nodes.Select(x => new List<Node> { x }).ToList();
            }

            return GetNodePermutations(nodes, length - 1)
                .SelectMany(x => nodes.Where(n => !x.Contains(n)), (x2, n2) => x2.Concat(new List<Node> { n2 }).ToList());
        }

        private static List<Node> BuildGraph(string[] input)
        {
            var lines = input.Select(x => x.Split(" ")).ToArray();
            var nodes = lines.SelectMany(x => new[] { x[0], x[2] }).Distinct().Select(x => new Node(x)).ToArray();

            foreach (var line in lines)
            {
                nodes.Single(x => x.Name == line[0])
                    .AddEdge(Convert.ToInt32(line[4]), nodes.Single(x => x.Name == line[2]));
                nodes.Single(x => x.Name == line[2])
                    .AddEdge(Convert.ToInt32(line[4]), nodes.Single(x => x.Name == line[0]));
            }

            return nodes.ToList();
        }

        private record Edge(int Distance, Node Child)
        {
            public override string ToString()
            {
                return $"Distance: {Distance}, Child: {Child.Name}";
            }
        }

        private class Node
        {
            public readonly string Name;
            public readonly List<Edge> Edges = new();

            public Node(string name)
            {
                Name = name;
            }

            public void AddEdge(int distance, Node endNode)
            {
                Edges.Add(new Edge(distance, endNode));
            }
        }
    }
}