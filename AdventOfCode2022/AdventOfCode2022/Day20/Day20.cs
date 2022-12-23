namespace AdventOfCode2022.Day20;

public static class Day20
{
    public static long CalculateGroveCoordinates(string[] input)
    {
        var original = input.Select((x, i) => (id: i, num: Convert.ToInt64(x))).ToList();
        var mutant = original.ToList();

        for (var i = 0; i < original.Count; i++)
        {
            var currentIndex = Array.IndexOf(mutant.ToArray(), (original[i].id, original[i].num));
            var targetIndex = GetTargetIndex(currentIndex, original, i, mutant);
            
            mutant.RemoveAt(currentIndex);
            mutant.Insert(targetIndex, (original[i].id, original[i].num));
        }

        return SumCoordinates(mutant);
    }
    
    public static long CalculateGroveCoordinatesHuge(string[] input)
    {
        var original = input.Select((x, i) => (id: i, num: Convert.ToInt64(x) * 811589153)).ToList();
        var mutant = original.ToList();

        for (var n = 0; n < 10; n++)
        {
            for (var i = 0; i < original.Count; i++)
            {
                var currentIndex = Array.IndexOf(mutant.ToArray(), (original[i].id, original[i].num));
                var targetIndex = GetTargetIndex(currentIndex, original, i, mutant);

                mutant.RemoveAt(currentIndex);
                mutant.Insert(targetIndex, (original[i].id, original[i].num));
            }
        }

        return SumCoordinates(mutant);
    }

    private static long SumCoordinates(List<(int id, long num)> mutant)
    {
        var zeroIndex = Array.IndexOf(mutant.Select(x => x.num).ToArray(), 0);
        
        return mutant[(zeroIndex + 1000) % mutant.Count].num +
               mutant[(zeroIndex + 2000) % mutant.Count].num +
               mutant[(zeroIndex + 3000) % mutant.Count].num;
    }

    private static int GetTargetIndex(int currentIndex, List<(int id, long num)> original, int i, List<(int id, long num)> mutant)
    {
        // Get target index regardless of list length
        var targetIndex = currentIndex + original[i].num;

        // Target index is within list bounds
        if (targetIndex >= 0 && targetIndex < mutant.Count)
        {
            return (int) targetIndex;
        }

        // Target index is higher than list length
        if (targetIndex >= mutant.Count)
        {
            return (int) (targetIndex % (mutant.Count - 1));
        }

        // Target index is negative
        return mutant.Count - 1 - (int) (Math.Abs(targetIndex) % (mutant.Count - 1));
    }
}