namespace AdventOfCode2023.Day9;

public static class Day9
{
    public static long SumExtrapolatedValues(string[] input)
    {
        return SumNextValues(input.Select(x => x.Split(" ").Select(int.Parse).ToList()));
    }
    
    public static long SumExtrapolatedValuesReverse(string[] input)
    {
        return SumNextValues(input.Select(x => x.Split(" ").Select(int.Parse).Reverse().ToList()));
    }

    private static long SumNextValues(IEnumerable<List<int>> sequences)
    {
        var result = 0;
        foreach (var sequence in sequences)
        {
            var nextValue = 0;
            var allSequences = new List<List<int>> { sequence };
            while (allSequences.Last().Any(x => x != 0))
            {
                allSequences.Add(GetNextSequence(allSequences.Last()));
            }

            for (var i = allSequences.Count - 1; i > 0; i--)
            {
                nextValue = allSequences[i].Last() + allSequences[i - 1].Last();
                allSequences[i - 1].Add(nextValue);
            }

            result += nextValue;
        }
        
        return result;
    }

    private static List<int> GetNextSequence(IList<int> sequence)
    {
        var result = new List<int>();
        for (var i = 0; i < sequence.Count - 1; i++)
        {
            result.Add(sequence[i + 1] - sequence[i]);
        }

        return result.ToList();
    }
}