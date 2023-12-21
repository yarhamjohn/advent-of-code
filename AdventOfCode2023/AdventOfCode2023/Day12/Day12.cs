using System.Collections;
using System.Text;

namespace AdventOfCode2023.Day12;

public static class Day12
{
    public static long test(string[] input)
    {
        var lines = input.Select(x => x.Split(" ")).Select(y => (y[0], y[1].Split(",").Select(int.Parse).ToArray()));

        var num = 0;
        foreach (var line in lines)
        {
            seen = new();
            // Console.WriteLine("==================== " + seen.Count);
            // Console.WriteLine(line.Item1 + " " + string.Join(",", line.Item2));
            Recurse(line.Item1, line.Item2, 0);
            // Console.WriteLine("==================== " + seen.Count);
            num += seen.Count;
        }

        return num;
    }

    // Need shortcuts. It should be possible to identify that we have reached
    // a position in the line, where we know how many matches there are given the
    // remaining springs
    public static long testUnfolded(string[] input)
    {
        var lines = input.Select(x => x.Split(" ")).Select(y => (y[0], y[1].Split(",").Select(int.Parse).ToArray()));
        var unfoldedLines = Unfold(lines);

        var num = 0L;
        var count = 0;
        foreach (var line in unfoldedLines)
        {
            count++;
            seen = new();
            // Console.WriteLine("==================== " + seen.Count);
            // Console.WriteLine(line.Item1 + " " + string.Join(",", line.Item2));
            Recurse(line.Item1, line.Item2, 0);
            Console.WriteLine(count + ": " + line.Item1 + " - " + seen.Count);
            num += seen.Count;
        }

        return num;
    }
    
    private static HashSet<string> seen;

    private static void Recurse(string line, int[] springs, int position)
    {
        if (position == line.Length)
        {
            // Console.WriteLine("++++++++++++ " + line);
            seen.Add(line);
            return;
        }
        
        var possibleLines = new List<string>();
        if (line[position] is '.' or '#')
        {
            possibleLines.Add(line);
        }
        else
        {
            possibleLines.Add(line[..position] + "." + line[(position + 1)..]);
            possibleLines.Add(line[..position] + "#" + line[(position + 1)..]);
        }

        foreach (var l in possibleLines)
        {
            if (IsValid(l, springs, position))
            {
                // Console.WriteLine(l + " - TRUE - " + position);
                Recurse(l, springs, position + 1);
            }
            else
            {
                // Console.WriteLine(l + " - FALSE - " + position);
            }
        }
    }

    private static bool IsValid(string line, int[] springs, int position)
    {
        var (seenSprings, inPlay) = GetSeenSprings(line, position);

        // if more springs were already seen than we have, its invalid
        if (seenSprings.Count > springs.Length)
        {
            return false;
        }

        // If the remaining springs can't fit the rest of the line, its not valid
        if (!CanFitRestIn(line, springs, position, seenSprings, inPlay))
        {
            return false;
        }

        // If there any already seen springs which have an incorrect length, its not valid
        return !seenSprings.Where((t, x) => t > springs[x]).Any();
    }

    private static bool CanFitRestIn(string line, int[] springs, int position, List<int> seenSprings, bool inPlay)
    {
        var totalSpringLengthLeft = springs[seenSprings.Count..].Sum();
        var numSpringsLeft = springs[seenSprings.Count..].Length;

        int requiredSpringSpace;
        int requiredGapSpace;
        if (inPlay)
        {
            // The current spring was complete
            if (seenSprings[^1] == springs[seenSprings.Count - 1])
            {
                requiredSpringSpace = totalSpringLengthLeft;
                requiredGapSpace = numSpringsLeft;
            }
            else
            {
                // The current spring was incomplete
                var remainsOfCurrentSpring = springs[seenSprings.Count - 1] - seenSprings[^1];
                requiredSpringSpace = remainsOfCurrentSpring + totalSpringLengthLeft;
                requiredGapSpace = numSpringsLeft;
            }
        }
        else
        {
            // The last spring was completed but smaller than required, so it is invalid
            if (seenSprings.Count != 0 && seenSprings[^1] < springs[seenSprings.Count - 1])
            {
                return false;
            }
            
            requiredSpringSpace = totalSpringLengthLeft;
            requiredGapSpace = numSpringsLeft - 1;
        }

        var availableSpringSpace = line[(position + 1)..].Count(x => x != '.');
        var availableSpace = line[(position + 1)..].Length;
        return requiredSpringSpace <= availableSpringSpace && requiredSpringSpace + requiredGapSpace <= availableSpace;
    }

    private static (List<int> seenSprings, bool inPlay) GetSeenSprings(string line, int position)
    {
        var seenSprings = new List<int>();
        var inPlay = false;
        
        for (var i = 0; i <= position; i++)
        {
            switch (line[i])
            {
                case '#' when inPlay:
                    seenSprings[^1]++;
                    break;
                case '#':
                    seenSprings.Add(1);
                    inPlay = true;
                    break;
                default:
                    inPlay = false;
                    break;
            }
        }

        return (seenSprings, inPlay);
    }

    public static long SumDamageCombinations(string[] input)
    {
        var lines = input.Select(x => x.Split(" ")).Select(y => (y[0], y[1].Split(",").Select(int.Parse).ToArray()));

        var combinations = 0;
        foreach (var line in lines)
        {
            found = new List<string>(); 
            GetCombinations("", line.Item1, line.Item2);
            
            // Console.WriteLine("================");
            Console.WriteLine(line.Item1 + " - " + found.Count);
            // Console.WriteLine(string.Join(",", found));
            // Console.WriteLine("================");

            combinations += found.ToHashSet().Count;
        }

        Console.WriteLine("===================== " + combinations);
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

    private static List<string> found;

    private static void GetCombinations(string current, string remainder, int[] springs)
    {
        // Console.WriteLine("-----------");
        // Console.WriteLine(current);
        // Console.WriteLine(string.Join(",", found));
        // Console.WriteLine(remainder);
        // Console.WriteLine(string.Join(",", springs));
        // Console.WriteLine("-----------");

        // There are no more things to fit
        if (springs.Length == 0)
        {
            // If there are springs remaining, its invalid
            if (remainder.Any(x => x == '#'))
            {
                return;
            }
            
            // Add the remainder and include in found
            found.Add(current + string.Join("", Enumerable.Range(0, remainder.Length).Select(_ => ".")));

            // return
            return;
        }

        // find first index where it fits
        for (var i = 0; i < remainder.Length; i++)
        {
            var firstSpring = springs.First();

            // skip known dots
            if (remainder[i] == '.')
            {
                // break if we are part way through the spring
                if (current.Length >= firstSpring && current[^1] == '#' && current[^firstSpring..].Any(x => x != '#'))
                {
                    break;
                }
                
                current += remainder[i].ToString();
                continue;
            }

            // can't fit the rest in
            if (i + springs.Sum() + (springs.Length - 1) > remainder.Length)
            {
                break;
            }
            
            // if in middle of spring we have already evaluated, finish it
            if (i > 0 && (remainder[i - 1] == '#' || current[^1] == '#'))
            {
                if (remainder[i] == '?')
                {
                    // spring is complete so go to the next
                    if (current.Length >= firstSpring && current[^firstSpring..].All(x => x == '#'))
                    {
                        current += ".";
                        springs = springs[1..];
                        firstSpring = springs.First();
                    }
                    else
                    {
                        current += "#";
                        continue;
                    }
                }
            
                if (remainder[i] == '#')
                {
                    current += '#';
                    continue;
                }
            }

            // check it can fit spring
            if (remainder[i..(i + firstSpring)].All(x => x != '.'))
            {
                // check there is no spring immediately after its end or before its start
                if (firstSpring == remainder.Length - i || remainder[i + firstSpring] != '#')
                {
                    current += string.Join("", Enumerable.Range(0, firstSpring).Select(_ => "#"));
                    if (firstSpring < remainder.Length - i)
                    {
                        current += ".";
                        GetCombinations(current, remainder[(i + firstSpring + 1)..], springs[1..]);
                        current = current[..^(firstSpring + 1)];
                    }
                    else
                    {
                        GetCombinations(current, remainder[(i + firstSpring)..], springs[1..]);
                        current = current[..^firstSpring];
                    }
                    
                    // if the position was unknown, we now assign it to be '.' and go on
                    if (remainder[i] == '?')
                    {
                        current += ".";
                    }

                    // if it was the last spring and it was fixed don't carry on
                    if (remainder[i] == '#' && springs.Length == 1)
                    {
                        break;
                    }
                }
                else
                {
                    // this is required to be included but couldn't be
                    if (remainder[i] == '#')
                    {
                        break;
                    }
                    
                    // it must have been unknown but now can't be a spring
                    current += ".";
                }
            }
            else
            {
                // there is a spring here but we can't use it
                var firstIndexOfASpring = remainder[i..(i + firstSpring)].IndexOf('#');
                var lastIndexOfASpace = remainder[i..(i + firstSpring)].LastIndexOf('.');
                if (firstIndexOfASpring != -1 && lastIndexOfASpace != -1 && firstIndexOfASpring < lastIndexOfASpace)
                {
                    break;
                }
                
                // it must have been unknown but now can't be a spring
                current += ".";
            }
        }
    }

    public static long SumDamageCombinationsUnfolded(string[] input)
    {
        var lines = input.Select(x => x.Split(" ")).Select(y => (y[0], y[1].Split(",").Select(int.Parse).ToArray()));
        var unfoldedLines = Unfold(lines);

        // var combinations = 0;
        // foreach (var line in unfoldedLines)
        // {
        //     found = new List<string>(); 
        //     GetCombinations("", line.Item1, line.Item2);
        //     
        //     // Console.WriteLine("================");
        //     Console.WriteLine(line.Item1 + " - " + found.Count);
        //     // Console.WriteLine(string.Join(",", found));
        //     // Console.WriteLine("================");
        //
        //     combinations += found.ToHashSet().Count;
        // }
        //
        // Console.WriteLine("===================== " + combinations);
        // return combinations;
        
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