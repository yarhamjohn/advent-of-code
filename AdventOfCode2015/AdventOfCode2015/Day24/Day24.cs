namespace AdventOfCode2015.Day24;

public static class Day24
{
    public static long CalculateQuantumEntanglement(IEnumerable<string> input)
    {
        var packages = input.Select(x => Convert.ToInt32(x)).ToList();

        var totalWeight = packages.Sum();
        var groupSize = totalWeight / 3;

        var groupCombinations = GetGroupCombinations(packages, groupSize);

        var fewestGroupOnePackages = GetFewestPackages(groupCombinations[1]);

        return GetQuantumEntanglement(fewestGroupOnePackages).Min();
    }

    private static IEnumerable<int> GetQuantumEntanglement(List<List<int>> fewestGroup1Packages)
    {
        return fewestGroup1Packages.Select(x => x.Aggregate((a, b) => a * b)).ToList();
    }

    private static List<List<int>> GetFewestPackages(List<List<int>> groupCombination)
    {
        return groupCombination.Where(x => x.Count == groupCombination.Min(y => y.Count)).ToList();
    }

    private static Dictionary<int, List<List<int>>> GetGroupCombinations(List<int> packages, int groupSize)
    {
        Console.WriteLine(string.Join(",", packages));
        // find all combinations of numbers that add to 508
        var possibleCombinations = GetCombinations(packages, packages.Count).Where(x => x.Sum() == groupSize);

        // return each set of 3 mutually exclusive groups
        return new Dictionary<int, List<List<int>>>() { { 1, new List<List<int>>() } };
    }

    private static IEnumerable<int[]> GetCombinations(List<int> packages, int numPackages)
    {
        // TODO: Requires permutations not combinations. Plus there will be FAR TOO MANY so this has to be cleverer about filtering.
        if (numPackages == 1)
        {
            return packages.Select(x => new[] { x });
        }

        return GetCombinations(packages, numPackages - 1)
            .SelectMany(x => packages.Where(y => !x.Contains(y)),
                (arr, num) => arr.Concat(new[] { num }).ToArray());
    }
}