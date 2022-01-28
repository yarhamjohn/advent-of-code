namespace AdventOfCode.Day7;

public static class Day7
{
    public static int CalculateFuelRequired(string input)
    {
        var positions = input.Split(",").Select(x => Convert.ToInt32(x)).ToArray();

        var fuelRequired = positions.Sum();
        for (var i = positions.Min(); i <= positions.Max(); i++)
        {
            var fuel = positions.Sum(p => Math.Abs(p - i));
            if (fuel < fuelRequired)
            {
                fuelRequired = fuel;
            }
        }
        
        return fuelRequired;
    }
    
    public static int CalculateFuelRequiredIncludingIncrease(string input)
    {
        var positions = input.Split(",").Select(x => Convert.ToInt32(x)).ToArray();

        var fuelRequired = positions.Sum() * positions.Length;
        for (var i = positions.Min(); i <= positions.Max(); i++)
        {
            var fuel = positions.Sum(p => CalculateFuelRequired(p, i));
            if (fuel < fuelRequired)
            {
                fuelRequired = fuel;
            }
        }
        
        return fuelRequired;
    }

    private static int CalculateFuelRequired(int p, int i)
    {
        var movementRequired = Math.Abs(p - i);

        /* S = n(a + l)/2
           S = sum of the consecutive integers
           n = number of integers
           a = first term
           l = last term
        */
        return (movementRequired + 1) * movementRequired / 2;
    }
}

