namespace AdventOfCode2015.Day24;

public static class Day24
{
    public static long CalculateQuantumEntanglement(IEnumerable<string> input)
    {
        var packages = input.Select(x => Convert.ToInt32(x)).ToArray();

        var totalWeight = packages.Sum();
        var groupWeight = totalWeight / 3;

        // Get all possible combinations of 3 groups that each weigh the same as the groupWeight
        var groupCombinations = GetGroupCombinations(packages, groupWeight);

        var fewestGroupOnePackages = GetFewestPackages(groupCombinations.Select(x => x.First().ToList()).ToList());

        return GetQuantumEntanglement(fewestGroupOnePackages).Min();
    }

    private static IEnumerable<int> GetQuantumEntanglement(List<List<int>> fewestGroup1Packages)
    {
        return fewestGroup1Packages.Select(x => x.Aggregate((a, b) => a * b)).ToList();
    }

    private static List<List<int>> GetFewestPackages(IReadOnlyCollection<List<int>> groupCombination)
    {
        return groupCombination.Where(x => x.Count == groupCombination.Min(y => y.Count)).ToList();
    }

    private static IEnumerable<IEnumerable<int[]>> GetGroupCombinations(int[] packages, int groupWeight)
    {
        Console.WriteLine(string.Join(",", packages));
        Console.WriteLine("------------------------");
        var possibleCombinations = GetPossibleCombinations(packages, groupWeight).OrderBy(x => x.Length).ToList();

        Console.WriteLine(possibleCombinations.Count);

        int length;
        int? maxLength = null;

        var permutations = new List<IEnumerable<int[]>>();
        foreach (var combination in possibleCombinations)
        {
            length = combination.Length;

            if (length > maxLength)
            {
                break;
            }
            
            //TODO we know the shortest possible valid is 6. answer is 10439961859

            var eligibleCombinations = possibleCombinations.Where(x => !x.Intersect(combination).Any()).ToList();
            eligibleCombinations.Add(combination);
            Console.WriteLine($"Combination: {string.Join(",", combination)}; # Eligible: {eligibleCombinations.Count}");
            foreach (var x in eligibleCombinations)
            {
                File.WriteAllLines("test.txt", new [] {string.Join(",", x)});
            }
            
            
            var result = GetPermutations(eligibleCombinations, 3)
                .Where(x => x.SelectMany(y => y).Distinct().Count() == packages.Length).ToList();

            if (result.Any())
            {
                permutations.AddRange(result);
                maxLength = length;
            }
        }

        Console.WriteLine(permutations.Count);

        return permutations;
    }

    private static List<int[]> GetPossibleCombinations(int[] packages, int groupWeight)
    {
        var result = new List<int[]>();

        for (var i = 1; i < packages.Length; i++)
        {
            // skip remaining loops since no combinations of this size can meet the weight
            if (packages.OrderBy(x => x).ToArray()[..i].Sum() > groupWeight)
            {
                break;
            }

            var possibleCombinations = GetUniqueCombinations(packages, i).Where(x => x.Sum() == groupWeight);
            result.AddRange(possibleCombinations);
        }

        return result;
    }

    private static IEnumerable<int[]> GetUniqueCombinations(int[] packages, int length)
    {
        for (var i = 0; i < packages.Length; i++)
        {
            var package = packages[i];

            if (length == 1)
            {
                yield return new[] { package };
            }
            else
            {
                var combinations = GetUniqueCombinations(packages.Skip(i + 1).ToArray(), length - 1);
                foreach (var result in combinations)
                {
                    yield return new[] { package }.Concat(result).ToArray();
                }
            }
        }
    }
    
    private static IEnumerable<IEnumerable<int[]>> GetPermutations(IEnumerable<int[]> combinations, int length)
    {
        if (length == 1)
        {
            return combinations.Select(x => new[] { x });
        }

        return GetPermutations(combinations, length - 1)
            .SelectMany(x => combinations.Where(y => !x.Contains(y)),
                (arr, num) => arr.Concat(new[] { num }).ToArray());

    }
}