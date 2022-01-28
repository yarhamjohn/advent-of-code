namespace AdventOfCode2021.Day3;

public static class Day3
{
    public static int CalculatePowerConsumption(IEnumerable<string> input)
    {
        var length = input.First().Length;

        var gamma = "";
        var epsilon = "";
        for (var i = 0; i < length; i++)
        {
            var countOfOnes = input.Count(x => x[i] == '1');
            var halfInputLength = input.Count() / 2;
            gamma += countOfOnes > halfInputLength ? "1" : "0";
            epsilon += countOfOnes < halfInputLength ? "1" : "0";
        }
        
        return Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
    }
    
    public static int CalculateLifeSupportRating(IEnumerable<string> input)
    {
        var oxygenGeneratorRating = GetOxygenGeneratorRating(input);
        var co2ScrubberRating = GetCO2ScrubberRating(input);

        return oxygenGeneratorRating * co2ScrubberRating;
    }

    private static int GetCO2ScrubberRating(IEnumerable<string> input, int pos = 0)
    {
        if (input.Count() == 1)
        {
            return Convert.ToInt32(input.Single(), 2);
        }
        
        var numOnes = input.Count(x => x[pos] == '1');
        var numZeros = input.Count(x => x[pos] == '0');

        var next = numZeros <= numOnes 
            ? input.Where(x => x[pos] == '0').ToList() 
            : input.Where(x => x[pos] == '1').ToList();

        return GetCO2ScrubberRating(next, ++pos);
    }

    private static int GetOxygenGeneratorRating(IEnumerable<string> input, int pos = 0)
    {
        if (input.Count() == 1)
        {
            return Convert.ToInt32(input.Single(), 2);
        }
        
        var numOnes = input.Count(x => x[pos] == '1');
        var numZeros = input.Count(x => x[pos] == '0');

        var next = numOnes >= numZeros 
            ? input.Where(x => x[pos] == '1').ToList() 
            : input.Where(x => x[pos] == '0').ToList();

        return GetOxygenGeneratorRating(next, ++pos);
    }
}
