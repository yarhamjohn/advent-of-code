namespace AdventOfCode2022.Day19;

public static class Day19
{
    public static long CalculateQualityLevels(string[] input)
    {
        return ParseInput(input).Select(x => CalculateQualityLevel(x) * x.Id).Sum();
    }

    private static int CalculateQualityLevel(Blueprint blueprint)
    {
        var minerals = new Dictionary<Type, int> { { Type.Ore, 0 }, { Type.Clay, 0 }, { Type.Obsidian, 0 }, { Type.Geode, 0 }};
        var robots = new Dictionary<Type, int> { { Type.Ore, 1 }, { Type.Clay, 0 }, { Type.Obsidian, 0 }, { Type.Geode, 0 }};

        var turnsToSkipAtStart = Math.Min(blueprint.Costs[Type.Ore].Values.Single(), blueprint.Costs[Type.Clay].Values.Single());

        minerals[Type.Ore] += turnsToSkipAtStart;
        
        return Thing(blueprint, turnsToSkipAtStart, robots, minerals);
    }

    private static int Thing(Blueprint blueprint, int timeElapsed, Dictionary<Type, int> robots, Dictionary<Type, int> minerals)
    {
        if (timeElapsed >= 24)
        {
            return minerals[Type.Geode];
        }
        
        // Cannot collect any geodes in time
        if (robots[Type.Geode] == 0 && robots[Type.Obsidian] == 0 && robots[Type.Clay] == 0 && timeElapsed >= 21)
        {
            return 0;
        }
        
        if (robots[Type.Geode] == 0 && robots[Type.Obsidian] == 0 && timeElapsed >= 22)
        {
            return 0;
        }
        
        if (robots[Type.Geode] == 0 && timeElapsed >= 23)
        {
            return 0;
        }
        
        var options = OptionsForPurchase(blueprint, robots, minerals);
        
        CollectOres(robots, minerals);

        var geodeTotals = new List<int> { 0 };
        foreach (var option in options)
        {
            if (option is null)
            {
                geodeTotals.Add(Thing(blueprint, timeElapsed + 1, CloneDictionary(robots), CloneDictionary(minerals)));
            }
            else
            {
                var newRobots = CloneDictionary(robots);
                newRobots[(Type)option]++;
                
                var newMinerals = CloneDictionary(minerals);
                foreach (var mineral in blueprint.Costs[(Type)option])
                {
                    newMinerals[mineral.Key] -= mineral.Value;
                }
                
                geodeTotals.Add(Thing(blueprint, timeElapsed + 1, newRobots, newMinerals));
            }
        }

        return geodeTotals.Max();
    }

    private static Dictionary<Type, int> CloneDictionary(Dictionary<Type, int> dict)
    {
        return dict.ToDictionary(x => x.Key, x => x.Value);
    }

    private static IEnumerable<Blueprint> ParseInput(string[] input)
    {
        foreach (var line in input)
        {
            var segments = line.Split(" ");
            var costs = new Dictionary<Type, Dictionary<Type, int>>
            {
                {Type.Ore, new Dictionary<Type, int>
                {
                    {Type.Ore, Convert.ToInt32(segments[6])}
                }},
                {Type.Clay, new Dictionary<Type, int>
                {
                    {Type.Ore, Convert.ToInt32(segments[12])}
                }},
                {Type.Obsidian, new Dictionary<Type, int>
                {
                    {Type.Ore, Convert.ToInt32(segments[18])},
                    {Type.Clay, Convert.ToInt32(segments[21])}
                }},
                {Type.Geode, new Dictionary<Type, int>
                {
                    {Type.Ore, Convert.ToInt32(segments[27])},
                    {Type.Obsidian, Convert.ToInt32(segments[30])}
                }}
            };
            
            yield return new Blueprint(Convert.ToInt32(segments[1].Replace(":", "")), costs);
        }
    }

    private static void CollectOres(Dictionary<Type, int> robots, Dictionary<Type, int> minerals)
    {
        foreach (var robot in robots)
        {
            minerals[robot.Key] += robot.Value;
        }
    }
    
    private static List<Type?> OptionsForPurchase(Blueprint blueprint, Dictionary<Type, int> robots, Dictionary<Type, int> minerals)
    {
        if (CanBuy(blueprint.Costs[Type.Geode], minerals))
        {
            return new List<Type?> { Type.Geode };
        }

        var options = new List<Type?>();
        foreach (var robot in blueprint.Costs)
        {        
            // If we own enough ore robots to fully regenerate ore after any purchase (i.e. 4 or more) then don't offer it as an option
            if (robot.Key == Type.Ore && robots[Type.Ore] > 4)
            {
                continue;
            }
            
            if (robot.Value.All(x => minerals[x.Key] >= x.Value))
            {
                options.Add(robot.Key);
            }
        }

        if (!options.Any())
        {
            return new List<Type?> { null };
        }
        
        if (CanBuy(blueprint.Costs[Type.Ore], minerals) &&
            CanBuy(blueprint.Costs[Type.Clay], minerals) &&
            CanBuy(blueprint.Costs[Type.Obsidian], minerals))
        {
            return new List<Type?> { Type.Obsidian };
        }
        
        // If you can't buy 1+ of the robots, then waiting is a valid option
        if (!CanBuy(blueprint.Costs[Type.Ore], minerals)
            || !CanBuy(blueprint.Costs[Type.Clay], minerals))
        {
            options.Add(null);
        }
        
        return options;
    }

    private static bool CanBuy(Dictionary<Type,int> blueprintCost, Dictionary<Type, int> mineral)
    {
        return blueprintCost.All(x => x.Value <= mineral[x.Key]);
    }

    private record Blueprint(int Id, Dictionary<Type, Dictionary<Type, int>> Costs);

    private enum Type
    {
        Ore,
        Clay,
        Obsidian,
        Geode
    }
}