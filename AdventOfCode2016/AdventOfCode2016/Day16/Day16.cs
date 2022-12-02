using System.Text;

namespace AdventOfCode2016.Day16;

public static class Day16
{
    public static string GetChecksum(string input, int length)
    {
        var data = CalculateData(input, length);
        var checksum = CalculateChecksum(data);
        while (checksum.Length % 2 == 0)
        {
            checksum = CalculateChecksum(checksum);
        }

        return checksum;
    }

    private static string CalculateData(string input, int length)
    {
        while (true)
        {
            if (input.Length >= length)
            {
                return input[..length];
            }
            
            input = input + "0" + GetReverse(input);
        }
    }

    private static string GetReverse(string input)
        => string.Join("", input.Reverse().Select(x => x == '1' ? '0' : '1'));

    private static string CalculateChecksum(string data)
    {
        var chunks = data.Chunk(2);
        var next = new StringBuilder();

        foreach (var chunk in chunks)
        {
            next.Append(chunk[0] == chunk[1] ? "1" : "0");
        }

        return next.ToString();
    }
}