namespace AdventOfCode2015.Day17;

public static class Day17
{
    public static long GetContainerCombinations(IEnumerable<string> input, int litres)
    {
        var containers = input.Select(x => Convert.ToInt32(x)).ToArray();

        /*
         1 << length is the same as 2 ** length, so given 5 containers we get an array of ints from 0 -> 31
         This works since each set of containers can be represented in binary where one bits indicate that
         the corresponding container is used in that set:
         00000 - no containers used
         00001 - container one used
         00010 - container two used
         00011 - containers one and two used
         01010 - containers four and two used
         11111 - all containers used  
        */
        var numContainerCombinations = Enumerable.Range(0, 1 << containers.Length);

        // For each combination, select the containers which correspond to those 'used' based on
        // the bitmap above. So for combination one, no containers are used, whilst for combination
        // two, only container 1 is used.
        var containerSets = numContainerCombinations
            .Select(index => containers.Where((_, i) => (index & (i << 1)) != 0));

        // Filter the container sets to only count those that match the total number of litres.
        return containerSets
            .Count(x => x.Sum() == litres);
    }
}