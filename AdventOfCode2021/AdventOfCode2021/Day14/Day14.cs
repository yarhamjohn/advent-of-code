using System.Text;

namespace AdventOfCode.Day14;

public static class Day14
{
    public static long CalculatePolymerTemplate(string[] input, int turns)
    {
        var rules = ParseRules(input);
        var mappingAfterTurns = GetMappingAfterTurns(rules, turns);

        var startPolymer = input.First();

        var counts = rules.Select(r => r.element).Distinct().ToDictionary(x => x, _ => 0L);
        for (var i = 0; i < startPolymer.Length - 1; i++)
        {
            var pair = startPolymer.Substring(i, 2);
            foreach (var (element, count) in mappingAfterTurns[pair].countGroups)
            {
                counts[element.ToString()] += count;
            }
        }

        return counts.Values.Max() - counts.Values.Where(v => v != 0).Min();
    }
    
    public static long CalculatePolymerTemplateForFortyTurns(string[] input)
    {
        var rules = ParseRules(input);
        var mappingAfter20Turns = GetMappingAfterTurns(rules, 20);

        var startPolymer = input.First();
        
        var counts = rules.Select(r => r.element).Distinct().ToDictionary(x => x, _ => 0L);

        for (var i = 0; i < startPolymer.Length - 1; i++)
        {
            var pair = startPolymer.Substring(i, 2);
            var map = mappingAfter20Turns[pair];
            
            var countOfPairs = new Dictionary<string, long>();
            
            for (var j = 0; j < map.polymer.Length - 1; j++)
            {
                var subPair = map.polymer.Substring(j, 2);
                if (countOfPairs.ContainsKey(subPair))
                {
                    countOfPairs[subPair] += 1;
                }
                else
                {
                    countOfPairs.Add(subPair, 1);
                }
            }

            foreach (var p in countOfPairs)
            {
                var finalChunk = mappingAfter20Turns[p.Key];
                var lastElement = finalChunk.polymer.Last().ToString();
                foreach (var countGroup in finalChunk.countGroups)
                {
                    var key = countGroup.Item1.ToString();
                    var additionalCount = key == lastElement ? countGroup.Item2 - 1 : countGroup.Item2;
                    
                    counts[key] += p.Value * additionalCount;
                }
            }

        }
        counts[startPolymer.Last().ToString()]++;

        return counts.Values.Max() - counts.Values.Where(v => v != 0).Min();
    }

    private static Dictionary<string, (string polymer, IEnumerable<(char, int)> countGroups)> GetMappingAfterTurns((string pair, string element)[] rules, int turns)
    {
        var mapping = new Dictionary<string, (string polymer, IEnumerable<(char, int)> countGroups)>();
        foreach (var (pair, _) in rules)
        {
            mapping.Add(pair, (pair, pair.GroupBy(e => e).Select(x => (x.Key, x.Count()))));

            var pairTurns = turns;
            while (pairTurns > 0)
            {
                var newPolymer = new StringBuilder();
                for (var i = 0; i < mapping[pair].polymer.Length; i++)
                {
                    newPolymer.Append(mapping[pair].polymer[i]);
                    if (i < mapping[pair].polymer.Length - 1)
                    {
                        newPolymer.Append(GetElementToInsert(rules, mapping[pair].polymer.Substring(i, 2)));
                    }
                }

                mapping[pair] = (newPolymer.ToString(), newPolymer.ToString().GroupBy(y => y).Select(x => (x.Key, x.Count())));

                pairTurns--;
            }
        }

        return mapping;
    }
    

    private static (string pair, string element)[] ParseRules(string[] input) =>
        input.Skip(2).Select(x =>
        {
            var rule = x.Split(" -> ");
            return (pair: rule[0], element: rule[1]);
        }).ToArray();

    private static string GetElementToInsert(IEnumerable<(string pair, string element)> rules, string pair) => 
        rules.Single(x => x.pair == pair).element;
}