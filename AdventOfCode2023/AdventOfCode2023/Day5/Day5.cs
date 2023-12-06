namespace AdventOfCode2023.Day5;

public static class Day5
{
    public static long GetLowestLocationNumber(string[]input)
    {
        var seeds = input.First().Split(": ")[1].Split(" ").Select(long.Parse).ToList();
        return seeds.Min(seed => GetLocation(seed, ParseInput(input[1..])));
    }

    public static long GetLowestLocationNumberRange(string[] input)
    {
        var initialSeeds = input.First().Split(": ")[1].Split(" ").Select(long.Parse).Chunk(2).Select(x => (x.First(), x.Last())).ToList();
        var mappings = ParseInput(input[1..]);

        var soilSeeds = CalculateRanges(initialSeeds, mappings["seed-to-soil"].OrderBy(x => x.SourceStart));
        var fertilizerSeeds = CalculateRanges(soilSeeds, mappings["soil-to-fertilizer"].OrderBy(x => x.SourceStart));
        var waterSeeds = CalculateRanges(fertilizerSeeds, mappings["fertilizer-to-water"].OrderBy(x => x.SourceStart));
        var lightSeeds = CalculateRanges(waterSeeds, mappings["water-to-light"].OrderBy(x => x.SourceStart));
        var temperatureSeeds = CalculateRanges(lightSeeds, mappings["light-to-temperature"].OrderBy(x => x.SourceStart));
        var humiditySeeds = CalculateRanges(temperatureSeeds, mappings["temperature-to-humidity"].OrderBy(x => x.SourceStart));
        var locationSeeds = CalculateRanges(humiditySeeds, mappings["humidity-to-location"].OrderBy(x => x.SourceStart));
        
        return locationSeeds.Min(x => x.start);
    }

    private static List<(long start, long length)> CalculateRanges(List<(long, long)> seeds, IOrderedEnumerable<Mapping> maps)
    {
        var newSeeds = new List<(long start, long length)>();
        foreach (var seed in seeds)
        {
            var mutantSeed = seed;
            var nonMatchedSeeds = new List<(long start, long length)>();
            var matchedRanges = new List<(long start, long length)>();

            var mapArray = maps.ToArray();
            for (var i = 0L; i < mapArray.Length; i++)
            {
                var map = mapArray[i];
                
                // ----SSSS----
                // ----------XX
                if (IsEntirelyBeforeRange(map, mutantSeed))
                {
                    nonMatchedSeeds.Add(mutantSeed);
                    break;
                }

                // ----SSSS----
                // ------XXXX--
                if (OverlapsRangeStart(map, mutantSeed))
                {
                    var overlapStart = map.SourceStart;
                    var overlapLength = mutantSeed.Item1 + mutantSeed.Item2 - map.SourceStart;
                    matchedRanges.Add((map.GetDestinationNumber(overlapStart), overlapLength));
                    nonMatchedSeeds.Add((mutantSeed.Item1, mutantSeed.Item2 - overlapLength));
                    break;
                }
                
                // ----SSSS----
                // --XXXX------
                if (OverlapsRangeEnd(map, mutantSeed))
                {
                    var overlapLength = map.SourceStart + map.Length - mutantSeed.Item1;
                    matchedRanges.Add((map.GetDestinationNumber(mutantSeed.Item1), overlapLength));
                    mutantSeed = (mutantSeed.Item1 + overlapLength, mutantSeed.Item2 - overlapLength);
                }
                
                // ----SSSS----
                // -----XX-----
                if (OverlapsEntireRange(map, mutantSeed))
                {
                    matchedRanges.Add((map.GetDestinationNumber(map.SourceStart), map.Length));

                    nonMatchedSeeds.Add((mutantSeed.Item1, map.SourceStart - mutantSeed.Item1));
                    mutantSeed = (map.SourceStart + map.Length, mutantSeed.Item1 + mutantSeed.Item2 - (map.SourceStart + map.Length));
                }
                
                // ----SSSS----
                // --XXXXXXXX--
                if (IsEntirelyInRange(map, mutantSeed))
                {
                    matchedRanges.Add((map.GetDestinationNumber(mutantSeed.Item1), mutantSeed.Item2));
                    break;
                }
                
                // ----SSSS----
                // XX----------
                if (IsEntirelyAfterRange(map, mutantSeed))
                {
                    // For completeness, but nothing needs doing
                }

                if (i == mapArray.Length - 1L)
                {
                    nonMatchedSeeds.Add(mutantSeed);
                }
            }
            
            newSeeds.AddRange(matchedRanges);
            newSeeds.AddRange(nonMatchedSeeds);
        }

        return newSeeds;
    }

    private static bool OverlapsRangeEnd(Mapping mapping, (long start, long length) seed)
        => mapping.IsInRange(seed.start) && !mapping.IsInRange(seed.start + seed.length - 1L);
        
    private static bool OverlapsRangeStart(Mapping mapping, (long start, long length) seed)
        => !mapping.IsInRange(seed.start) && mapping.IsInRange(seed.start + seed.length - 1L);

    private static bool OverlapsEntireRange(Mapping mapping, (long start, long length) seed)
        => mapping.SourceStart > seed.start && mapping.SourceStart + mapping.Length < seed.start + seed.length;
        
    private static bool IsEntirelyAfterRange(Mapping mapping, (long start, long length) seed)
        => mapping.SourceStart + mapping.Length < seed.start;
        
    private static bool IsEntirelyBeforeRange(Mapping mapping, (long start, long length) seed)
        => seed.start + seed.length < mapping.SourceStart;

    private static bool IsEntirelyInRange(Mapping mapping, (long start, long length) seed)
        => mapping.IsInRange(seed.start) && mapping.IsInRange(seed.start + seed.length - 1L);

    private static Dictionary<string, List<Mapping>> ParseInput(IEnumerable<string> input)
    {
        var mappings = new Dictionary<string, List<Mapping>>();
        
        var name = "";
        foreach (var line in input)
        {
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
                mappings[name].Add(new Mapping(mapNums[1], mapNums[0], mapNums[2]));
            }
        }

        return mappings;
    }

    private static readonly Dictionary<long, long> SoilToLocationMap = new();
    private static readonly Dictionary<long, long> FertilizerToLocationMap = new();
    private static readonly Dictionary<long, long> WaterToLocationMap = new();
    private static readonly Dictionary<long, long> LightToLocationMap = new();
    private static readonly Dictionary<long, long> TemperatureToLocationMap = new();
    private static readonly Dictionary<long, long> HumidityToLocationMap = new();

    private static long GetLocation(long seed, IReadOnlyDictionary<string, List<Mapping>> mappings)
    {
        var soil = GetNextCategoryNum(seed, mappings["seed-to-soil"]);
        if (SoilToLocationMap.TryGetValue(soil, out var soilToLocation))
        {
            return soilToLocation;
        }
        
        var fertilizer = GetNextCategoryNum(soil, mappings["soil-to-fertilizer"]);
        if (FertilizerToLocationMap.TryGetValue(fertilizer, out var fertilizerToLocation))
        {
            return fertilizerToLocation;
        }
        
        var water = GetNextCategoryNum(fertilizer, mappings["fertilizer-to-water"]);
        if (WaterToLocationMap.TryGetValue(water, out var waterToLocation))
        {
            return waterToLocation;
        }
        
        var light = GetNextCategoryNum(water, mappings["water-to-light"]);
        if (LightToLocationMap.TryGetValue(light, out var lightToLocation))
        {
            return lightToLocation;
        }
        
        var temperature = GetNextCategoryNum(light, mappings["light-to-temperature"]);
        if (TemperatureToLocationMap.TryGetValue(temperature, out var temperatureToLocation))
        {
            return temperatureToLocation;
        }
        
        var humidity = GetNextCategoryNum(temperature, mappings["temperature-to-humidity"]);
        if (HumidityToLocationMap.TryGetValue(humidity, out var humidityToLocation))
        {
            return humidityToLocation;
        }
        
        var location = GetNextCategoryNum(humidity, mappings["humidity-to-location"]);

        SoilToLocationMap[soil] = location;
        FertilizerToLocationMap[fertilizer] = location;
        WaterToLocationMap[water] = location;
        LightToLocationMap[light] = location;
        TemperatureToLocationMap[temperature] = location;
        HumidityToLocationMap[humidity] = location;

        return location;
    }

    private static long GetNextCategoryNum(long seed, IEnumerable<Mapping> mappings)
        => mappings.SingleOrDefault(map => map.IsInRange(seed))?.GetDestinationNumber(seed) ?? seed;
}

public class Mapping(long sourceStart, long destinationStart, long length)
{
    public readonly long SourceStart = sourceStart;
    public readonly long Length = length;

    public bool IsInRange(long sourceNumber)
        => sourceNumber >= SourceStart && sourceNumber < SourceStart + Length;

    public long GetDestinationNumber(long sourceNumber)
        => sourceNumber - SourceStart + destinationStart;
}
