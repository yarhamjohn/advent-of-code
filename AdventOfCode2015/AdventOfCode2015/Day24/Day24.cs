namespace AdventOfCode2015.Day24;

public static class Day24
{
    public static long CalculateQuantumEntanglement(IEnumerable<string> input)
    {
        var packages = input.Select(x => Convert.ToInt32(x)).ToArray();

        var totalWeight = packages.Sum();
        var groupWeight = totalWeight / 3;

        var result = int.MaxValue;
        
        for (var combinationLength = 1; combinationLength <= packages.Length; combinationLength++)
        {
            // get all combinations of length i
            var combinations = GetUniqueCombinations(packages, combinationLength);
            
            //Console.WriteLine("First Combinations");
            //foreach (var a in combinations) Console.WriteLine(string.Join(",", a));
            
            // filter for any combinations have required weight.
            var validCombinations = combinations.Where(x => x.Sum() == groupWeight).ToArray();
            
            //Console.WriteLine("First Valid Combinations");
            //foreach (var a in validCombinations) Console.WriteLine(string.Join(",", a));

            // if there are none move to next i
            if (!validCombinations.Any())
            {
                continue;
            }

            // if combinations do exist, repeat on remaining packages
            foreach (var combination in validCombinations)
            {
                var qe = combination.Aggregate((a, b) => a * b);
                if (qe > result)
                {
                    continue;
                }

                // Console.WriteLine(string.Join(",", combination));
                var remainingPackages = packages.Where(x => !combination.Contains(x)).ToArray();
                for (var secondCombinationLength = 1;
                     secondCombinationLength <= remainingPackages.Length;
                     secondCombinationLength++)
                {
                    // get all combinations of length i
                    var secondCombinations = GetUniqueCombinations(remainingPackages, secondCombinationLength);

                    //Console.WriteLine("Second Combinations");
                    //foreach (var a in secondCombinations) Console.WriteLine(string.Join(",", a));
 
                    // filter for any combinations have required weight.
                    var validSecondCombinations = secondCombinations.Where(x => x.Sum() == groupWeight).ToArray();

                    //Console.WriteLine("Second Valid Combinations");
                    //foreach (var a in validSecondCombinations) Console.WriteLine(string.Join(",", a));
                    
                    // if there are none move to next i
                    if (!validSecondCombinations.Any())
                    {
                        continue;
                    }

                    // if combinations do exist, repeat on remaining packages
                    foreach (var comb in validSecondCombinations)
                    {
                        // Console.Write($"\r{string.Join(",", comb)}                               ");
                        var lastPackages = remainingPackages.Where(x => !comb.Contains(x)).ToArray();
                        for (var thirdCombinationLength = 1;
                             thirdCombinationLength <= lastPackages.Length;
                             thirdCombinationLength++)
                        {
                            // get all combinations of length i
                            var thirdCombinations = GetUniqueCombinations(lastPackages, thirdCombinationLength);

                            //Console.WriteLine("Third Combinations");
                            //foreach (var a in thirdCombinations) Console.WriteLine(string.Join(",", a));

                            // filter for any combinations have required weight.
                            var validThirdCombinations = thirdCombinations.Where(x => x.Sum() == groupWeight).ToArray();

                            //Console.WriteLine("Third Valid Combinations");
                            //foreach (var a in validThirdCombinations) Console.WriteLine(string.Join(",", a));
                            
                            // if there are none move to next i
                            if (!validThirdCombinations.Any())
                            {
                                continue;
                            }

                            // Console.WriteLine();
                            // Console.Write($"\r{string.Join(",", validThirdCombinations.Select(x => string.Join("; ", x)))}                               ");

                            foreach (var x in validThirdCombinations)
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

            if (result < int.MaxValue) break;

            // if additional valid combinations are found, save them
            // otherwise, move to next i
        }

        // foreach (var a in result)
        // {
        //     foreach (var b in a)
        //     {
        //         Console.WriteLine(string.Join(",", b));
        //     }
        //
        //     Console.WriteLine("--------------------");
        // }
        return result;
        // var fewestGroup1Packages = result.SelectMany(x => x.Select(y => y.ToList())).ToList();
        // return GetQuantumEntanglement(fewestGroup1Packages).Min();

        // Get all possible combinations of 3 groups that each weigh the same as the groupWeight
        //var groupCombinations = GetGroupCombinations(packages, groupWeight);

        //var fewestGroupOnePackages = GetFewestPackages(groupCombinations.Select(x => x.First().ToList()).ToList());

        //return GetQuantumEntanglement(fewestGroupOnePackages).Min();
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