namespace AdventOfCode2023.Day1;

public static class Day1
{
    private static readonly Dictionary<string, string> Map = new()
    {
        { "1", "1" }, { "2", "2" }, { "3", "3" },
        { "4", "4" }, { "5", "5" }, { "6", "6" },
        { "7", "7" }, { "8", "8" }, { "9", "9" },
        { "one", "1" }, { "two", "2" }, { "three", "3" },
        { "four", "4" }, { "five", "5" }, { "six", "6" },
        { "seven", "7" }, { "eight", "8" }, { "nine", "9" }
    };
    
    public static int GetCalibrationSum(IEnumerable<string> input)
    {
        return input.Sum(line =>
        {
            var indexMap = new Dictionary<int, string>();
            foreach (var (key, value) in Map)
            {
                var firstIdx = line.IndexOf(key, StringComparison.Ordinal);
                if (firstIdx != -1)
                {
                    indexMap[firstIdx] = value;
                }

                var lastIdx = line.LastIndexOf(key, StringComparison.Ordinal);
                if (firstIdx != lastIdx && lastIdx != -1)
                {
                    indexMap[lastIdx] = value;
                }
            }

            return int.Parse(indexMap[indexMap.Keys.Min()] + indexMap[indexMap.Keys.Max()]);
        });
    }
}