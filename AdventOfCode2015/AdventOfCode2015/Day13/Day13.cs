namespace AdventOfCode2015.Day13;

public static class Day13
{
    public static int CalculateOptimalHappiness(IEnumerable<string> input)
    {
        var edges = input
            .Select(x => BuildEdge(x.Split(" ")))
            .ToList();

        var names = edges.Select(x => x.PersonOne).Distinct().ToArray();

        var combinations = GetCombinations(names[1..].ToList(), names.Length - 1)
            .Select(x => new[] { names[0] }.Concat(x));

        return combinations.Select(c => GetHappiness(c.ToArray(), edges)).Max();
    }

    private static Edge BuildEdge(IReadOnlyList<string> y) =>
        new(y[0], y[^1][..(y[^1].Length - 1)],
            Convert.ToInt32(y[3]) * (y[2] == "gain" ? 1 : -1));

    private static int GetHappiness(IReadOnlyList<string> arrangement, IReadOnlyCollection<Edge> edges)
    {
        var total = 0;
        for (var i = 0; i < arrangement.Count; i++)
        {
            var next = i == arrangement.Count - 1 ? 0 : i + 1;
            var edgesInvolved = edges
                .Where(e =>
                    e.PersonOne == arrangement[i] && e.PersonTwo == arrangement[next]
                    || e.PersonTwo == arrangement[i] && e.PersonOne == arrangement[next]);

            total += edgesInvolved.Sum(x => x.Happiness);
        }

        return total;
    }

    private static IEnumerable<List<string>> GetCombinations(IReadOnlyCollection<string> names, int count)
    {
        if (count == 1)
        {
            return names.Select(x => new List<string> { x }).ToList();
        }

        return GetCombinations(names, count - 1)
            .SelectMany(x => names.Where(n => !x.Contains(n)), (x2, n2) => x2.Concat(new List<string> { n2 }).ToList());
    }

    private record Edge(string PersonOne, string PersonTwo, int Happiness);
}