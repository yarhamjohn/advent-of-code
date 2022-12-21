using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode2022.Day19;

public static class Day19
{
    public static long CalculateQualityLevels(string[] input)
    {
        return 0;
    }

    public class Me
    {
        private Dictionary<Type, int> NumMinerals = new() { { Type.Ore, 0 }, { Type.Clay, 0 }, { Type.Obsidian, 0 }, { Type.Geode, 0 }};
        private Dictionary<Type, int> NumRobots = new() { { Type.Ore, 0 }, { Type.Clay, 0 }, { Type.Obsidian, 0 }, { Type.Geode, 0 }};

        public void BuildRobot(Type robot)
        {
            NumRobots[robot]++;
        }
        
        public void CollectOres()
        {
            foreach (var robot in NumRobots)
            {
                NumMinerals[robot.Key] += robot.Value;
            }
        }

        public List<Type?> OptionsForPurchase(Blueprint blueprint)
        {
            var options = new List<Type?> { null }; // There is always the option to not buy a robot at all
            foreach (var robot in blueprint.Costs)
            {
                foreach (var mineral in robot.Value)
                {
                    if (NumMinerals[mineral.Key] >= mineral.Value)
                    {
                        options.Add(robot.Key);
                    }
                }
            }
            
            return options;
        }
    }

    public record Blueprint(int Id, Dictionary<Type, Dictionary<Type, int>> Costs);

    public enum Type
    {
        Ore,
        Clay,
        Obsidian,
        Geode
    }
}