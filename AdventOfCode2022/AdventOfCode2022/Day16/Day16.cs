namespace AdventOfCode2022.Day16;

public static class Day16
{
    public static long CalculatePressureReleased(string[] input)
    {
        var valves = GetValves(input);

        var routeCombinations = GetRouteCombinations(valves.Where(x => x.Value.FlowRate != 0).Select(x => x.Key).ToArray());

        var pressureReleased = 0;
        
        foreach (var routeCombination in routeCombinations)
        {
            var pressure = 0;
            var currentPressure = 0;
            var time = 30;

            for (var step = 0; step < routeCombination.Length; step++)
            {
                var distance = GetShortestDistance(step == 0 ? valves["AA"] : valves[routeCombination[step - 1]], valves[routeCombination[step]], valves);

                var timeElapsed = distance + 1; // Movement and opening time
                
                time -= timeElapsed;
                
                pressure += valves[routeCombination[step]].FlowRate * time; // Add total pressure released for period remaining
            }

            if (pressure > pressureReleased)
            {
                pressureReleased = pressure;
            }
        }
        
        return pressureReleased;
    }

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
            
            var unvisitedNeighbours = valves[currentValve].ConnectedValves.Where(x => distances[x] == int.MaxValue);

            foreach (var neighbor in unvisitedNeighbours)
            {
                distances[neighbor] = distances[currentValve] + 1;
                queue.Enqueue(neighbor);
            }
        }

        return distances[valveTwo.Id];
    }

    private static IEnumerable<string[]> GetRouteCombinations(string[] valveIds)
    {
        // https://stackoverflow.com/questions/7802822/all-possible-combinations-of-a-list-of-values
        return Enumerable.Range(0, 1 << valveIds.Length)
            .Select(index => valveIds.Where((_, i) => (index & (1 << i)) != 0).ToArray());
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