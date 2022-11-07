namespace AdventOfCode2016.Day9;

public static class Day9
{
    public static long GetDecompressedLength(string input)
    {
        var counter = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] >= 'A' && input[i] <= 'Z')
            {
                counter++;
            }
            else if (input[i] == '(')
            {
                var closeIndex = input[i..].IndexOf(')') + i;
                var segments = input[(i + 1)..closeIndex].Split('x');
                var (numChars, times) = (Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1]));

                counter += numChars * times; 
                i = closeIndex + numChars;
            }
        }
        
        return counter;
    }
}