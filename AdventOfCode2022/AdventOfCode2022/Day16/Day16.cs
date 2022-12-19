using System.ComponentModel.Design;

namespace AdventOfCode2022.Day16;

public static class Day16
{
    public static long CalculatePressureReleased(string[] input)
    {
        var valves = GetValves(input);
        var distances = GetDistances(valves);
        var permutations = GetPermutations(valves);

        return GetPressuresPerRoute(permutations, distances, valves, 30).Max(x => x.Value);
    }
    public static long CalculatePressureReleasedWithElephant(string[] input)
    {
        var valves = GetValves(input);
        var distances = GetDistances(valves);
        var permutations = GetPermutations(valves);
        var pressures = GetPressuresPerRoute(permutations, distances, valves, 26);

        return pressures.Aggregate(0, (current, next) => 
            (from route in pressures where !next.Key.Intersect(route.Key).Any() select next.Value + route.Value).Prepend(current).Max());
    }

    private static string[][] GetPermutations(Dictionary<string, Valve> valves)
    {
        // Don't bother going to 8 as we can't reach all within 30 mins        
        return Enumerable.Range(7, 1)
            .SelectMany(x => GetRoutePermutations(valves.Where(x1 => x1.Value.FlowRate != 0).Select(y => y.Key).ToArray(), x))
            .ToArray();
    }

    private static Dictionary<string[], int> GetPressuresPerRoute(string[][] routePermutations, Dictionary<string, Dictionary<string, int>> distances, Dictionary<string, Valve> valves, int mins)
    {
        var result = new Dictionary<string[], int>();

        foreach (var route in routePermutations)
        {
            if (GetTotalDistance(distances, route) + route.Length > mins)
            {
                continue;
            }

            var pressure = 0;
            var time = mins;

            for (var step = 0; step < route.Length; step++)
            {
                var distance = step == 0
                    ? distances["AA"].Single(x => x.Key == route[step]).Value
                    : distances[route[step - 1]].Single(y => y.Key == route[step]).Value;

                time -= distance + 1;

                if (time < 0)
                {
                    break;
                }

                pressure += valves[route[step]].FlowRate * time;
            }

            if (time >= 0)
            {
                result[route] = pressure;
            }
        }

        return result;
    }

    private static int GetTotalDistance(Dictionary<string, Dictionary<string, int>> distances, string[] routeCombination)
    {
        var totalDistance = 0;
        for (var i = 0; i < routeCombination.Length - 1; i++)
        {
            if (i == 0)
            {
                totalDistance += distances["AA"][routeCombination[i]];
            }

            totalDistance += distances[routeCombination[i]][routeCombination[i + 1]];
        }

        return totalDistance;
    }

    private static Dictionary<string, Dictionary<string, int>> GetDistances(Dictionary<string, Valve> valves) =>
        valves.ToDictionary(
            x => x.Key,
            x => valves.Where(v => v.Value.Id != x.Key)
                .ToDictionary(
                    y => y.Key,
                    y => GetShortestDistance(x.Value, y.Value, valves)));

    private static int GetShortestDistance(Valve valveOne, Valve valveTwo, Dictionary<string, Valve> valves)
    {
        var distances = valves.ToDictionary(x => x.Key, x => x.Key == valveOne.Id ? 0 : int.MaxValue);

        var queue = new Queue<string>();
        
        var currentValve = valveOne.Id;

        foreach (var neighbor in valves[currentValve].ConnectedValves)
        {
            distances[neighbor] = distances[currentValve] + 1;
            queue.Enqueue(neighbor);
        }
        
        while (queue.Any())
        {
            currentValve = queue.Dequeue();

            foreach (var neighbor in valves[currentValve].ConnectedValves.Where(x => distances[x] == int.MaxValue))
            {
                distances[neighbor] = distances[currentValve] + 1;
                queue.Enqueue(neighbor);
            }
        }

        return distances[valveTwo.Id];
    }

    private static IEnumerable<string[]> GetRoutePermutations(string[] valveIds, int length)
    {
        if (length == 1)
        {
            return valveIds.Select(x => new [] { x }).ToList();
        }

        return GetRoutePermutations(valveIds, length - 1)
            .SelectMany(x => valveIds.Where(n => !x.Contains(n)), (x2, n2) => x2.Concat(new [] { n2 }).ToArray());
    }

    private static Dictionary<string, Valve> GetValves(string[] input)
    {
        var valves = new Dictionary<string, Valve>();
        foreach (var line in input)
        {
            var id = line[6..8];
            var flowRate = Convert.ToInt32(line.Split(";").First().Split("=").Last());
            var connectedValves = line.Split(new [] {" tunnels lead to valves ", " tunnel leads to valve "}, StringSplitOptions.None).Last().Split(", ").ToList();
            valves[id] = new Valve(id, flowRate, connectedValves);
        }

        return valves;
    }
}

internal class Valve
{
    public string Id { get; }
    public int FlowRate { get; }
    public List<string> ConnectedValves { get; }

    public Valve(string id, int flowRate, List<string> connectedValves)
    {
        Id = id;
        FlowRate = flowRate;
        ConnectedValves = connectedValves;
    }
}