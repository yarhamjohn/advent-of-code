using System.Text;

namespace AdventOfCode2022.Day25;

public static class Day25
{
    public static string CalculateSnafuNumber(string[] input)
    {
        var fuel = input.Sum(CalculateDecimal);
        Console.WriteLine(fuel);
        return CalculateSnafu(fuel);
    }

    public static string CalculateSnafu(long num)
    {
        var biggestPower = GetBiggestConsumablePower(num);

        var temp = GetNaiveSnafu(num, biggestPower);

        return FixSnafu(temp);
    }

    private static string FixSnafu(string temp)
    {
        var workingString = string.Join("", temp.Reverse());
        
        while (!workingString.All(x => new [] { '=', '-', '0', '1', '2'}.Contains(x)))
        {
            for (var i = workingString.Length - 1; i >= 0; i--)
            {
                var tooBig = workingString[i] != '=' && workingString[i] != '-' && workingString[i] - '0' > 2;
                
                if (i == workingString.Length - 1 && tooBig)
                {
                    workingString = workingString[..^1] + GetNewLowerValue(i, workingString) + "1";
                    break;
                }

                if (tooBig)
                {
                    workingString = workingString[..i] + GetNewLowerValue(i, workingString) + GetNewUpperValue(i, workingString) + workingString[(i + 2)..];
                    break;
                }
            }
        }

        return string.Join("", workingString.Reverse());
    }

    private static string GetNewUpperValue(int i, string workingString)
    {
        var val = workingString[i + 1];
        return val switch
        {
            '-' => "0",
            '=' => "-",
            _ => (val - '0' + 1).ToString()
        };
    }

    private static string GetNewLowerValue(int i, string workingString)
    {
        var valueProvided = (long) Math.Pow(5, i) * (workingString[i] - '0');

        var extraValue = (long) Math.Pow(5, i + 1) - valueProvided;

        return extraValue / (long) Math.Pow(5, i) == 1 ? "-" : "=";
    }

    private static string GetNaiveSnafu(long num, int biggestPower)
    {
        var temp = new StringBuilder();
        for (var i = biggestPower; i >= 0; i--)
        {
            var total = num / (long) Math.Pow(5, i);
            temp.Append(total);
            
            num -= total * (long) Math.Pow(5, i);
        }

        return temp.ToString();
    }

    private static int GetBiggestConsumablePower(long num)
    {
        var biggestPower = 0;
        while (true)
        {
            if (num < Math.Pow(5, biggestPower))
            {
                biggestPower--;
                break;
            }

            biggestPower++;
        }

        return biggestPower;
    }

    private static long CalculateDecimal(string snafu)
    {
        var powers = CalculatePowers(snafu);

        return snafu
            .Select((element, i) =>
                element switch
                {
                    '=' => powers[i] * -2,
                    '-' => powers[i] * -1,
                    _ => powers[i] * (element - '0')
                })
            .Sum();
    }

    private static long[] CalculatePowers(string snafu)
    {
        return snafu.Select((_, i) => (long) Math.Pow(5, i)).Reverse().ToArray();
    }
}