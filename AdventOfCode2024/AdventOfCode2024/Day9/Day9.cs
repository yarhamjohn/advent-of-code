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
        return blockMap
            .Select((fileId, blockIdx) => (blockIdx, fileId))
            .Where(x => x.fileId is not null)
            .Sum(x => x.blockIdx * (long)x.fileId!);
    }

    private static void ReorganiseBlockMap(List<int?> blockMap)
    {
        for (var blockIdx = 0; blockIdx < blockMap.Count; blockIdx++)
        {
            if (blockMap[blockIdx] != null || blockMap[blockIdx..].All(x => x is null))
            {
                continue;
            }
            
            var idxToMove = blockMap.FindLastIndex(idx => idx is not null);
            blockMap[blockIdx] = blockMap[idxToMove];
            blockMap[idxToMove] = null;
        }
    }

    private static void ReorganiseBlockMapFiles(List<int?> blockMap)
    {
        var files = blockMap
            .Where(fileId => fileId is not null)
            .GroupBy(fileId => (int)fileId!)
            .ToDictionary(x => x.Key, x => x.Count())
            .OrderByDescending(x => x.Key);

        foreach (var file in files)
        {
            var currentIdx = blockMap.IndexOf(file.Key);

            var nullIndexes = blockMap
                .Select((fileId, blockIdx) => (fileId, blockIdx))
                .Where(x => x.fileId is null && x.blockIdx < currentIdx)
                .Select(x => x.blockIdx);

            foreach (var nullIdx in nullIndexes)
            {
                if (nullIdx + file.Value >= blockMap.Count)
                {
                    break;
                }

                var blockCanMove = blockMap[nullIdx..(nullIdx + file.Value)].All(fileId => fileId is null);
                
                if (blockCanMove)
                {
                    var rangeToInsert = Enumerable.Range(0, file.Value).ToArray();

                    blockMap.RemoveRange(currentIdx, file.Value);
                    blockMap.InsertRange(currentIdx, rangeToInsert.Select(_ => (int?)null));

                    blockMap.RemoveRange(nullIdx, file.Value);
                    blockMap.InsertRange(nullIdx, rangeToInsert.Select(_ => (int?) file.Key));

                    break;
                }
            }
        }
    }

    private static List<int?> GetBlockMap(int[] input)
    {
        var blockMap = new List<int?>();

        var fileId = 0;

        for (var blockIdx = 0; blockIdx < input.Length; blockIdx++)
        {
            if (blockIdx % 2 == 0)
            {
                for (var i = 0; i < input[blockIdx]; i++)
                {
                    blockMap.Add(fileId);
                }

                fileId++;
            }
            else
            {
                for (var i = 0; i < input[blockIdx]; i++)
                {
                    blockMap.Add(null);
                }
            }
        }

        return blockMap;
    }
}