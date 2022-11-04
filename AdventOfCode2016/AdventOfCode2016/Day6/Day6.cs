namespace AdventOfCode2016.Day6;

public static class Day6
{
    public static string GetMessage(string[] input)
    {
        var result = new List<char>();
        
        var chunks = input.Select(x => x.ToCharArray()).ToArray();
        for (var i = 0; i < input.First().Length; i++)
        {
            result.Add(GetOrderedGroups(chunks, i).First().Key);
        }
        
        return string.Join("", result);
    }

    public static string GetMessageModified(string[] input)
    {
        var result = new List<char>();
        
        var chunks = input.Select(x => x.ToCharArray()).ToArray();
        for (var i = 0; i < input.First().Length; i++)
        {
            result.Add(GetOrderedGroups(chunks, i).Last().Key);
        }
        
        return string.Join("", result);
    }
    
    private static IOrderedEnumerable<IGrouping<char, char>> GetOrderedGroups(char[][] chunks, int i)
    {
        return chunks.Select(x => x[i]).GroupBy(x => x).OrderByDescending(x => x.Count());
    }
}