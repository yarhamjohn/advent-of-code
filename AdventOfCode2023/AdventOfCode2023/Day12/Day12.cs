using System.Collections;
using System.Text;

namespace AdventOfCode2023.Day12;

public static class Day12
{
    public static long SumDamageCombinations(string[] input)
    {
        var lines = input.Select(x => x.Split(" ")).Select(y => (y[0], y[1].Split(",").Select(int.Parse).ToArray()));

        // for each line
        // recursively
        // put first item in string
        // repeat with other items until they don't fit
        // if all fit, add 1 to score
        // each level, shift right until they don't fit

        var combinations = 0;
        foreach (var line in lines)
        {
            var found = GetCombinations(new StringBuilder(), new List<string>(), line.Item1, line.Item2);
            combinations += found.ToHashSet().Count;
        }

        return combinations;
        
        var totalCombinations = 0;
        foreach (var (line, places) in lines)
        {
            var masks = GetMasks(line.Length, places);
            var countMatches = CountMatches(line, masks);
            Console.WriteLine(line + " - " + countMatches);
            
            totalCombinations += countMatches;
        }
        
        return totalCombinations;
    }

    private static List<string> GetCombinations(StringBuilder current, List<string> found, string remainder, int[] springs)
    {
        // There are no more things to fit
        if (springs.Length == 0)
        {
            // Add any trailing bits
            current.Append(remainder);
            
            // Add the mask to the found list
            found.Add(current.ToString());
            
            // return
            return found;
        }
        
        // find first index where it fits
        for (var i = 0; i < remainder.Length; i++)
        {
            // skip known dots
            if (remainder[i] == '.')
            {
                current.Append(remainder[i]);
                continue;
            }
            
            // can't fit the rest in
            if (i + springs.Sum() + (springs.Length - 1) > remainder.Length)
            {
                current.Append(remainder);
                found.Add(current.ToString());
                break;
            }

            var firstSpring = springs.First();
            
            // check it can fit spring
            if (remainder[i..(i + firstSpring)].All(x => x != '.'))
            {
                // check there is no spring immediately after its end
                if (firstSpring == (remainder.Length - i) || remainder[i + firstSpring] != '#')
                {
                    current.Append(Enumerable.Range(0, firstSpring).Select(_ => "#"));
                }
            }
        }

        return found;
    }
    
    private static int num = 0;

    private static void CountCombinations(string conditions, int[] springs)
    {
        Console.WriteLine(conditions + " | " + string.Join(",", springs) + " | " + num);
        // all the springs fitted
        if (springs.Length == 0)
        {
            Console.WriteLine("Add 1");
            num++;
            return;
        }

        var firstSpring = springs.First();

        // find first index where it fits
        for (var i = 0; i < conditions.Length; i++)
        {
            // skip known dots
            if (conditions[i] == '.')
            {
                continue;
            }
            
            // can't fit the rest in
            if (i + springs.Sum() + (springs.Length - 1) > conditions.Length)
            {
                break;
            }
            
            // check it can fit spring
            if (conditions[i..(i + firstSpring)].All(x => x != '.'))
            {
                // check there is no spring immediately after its end
                if (firstSpring == (conditions.Length - i) || conditions[i+firstSpring] != '#')
                {
                    // Add space for a trailing '.' if its not the last spring and the next place is not a '.'
                    var remainingConditions = springs.Length == 1
                        ? conditions[(i + firstSpring)..]
                        : conditions[i + firstSpring] == '.'
                            ? conditions[(i + firstSpring)..]
                            : conditions[(i + firstSpring + 1)..];

                    var remainingSprings = springs[1..];

                    CountCombinations(remainingConditions, remainingSprings);
                }
            }
        }
    }

    public static long SumDamageCombinationsUnfolded(string[] input)
    {
        var lines = input.Select(x => x.Split(" ")).Select(y => (y[0], y[1].Split(",").Select(int.Parse).ToArray()));
        var unfoldedLines = Unfold(lines);

        var totalCombinations = 0;
        foreach (var (line, places) in unfoldedLines)
        {
            var masks = GetMasks(line.Length, places);
            var countMatches = CountMatches(line, masks);
            Console.WriteLine(line + " - " + countMatches);
            
            totalCombinations += countMatches;
        }
        
        return totalCombinations;
    }

    private static List<(string, int[])> Unfold(IEnumerable<(string, int[])> lines)
    {
        var result = new List<(string, int[])>();
        foreach (var line in lines)
        {
            var newString = string.Join("?", Enumerable.Range(0, 5).Select(_ => line.Item1));
            var newPlaces = Enumerable.Range(0, 5).SelectMany(_ => line.Item2).ToArray();
            result.Add((newString, newPlaces));
        }

        return result;
    }

    private static int CountMatches(string line, IEnumerable<string> masks)
    {
        return masks.Count(mask => MaskIsValid(line, mask));
    }

    private static bool MaskIsValid(string line, string mask)
    {
        return line
            .Where((ch, idx) => ch != mask[idx])
            .All(ch => ch == '?');
    }

    public static List<string> GetMasks2(int lineLength, int[] places)
    {
        var result = new List<string>();

        var numDots = lineLength - places.Sum();
        var numGaps = places.Length + 1; // gaps between numbers and start/end
        var minimumString = string.Join(".", places.Select(x => Enumerable.Range(1, x).Select(_ => "#")));
        var remainingDots = lineLength - minimumString.Length;

        if (remainingDots == 0)
        {
            result.Add(minimumString);
        } else if (remainingDots == 1)
        {
            var gapIndexes = minimumString.Select((x, i) => (x, i)).Where(y => y.x == '.').Select(z => z.i).Concat(new List<int> {0, minimumString.Length});
            foreach (var gap in gapIndexes)
            {
                result.Add(minimumString[..gap] + "." + minimumString[gap..]);
            }
        }
        
        return result;
    }

    public static IEnumerable<string> GetMasks(int lineLength, int[] places)
    {
        var options = new List<string>();
        var numDots = lineLength - places.Sum();

        if (numDots == 0)
        {
            options.Add(string.Join("", Enumerable.Range(0, lineLength).Select(_ => '#')));
        }
        else
        {
            var numGaps = places.Length - 1; // Ignore before and after
            if (numGaps == 0)
            {
                options.Add(string.Join("", Enumerable.Range(0, places[0]).Select(_ => '#')));
            }
            else
            {
                var combinations = new List<List<string>>();
                if (numGaps == 1)
                {
                    for (var i = 1; i <= numDots; i++)
                    {
                        combinations.Add([string.Join("", Enumerable.Range(1, i).Select(_ => "."))]);
                    }
                }
                else
                {
                    if (numGaps == numDots)
                    {
                        combinations.Add(Enumerable.Range(0, numGaps).Select(_ =>  ".").ToList());
                    }
                    else
                    {
                        // each gap has 1+ dots
                        // each gap has max dots = numDots - (numGaps - 1)
                        // 2 gaps 3 dots
                        // . . / .. . / . ..
                        for (var a = 1; a <= (numDots - (numGaps - 1)); a++)
                        {
                            var lengths = Enumerable.Range(1, numDots - (numGaps - 1)).Select(x => x);
                            // get all combinations of length 2
                            // remove any where total > numDots
                            var lengthGroups = GetLengthGroups(numGaps,lengths.ToArray());
                            var gapGroup = lengthGroups
                                .Where(x => x.Sum() <= numDots);

                            var selectMany = gapGroup
                                .Select(gaps => gaps
                                    .Select(len => string.Join("", Enumerable.Range(0, len).Select(_ => "."))).ToList())
                                .ToList();
                            
                            combinations.AddRange(selectMany.ToList());
                        }
                        
                        // for (var x = 0; x < numGaps; x++)
                        // {
                        //     var l = new List<string>();
                        //     for (var i = 1; i <= numDots - numGaps; i++)
                        //     {
                        //         l.Add(string.Join("", Enumerable.Range(1, i).Select(_ => ".")));
                        //     }
                        //
                        //     combinations.Add(l);
                        // }
                    }
                }

                foreach (var combination in combinations)
                {
                    var builder = new StringBuilder();
                    for (var i = 0; i < places.Length; i++)
                    {
                        builder.Append(string.Join("", Enumerable.Range(0, places[i]).Select(_ => "#")));
                        if (i != places.Length - 1)
                        {
                            builder.Append(combination[i]);
                        }
                    }

                    options.Add(builder.ToString());
                }
            }
        }

        var result = new HashSet<string>();
        foreach (var opt in options)
        {
            var diff = lineLength - opt.Length;
            if (diff == 0)
            {
                result.Add(opt);
            }

            for (var y = 0; y <= diff; y++)
            {
                result.Add(
                    string.Join("", Enumerable.Range(0, y).Select(_ => "."))
                    + opt
                    + string.Join("", Enumerable.Range(0, diff - y).Select(_ => ".")));
            }
        }

        return result;
    }

    // ?.??##???.?#??#??.# 1,4,1,5,1
    private static List<List<int>> GetLengthGroups(int numGaps, int[] lengths)
    {
        if (numGaps == 1)
        {
            return lengths.Select(x => new List<int> { x }).ToList();
        }

        var result = new List<List<int>>();

        for (var x = 0; x < lengths.Length; x++)
        {
            // 0
            var starts = Enumerable.Range(0, numGaps).Select(_ => new List<int> {lengths[x]}.ToList());
            // 1,1,1,1
            foreach (var st in starts)
            {
                var array = lengths[..x].Concat(lengths[x..]).ToArray();
                result.AddRange(GetLengthGroups(numGaps - 1, array).Select(x => st.Concat(x).ToList()));
            }
        }
        
        return result;
    }
    
}