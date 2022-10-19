namespace AdventOfCode2015.Day20;

public static class Day20
{
    public static long GetLowestHouseNumber(int input)
    {
        // Since each elf delivers 10 * its number, if the target input
        // is 1000 then elf # 100 would deliver all 1000 so there is no
        // need to check further
        var maxPossibleHouseNumber = input / 10;
        
        var housesInScope = Enumerable.Range(1, maxPossibleHouseNumber);

        foreach (var house in housesInScope)
        {
            var presents = GetDivisors(house).Sum() * 10;
            
            if (presents >= input)
            {
                return house;
            }
        }

        return 0;
    }
    
    public static long GetLowestHouseNumberNonInfinite(int input)
    {
        // Since each elf delivers 11 * its number, if the target input
        // is 1100 then elf # 100 would deliver all 1100 so there is no
        // need to check further
        var maxPossibleHouseNumber = input / 11;
        
        var housesInScope = Enumerable.Range(1, maxPossibleHouseNumber);

        foreach (var house in housesInScope)
        {
            var presents = GetDivisorsNonInfinite(house).Sum() * 11;
            
            if (presents >= input)
            {
                return house;
            }
        }

        return 0;
    }

    private static IEnumerable<int> GetDivisors(int input)
    {
        var divisors = new List<int>();

        for (var i = 1; i <= Math.Sqrt(input); i++)
        {
            if (input % i == 0)
            {
                divisors.Add(i);
                
                var alternateDivisor = input / i;
                if (i != alternateDivisor)
                {
                    divisors.Add(alternateDivisor);
                }
            }
        }

        return divisors;
    }
    
    private static IEnumerable<int> GetDivisorsNonInfinite(int input)
    {
        var divisors = new List<int>();

        for (var i = 1; i <= Math.Sqrt(input); i++)
        {
            if (input % i == 0)
            {
                if (input / i <= 50)
                {
                    divisors.Add(i);
                }

                var alternateDivisor = input / i;
                if (i != alternateDivisor & input / alternateDivisor <= 50)
                {
                    divisors.Add(alternateDivisor);
                }
            }
        }

        return divisors;
    }
}