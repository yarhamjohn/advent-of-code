namespace AdventOfCode2016.Day9;

public static class Day9
{
    public static long GetAdvancedDecompressedLength(string input)
    {
        var counter = 0L;

        for (var i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case >= 'A' and <= 'Z':
                    counter++;
                    break;
                case '(':
                {
                    var closeIndex = input[i..].IndexOf(')') + i;
                    var segments = input[(i + 1)..closeIndex].Split('x');
                    var (numChars, times) = (Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1]));

                    var stringToRepeat = input[(closeIndex + 1)..(closeIndex + numChars + 1)];
                    if (stringToRepeat.Contains('('))
                    {
                        var additionalLength = GetAdvancedDecompressedLength(stringToRepeat);
                        counter += additionalLength * times;
                        i = closeIndex + stringToRepeat.Length;
                    }
                    else
                    {
                        counter += numChars * times;
                        i = closeIndex + numChars;
                    }

                    break;
                }
            }
        }
        
        return counter;
    }
    
    public static long GetDecompressedLength(string input)
    {
        var counter = 0;

        for (var i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case >= 'A' and <= 'Z':
                    counter++;
                    break;
                case '(':
                {
                    var closeIndex = input[i..].IndexOf(')') + i;
                    var segments = input[(i + 1)..closeIndex].Split('x');
                    var (numChars, times) = (Convert.ToInt32(segments[0]), Convert.ToInt32(segments[1]));

                    counter += numChars * times; 
                    i = closeIndex + numChars;
                    break;
                }
            }
        }
        
        return counter;
    }
}