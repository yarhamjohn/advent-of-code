namespace AdventOfCode2022.Day25;

public static class Day25
{
    public static string CalculateSnafuNumber(string[] input)
    {
        var fuel = input.Sum(CalculateDecimal);
        Console.WriteLine(fuel);
        return CalculateSnafu(fuel);
    }

    public static string CalculateSnafu(int sum)
    {
        if (sum < 3)
        {
            return sum.ToString();
        }

        var maxValuesByIndex = new[] { 2, 12, 62, 312, 937, 4062 };

        var idx = GetMaxIndex(sum, maxValuesByIndex);

        var a = Math.Pow(5, idx) - maxValuesByIndex[idx - 1];
        var quantity = sum > maxValuesByIndex[idx - 1] &&
                       sum < Math.Pow(5, idx) + a ? 1 : 2;

        var z = (int) Math.Pow(5, idx) - sum;
        var z1 = z / (int) Math.Pow(5, idx - 1);

        var sixHundredAndTwentyFives = "";
        if (idx == 5)
        {
            sixHundredAndTwentyFives = z1 == 2
                ? "="
                : z1 == 1
                    ? "-"
                    : z1.ToString();
            
            idx--;
        }
        
        var oneHundredAndTwentyFives = "";
        if (idx == 4)
        {
            oneHundredAndTwentyFives = z1 == 2
                ? "="
                : z1 == 1
                    ? "-"
                    : z1.ToString();
            
            idx--;
        }
        
        var twentyFives = "";

        if (idx == 3)
        {
            twentyFives = z1 == 2
                ? "="
                : z1 == 1
                    ? "-"
                    : z1.ToString();
            
            idx--;
        }

        var fives = "";
        if (idx == 2)
        {
            fives = z1 == 2
                ? "="
                : z1 == 1
                    ? "-"
                    : z1.ToString();
        }

        if (idx == 1)
        {
            fives = sum - 5 <= 2
                ? "1"
                : "2";
        }

        var ones = GetOnes(sum);

        return (idx > 1 ? quantity : "") + sixHundredAndTwentyFives + oneHundredAndTwentyFives + twentyFives +  fives + ones;
    }

    private static string GetOnes(int sum)
    {
        var y = sum % 5;
        return y switch
        {
            3 => "=",
            4 => "-",
            _ => y.ToString()
        };
    }

    private static int GetMaxIndex(int sum, int[] maxValuesByIndex)
    {
        var idx = 0;
        while (true)
        {
            if (sum <= maxValuesByIndex[idx])
            {
                break;
            }

            idx++;
        }

        return idx;
    }

    private static (int biggestUsedPower, int quantity) GetBiggestUsedPowerAndQuantity(int sum)
    {
        var maxValuesByIndex = new[] { 2, 12, 62, 312, 937, 4062 };

        var idx = 0;
        while (true)
        {
            if (sum <= maxValuesByIndex[idx])
            {
                break;
            }

            idx++;
        }
        
        var quantity = sum >= Math.Pow(5, idx) - maxValuesByIndex[idx - 1] &&
                       sum <= Math.Pow(5, idx) + maxValuesByIndex[idx - 1] ? 1 : 2;

        return (idx, quantity);
    }

    private static int CalculateDecimal(string snafu)
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

    private static int[] CalculatePowers(string snafu)
    {
        return snafu.Select((_, i) => (int)Math.Pow(5, i)).Reverse().ToArray();
    }
}