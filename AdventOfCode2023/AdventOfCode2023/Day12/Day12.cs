using System.Text;

namespace AdventOfCode2023.Day12;

public static class Day12
{
    public static long SumDamageCombinations(string[] input)
    {
        var lines = input.Select(x => x.Split(" ")).Select(y => (y[0], y[1].Split(",").Select(int.Parse).ToArray()));

        var totalCombinations = 0;
        foreach (var (line, places) in lines)
        {
            var masks = GetMasks(line.Length, places);

            Console.WriteLine(line);
            
            foreach (var m in masks)
            {
                Console.WriteLine(m);
            }

            Console.WriteLine();
            
            totalCombinations += CountMatches(line, masks);
        }
        
        return totalCombinations;
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
        var result = new HashSet<string>();
        if (places.Length == 1)
        {
            for (var i = 0; i <= lineLength - places.Single(); i++)
            {
                var builder = new StringBuilder();
                for (var x = 0; x < i; x++)
                {
                    builder.Append('.');
                }
        
                builder.Append(string.Join("", Enumerable.Range(0, places.Single()).Select(_ => "#")));
        
                for (var y = i + places.Single(); y < lineLength; y++)
                {
                    builder.Append('.');
                }
        
                result.Add(builder.ToString());
            }
        }
        else
        {
            var options = new List<string>();
            for (var numDots = 1; numDots <= lineLength - places.Count(); numDots++)
            {
                var builder = new StringBuilder();
                for (var i = 0; i < places.Length; i++)
                {
                    builder.Append(string.Join("", Enumerable.Range(0, places[i]).Select(_ => "#")));
                    if (i != places.Length - 1)
                    {
                        builder.Append(string.Join("", Enumerable.Range(0, numDots).Select(_ => ".")));
                    }
                }
                
                options.Add(builder.ToString());
            }

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
        }

        return result;
    }
}