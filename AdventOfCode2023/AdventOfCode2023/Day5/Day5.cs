namespace AdventOfCode2023.Day5;

public static class Day5
{
    public static long GetLowestLocationNumber(string[]input)
    {
        var seeds = input.First().Split(": ")[1].Split(" ").Select(long.Parse).ToList();
        
        var mappings = ParseInput(input[1..]);

        return seeds.Min(seed => GetLocation(seed, mappings));
    }

    public static long GetLowestLocationNumberRange(string[] input)
    {
        var initialSeeds = input.First().Split(": ")[1].Split(" ").Select(long.Parse).Chunk(2).Select(x => new SeedRange(x.First(), x.Last())).ToList();
        var mappings = ParseInput(input[1..]);

        var seedsWithSoil = new List<SeedRange>();
        for (var i = 0; i < initialSeeds.Count; i++)
        {
            var maps = mappings["seed-to-soil"];
            foreach (var map in maps)
            {
                // TODO: Need to be aware of all the maps before adding any ranges that have no soil match :/
                seedsWithSoil.AddRange(initialSeeds[i].GetSoilRange(map));
            }
        }

        return seedsWithSoil.Min(x => x.Start);
    }

    private class SeedRange
    {
        public long Start { get; }
        public long Length { get; }

        private long _soilStart;
        private long _soilLength;

        public SeedRange(long start, long length)
        {
            Start = start;
            Length = length;
        }

        public SeedRange(long start, long length, long soilStart, long soilLength)
        {
            Start = start;
            Length = length;
            _soilStart = soilStart;
            _soilLength = soilLength;
        }

        public List<SeedRange> GetSoilRange(Mapping mapping)
        {
            if (IsEntirelyInRange(mapping))
            {
                _soilStart = mapping.DestinationStart;
                _soilLength = mapping.Length;

                return new List<SeedRange> { new SeedRange(Start, Length, _soilStart, _soilLength) };
            }

            if (OverlapsRangeEnd(mapping))
            {
                return new List<SeedRange>
                {
                    new SeedRange(mapping.DestinationStart, _soilLength,_soilStart, _soilLength),
                    new SeedRange(Start + Length, Start + Length - (_soilStart + _soilLength), Start + Length, Start + Length - (_soilStart + _soilLength))
                };
            }
            
            if (OverlapsRangeStart(mapping))
            {
                return new List<SeedRange>
                {
                    new SeedRange(Start, _soilStart - Start, Start, _soilStart - Start),
                    new SeedRange(mapping.DestinationStart, _soilLength,_soilStart, _soilLength)
                };
            }
            
            if (OverlapsEntireRange(mapping))
            {
                return new List<SeedRange>
                {
                    new SeedRange(Start, _soilStart - Start, Start, _soilStart - Start),
                    new SeedRange(mapping.DestinationStart, _soilLength,_soilStart, _soilLength),
                    new SeedRange(Start + Length, Start + Length - (_soilStart + _soilLength), Start + Length, Start + Length - (_soilStart + _soilLength))
                };
            }
            
            if (DoesNotOverlapRange(mapping))
            {
                return new List<SeedRange> { new SeedRange(Start, Length, Start, Length) };
            }

            return [];
        }

        private bool OverlapsRangeEnd(Mapping mapping)
        {
            return mapping.IsInRange(Start) && !mapping.IsInRange(Start + Length - 1);
        }
        
        private bool OverlapsRangeStart(Mapping mapping)
        {
            return !mapping.IsInRange(Start) && mapping.IsInRange(Start + Length - 1);
        }

        private bool OverlapsEntireRange(Mapping mapping)
        {
            return mapping.SourceStart > Start && mapping.SourceStart + mapping.Length < Start + Length;
        }
        
        private bool DoesNotOverlapRange(Mapping mapping)
        {
            return mapping.SourceStart + mapping.Length < Start || mapping.SourceStart > Start + Length;
        }

        private bool IsEntirelyInRange(Mapping mapping)
        {
            return mapping.IsInRange(Start) && mapping.IsInRange(Start + Length - 1);
        }
    }

    private static IEnumerable<long> GetSeedsFromRange(long start, long length)
    {
        for (var i = 0; i < length; i++)
        {
            yield return start + i;
        }
    }

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
                mappings[name].Add(new Mapping(name, mapNums[1], mapNums[0], mapNums[2]));
            }
        }

        return mappings;
    }

    private static Dictionary<long, long> soilToLocationMap = new Dictionary<long, long>();
    private static Dictionary<long, long> fertilizerToLocationMap = new Dictionary<long, long>();
    private static Dictionary<long, long> waterToLocationMap = new Dictionary<long, long>();
    private static Dictionary<long, long> lightToLocationMap = new Dictionary<long, long>();
    private static Dictionary<long, long> temperatureToLocationMap = new Dictionary<long, long>();
    private static Dictionary<long, long> humidityToLocationMap = new Dictionary<long, long>();

    private static long GetLocation(long seed, Dictionary<string,List<Mapping>> mappings)
    {
        var soil = GetNextCategoryNum(seed, mappings["seed-to-soil"]);
        if (soilToLocationMap.TryGetValue(soil, out var soilToLocation))
        {
            return soilToLocation;
        }
        
        var fertilizer = GetNextCategoryNum(soil, mappings["soil-to-fertilizer"]);
        if (fertilizerToLocationMap.TryGetValue(fertilizer, out var fertilizerToLocation))
        {
            return fertilizerToLocation;
        }
        
        var water = GetNextCategoryNum(fertilizer, mappings["fertilizer-to-water"]);
        if (waterToLocationMap.TryGetValue(water, out var waterToLocation))
        {
            return waterToLocation;
        }
        
        var light = GetNextCategoryNum(water, mappings["water-to-light"]);
        if (lightToLocationMap.TryGetValue(light, out var lightToLocation))
        {
            return lightToLocation;
        }
        
        var temperature = GetNextCategoryNum(light, mappings["light-to-temperature"]);
        if (temperatureToLocationMap.TryGetValue(temperature, out var temperatureToLocation))
        {
            return temperatureToLocation;
        }
        
        var humidity = GetNextCategoryNum(temperature, mappings["temperature-to-humidity"]);
        if (humidityToLocationMap.TryGetValue(humidity, out var humidityToLocation))
        {
            return humidityToLocation;
        }
        
        var location = GetNextCategoryNum(humidity, mappings["humidity-to-location"]);

        soilToLocationMap[soil] = location;
        fertilizerToLocationMap[fertilizer] = location;
        waterToLocationMap[water] = location;
        lightToLocationMap[light] = location;
        temperatureToLocationMap[temperature] = location;
        humidityToLocationMap[humidity] = location;

        return location;
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
