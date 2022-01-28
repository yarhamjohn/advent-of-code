using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day10
{
    public static class Day10
    {
        public static long GetJoltDifference(List<string> input)
        {
            var adapters = input.Select(x => Convert.ToInt32(x)).ToList();
            adapters.Sort();

            var oneJolt = 0;
            var threeJolt = 1; // There is a 3 volt difference to the device

            var currentJolt = 0;
            foreach (var adapter in adapters)
            {
                if (adapter - currentJolt == 1)
                {
                    oneJolt++;
                }

                if (adapter - currentJolt == 3)
                {
                    threeJolt++;
                }
                
                currentJolt = adapter;
            }
            
            return oneJolt * threeJolt;
        }

        public static long GetAdapterArrangements(List<string> input)
        {
            var adapters = input.Select(x => Convert.ToInt64(x)).ToList();
            adapters.Sort();

            var arrangements = new List<HashSet<long>>();
            var runningTotal = 1L;
            var lastAdapter = 0L;

            foreach (var adapter in adapters)
            {
                if (adapter - lastAdapter == 3)
                {
                    runningTotal *= arrangements.Count(x => x.Last() == adapter - 3);
                    arrangements = new List<HashSet<long>> {new() {adapter}};
                    lastAdapter = adapter;
                    continue;
                }
                
                var indexesToRemove = new List<long>();
                var arrangementsToAdd = new List<HashSet<long>>();
                for (var j = 0; j < arrangements.Count; j++)
                {
                    if (adapter - arrangements[j].Last() >= 3)
                    {
                        indexesToRemove.Add(j);
                    }
                    
                    if (adapter - arrangements[j].Last() <= 3)
                    {
                        var copy = arrangements[j].ToHashSet();
                        copy.Add(adapter);
                        arrangementsToAdd.Add(copy);
                    }
                }

                if (indexesToRemove.Any())
                {
                    for (var index = indexesToRemove.Count - 1; index >= 0; index--)
                    {
                        arrangements.RemoveAt(index);
                    }
                }

                if (arrangementsToAdd.Any())
                {
                    arrangements.AddRange(arrangementsToAdd);
                }

                if (!arrangements.Any() || adapter <= 3)
                {
                    arrangements.Add(new HashSet<long> {adapter});
                }

                lastAdapter = adapter;
            }

            if (adapters[^1] - adapters[^2] == 3)
            {
                return runningTotal;
            }

            return runningTotal * arrangements.Count(x => x.Last() == adapters.Last());
        }
    }
}