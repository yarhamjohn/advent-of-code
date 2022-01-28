namespace AdventOfCode.Day12;

public static class Day12
{
    public static int CalculateNumberOfPaths(IEnumerable<string> input)
    {
        var nodes = CreateNodes(input);

        var startingNode = nodes.Single(x => x.Name == "start");
        var paths = CreatePath(startingNode, new List<List<Node>> {new() {startingNode }});

        return paths.Count;
    }
    
    public static int CalculateNumberOfPathsWithDoubleVisit(IEnumerable<string> input)
    {
        var nodes = CreateNodes(input);

        var startingNode = nodes.Single(x => x.Name == "start");
        var smallCaves = nodes.Where(n => n.Name.ToLower() == n.Name);

        var paths = new List<List<Node>>();
        foreach (var cave in smallCaves)
        {
            paths.AddRange(CreatePath(startingNode, new List<List<Node>> { new() { startingNode } }, cave));
        }

        return paths.Select(x => string.Join("", x.Select(y => y.Name))).Distinct().Count();
    }

    private static List<List<Node>> CreatePath(Node startNode, List<List<Node>> paths, Node smallCave)
    {
        var newPaths = new List<List<Node>>();
        foreach (var node in startNode.ConnectedNodes)
        {
            foreach (var path in paths.Select(p => new List<Node>(p)))
            {
                path.Add(node);
                if (node.Name == "end")
                {
                    newPaths.Add(path);
                } else if (path.Count(x => x.Name == node.Name) > 2 && node.Name.ToLower() == node.Name && node.Name == smallCave.Name)
                {
                    break;
                } else if (path.Count(x => x.Name == node.Name) > 1 && node.Name.ToLower() == node.Name && node.Name != smallCave.Name)
                {
                    break;
                }
                else if (node.Name == "start")
                {
                    break;
                }
                else
                {
                    newPaths.AddRange(CreatePath(node, new() { path }, smallCave));
                }
            }
        }

        return newPaths;
    }

    private static List<List<Node>> CreatePath(Node startNode, List<List<Node>> paths)
    {
        var newPaths = new List<List<Node>>();
        foreach (var node in startNode.ConnectedNodes)
        {
            foreach (var path in paths.Select(p => new List<Node>(p)))
            {
                path.Add(node);
                if (node.Name == "end")
                {
                    newPaths.Add(path);
                } else if (path.Count(x => x.Name == node.Name) > 1 && node.Name.ToLower() == node.Name)
                {
                    break;
                }
                else if (node.Name == "start")
                {
                    break;
                }
                else
                {
                    newPaths.AddRange(CreatePath(node, new() { path }));
                }
            }
        }

        return newPaths;
    }

    private static List<Node> CreateNodes(IEnumerable<string> input)
    {
        var pairs = input.Select(line =>
            {
                var path = line.Split("-");
                return (from: path[0], to: path[1]);
            }
        );
            
        var nodes = new List<Node>();
        foreach (var (from, to) in pairs)
        {
            var existingNodeNames = nodes.Select(n => n.Name).ToList();
            if (!existingNodeNames.Contains(@from))
            {
                nodes.Add(new(@from));
            }

            if (!existingNodeNames.Contains(to))
            {
                nodes.Add(new(to));
            }

            var startNode = nodes.Single(x => x.Name == @from);
            var endNode = nodes.Single(x => x.Name == to);
            startNode.AddConnection(endNode);
            endNode.AddConnection(startNode);
        }

        return nodes;
    }

    public class Node
    {
        public string Name { get; }
        public readonly List<Node> ConnectedNodes = new();

        public Node(string name)
        {
            Name = name;
        }

        public void AddConnection(Node connectedNode)
        {
            if (!ConnectedNodes.Select(x => x.Name).Contains(connectedNode.Name))
            {
                ConnectedNodes.Add(connectedNode);
            };
        }
    }
}