namespace AdventOfCode2015.Day24;

public static class Day24
{
    public static long CalculateQuantumEntanglement(IEnumerable<string> input, int compartments)
    {
        var packages = input.Select(x => Convert.ToInt64(x)).ToArray();
        var targetWeight = packages.Sum() / compartments;

        var quantumEntanglement = long.MaxValue;
        
        for (var combinationLength = 1; combinationLength <= packages.Length; combinationLength++)
        {
            var combinations1 = GetCombinationsMatchingTargetWeight(packages, combinationLength, targetWeight);
            if (!combinations1.Any()) continue;

            foreach (var c1 in combinations1)
            {
                var nextQuantumEntanglement = c1.Aggregate((a, b) => a * b);
                if (nextQuantumEntanglement > quantumEntanglement) continue;

                var p2 = packages.Where(x => !c1.Contains(x)).ToArray();
                for (var l2 = 1; l2 <= p2.Length; l2++)
                {
                    var combinations2 = GetCombinationsMatchingTargetWeight(p2, l2, targetWeight);
                    if (!combinations2.Any()) continue;
                    
                    foreach (var p3 in GetUnusedPackages(combinations2, p2))
                    {
                        for (var l3 = 1; l3 <= p3.Length; l3++)
                        {
                            var combinations3 = GetCombinationsMatchingTargetWeight(p3, l3, targetWeight);
                            if (!combinations3.Any()) continue;
                            
                            if (compartments == 3 && nextQuantumEntanglement < quantumEntanglement)
                            {
                                quantumEntanglement = nextQuantumEntanglement;
                                continue;
                            }
                            
                            foreach (var p4 in GetUnusedPackages(combinations3, p3))
                            {
                                for (var l4 = 1; l4 <= p4.Length; l4++)
                                {
                                    var combinations4 = GetCombinationsMatchingTargetWeight(p4, l4, targetWeight);
                                    if (!combinations4.Any()) continue;
                                    
                                    if (nextQuantumEntanglement < quantumEntanglement)
                                    {
                                        quantumEntanglement = nextQuantumEntanglement;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (quantumEntanglement < long.MaxValue) break;
        }

        return quantumEntanglement;
    }

    private static IEnumerable<long[]> GetUnusedPackages(List<long[]> combinations2, long[] p2)
    {
        return combinations2.Select(c2 => p2.Where(x => !c2.Contains(x)).ToArray());
    }

    private static List<long[]> GetCombinationsMatchingTargetWeight(long[] packages, int combinationLength, long targetWeight)
    {
        return GetUniqueCombinations(packages, combinationLength).Where(x => x.Sum() == targetWeight).ToList();
    }

    private static IEnumerable<long[]> GetUniqueCombinations(long[] packages, long length)
    {
        for (var position = 0; position < packages.Length; position++)
        {
            var package = packages[position];

            if (length == 1)
            {
                yield return new[] { package };
            }
            else
            {
                var combinations = GetUniqueCombinations(packages.Skip(position + 1).ToArray(), length - 1);
                foreach (var result in combinations)
                {
                    yield return new[] { package }.Concat(result).ToArray();
                }
            }
        }
    }
}