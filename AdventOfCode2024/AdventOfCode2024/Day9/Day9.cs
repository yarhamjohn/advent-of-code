namespace AdventOfCode2024.Day9;

public static class Day9
{
    public static long Part1(string input)
    {
        var blockMap = GetBlockMap(ParseInput(input));

        ReorganiseBlockMap(blockMap);
        
        return GetChecksum(blockMap);
    }

    public static long Part2(string input)
    {
        var blockMap = GetBlockMap(ParseInput(input));

        ReorganiseBlockMapFiles(blockMap);
        
        return GetChecksum(blockMap);
    }

    private static int[] ParseInput(string input)
    {
        return input.ToCharArray().Select(x => x - '0').ToArray();
    }

    private static long GetChecksum(List<int?> blockMap)
    {
        return blockMap.Where(x => x is not null).Select((x, i) => i * (long)x!).Sum();
    }

    private static void ReorganiseBlockMap(List<int?> blockMap)
    {
        for (var i = 0; i < blockMap.Count; i++)
        {
            if (blockMap[i] != null)
            {
                continue;
            }
            
            var idxToMove = blockMap.FindLastIndex(x => x is not null);
            blockMap[i] = blockMap[idxToMove];
            blockMap[idxToMove] = null;
        }
    }

    private static void ReorganiseBlockMapFiles(List<int?> blockMap)
    {
        // Work out length required to fit file (moving backwards)
        // Find first space it fits (move forwards) and move it
    }

    private static List<int?> GetBlockMap(int[] input)
    {
        var blockMap = new List<int?>();

        var counter = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (i % 2 == 0)
            {
                for (var j = 0; j < input[i]; j++)
                {
                    blockMap.Add(counter);
                }

                counter++;
            }
            else
            {
                for (var j = 0; j < input[i]; j++)
                {
                    blockMap.Add(null);
                }
            }
        }

        return blockMap;
    }
}