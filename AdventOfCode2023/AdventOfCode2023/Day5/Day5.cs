namespace AdventOfCode2023.Day5;

public static class Day5
{
    public static long GetLowestLocationNumber(IEnumerable<string> input)
    {
        var (seeds, mappings) = ParseInput(input);

        return seeds.Min(seed => GetLocation(seed, mappings));
    }

    private static (List<long> seeds, Dictionary<string, List<Mapping>> mappings) ParseInput(IEnumerable<string> input)
    {
        var seeds = new List<long>();
        var mappings = new Dictionary<string, List<Mapping>>();
        
        var name = "";
        foreach (var line in input)
        {
            if (line.Contains("seeds"))
            {
                seeds = line.Split(": ")[1].Split(" ").Select(long.Parse).ToList();
                continue;
            }
            
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            
            if (line.Contains("map"))
            {
                name = line.Split(" ")[0];
                mappings[name] = [];
            }
            else
            {
                var mapNums = line.Split(" ").Select(long.Parse).ToArray();
                mappings[name].Add(new Mapping(name, mapNums[1], mapNums[0], mapNums[2]));
            }
        }

        return (seeds, mappings);
    }

    private static long GetLocation(long seed, Dictionary<string,List<Mapping>> mappings)
    {
        var soil = GetNextCategoryNum(seed, mappings["seed-to-soil"]);
        var fertilizer = GetNextCategoryNum(soil, mappings["soil-to-fertilizer"]);
        var water = GetNextCategoryNum(fertilizer, mappings["fertilizer-to-water"]);
        var light = GetNextCategoryNum(water, mappings["water-to-light"]);
        var temperature = GetNextCategoryNum(light, mappings["light-to-temperature"]);
        var humidity = GetNextCategoryNum(temperature, mappings["temperature-to-humidity"]);
        return GetNextCategoryNum(humidity, mappings["humidity-to-location"]);
    }

    private static long GetNextCategoryNum(long seed, IEnumerable<Mapping> mappings)
    {
        return mappings
            .SingleOrDefault(map => map.IsInRange(seed))?.GetDestinationNumber(seed)
               ?? seed;
    }
}

public class Mapping(string type, long sourceStart, long destinationStart, long length)
{
    public readonly string Type = type;
    public readonly long SourceStart = sourceStart;
    public readonly long DestinationStart = destinationStart;
    public readonly long Length = length;

    public bool IsInRange(long sourceNumber)
    {
        return sourceNumber >= SourceStart && sourceNumber < SourceStart + Length;
    }

    public long GetDestinationNumber(long sourceNumber)
    {
        return sourceNumber - SourceStart + DestinationStart;
    }
}
