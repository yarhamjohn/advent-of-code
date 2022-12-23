namespace AdventOfCode2022.Day20;

public static class Day20
{
    public static long CalculateGroveCoordinates(string[] input)
    {
        var original = input.Select((x, i) => (id: i, num: Convert.ToInt32(x))).ToList();
        var mutant = input.Select((x, i) => (id: i, num: Convert.ToInt32(x))).ToList();

        for (var i = 0; i < original.Count; i++)
        {
            var currentIndex = Array.IndexOf(mutant.ToArray(), (original[i].id, original[i].num));
            var targetIndex = GetTargetIndex(currentIndex, original, i, mutant);
            
            mutant.RemoveAt(currentIndex);
            mutant.Insert(targetIndex, (original[i].id, original[i].num));
        }

        return SumCoordinates(mutant);
    }

    private static long SumCoordinates(List<(int id, int num)> mutant)
    {
        var zeroIndex = Array.IndexOf(mutant.Select(x => x.num).ToArray(), 0);
        
        return mutant[(zeroIndex + 1000) % mutant.Count].num +
               mutant[(zeroIndex + 2000) % mutant.Count].num +
               mutant[(zeroIndex + 3000) % mutant.Count].num;
    }

    private static int GetTargetIndex(int currentIndex, List<(int id, int num)> original, int i, List<(int id, int num)> mutant)
    {
        // Get target index regardless of list length
        var targetIndex = currentIndex + original[i].num;

        // Target index is within list bounds
        if (targetIndex >= 0 && targetIndex < mutant.Count)
        {
            return targetIndex;
        }

        // Target index is higher than list length
        if (targetIndex >= mutant.Count)
        {
            return targetIndex % (mutant.Count - 1);
        }

        // Target index is negative
        return mutant.Count - 1 - Math.Abs(targetIndex) % (mutant.Count - 1);
    }
}