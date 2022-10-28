namespace AdventOfCode2015.Day24;

public static class Day24
{
    public static long CalculateQuantumEntanglement(IEnumerable<string> input)
    {
        var packages = input.Select(x => Convert.ToInt64(x)).ToArray();

        var totalWeight = packages.Sum();
        var groupWeight = totalWeight / 3;

        var result = long.MaxValue;
        
        for (var combinationLength = 1; combinationLength <= packages.Length; combinationLength++)
        {
            var combinations = GetUniqueCombinations(packages, combinationLength);
            
            var validCombinations = combinations.Where(x => x.Sum() == groupWeight).ToArray();
            if (!validCombinations.Any())
            {
                continue;
            }

            foreach (var combination in validCombinations)
            {
                var qe = combination.Aggregate((a, b) => a * b);
                if (qe > result)
                {
                    continue;
                }

                var remainingPackages = packages.Where(x => !combination.Contains(x)).ToArray();
                for (var secondCombinationLength = 1;
                     secondCombinationLength <= remainingPackages.Length;
                     secondCombinationLength++)
                {
                    var secondCombinations = GetUniqueCombinations(remainingPackages, secondCombinationLength);
 
                    var validSecondCombinations = secondCombinations.Where(x => x.Sum() == groupWeight).ToArray();
                    if (!validSecondCombinations.Any())
                    {
                        continue;
                    }

                    foreach (var comb in validSecondCombinations)
                    {
                        var lastPackages = remainingPackages.Where(x => !comb.Contains(x)).ToArray();
                        for (var thirdCombinationLength = 1;
                             thirdCombinationLength <= lastPackages.Length;
                             thirdCombinationLength++)
                        {
                            var thirdCombinations = GetUniqueCombinations(lastPackages, thirdCombinationLength);
                            var validThirdCombinations = thirdCombinations.Where(x => x.Sum() == groupWeight).ToArray();
                            if (validThirdCombinations.Any())
                            {
                                var quantumEntanglement = combination.Aggregate((a, b) => a * b);
                                if (quantumEntanglement < result)
                                {
                                    result = quantumEntanglement;
                                }
                            }
                        }
                    }
                }
            }

            if (result < long.MaxValue) break;
        }

        return result;
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