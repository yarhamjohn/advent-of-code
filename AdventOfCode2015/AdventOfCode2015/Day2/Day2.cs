namespace AdventOfCode2015.Day2;

public static class Day2
{
    public static int GetRequiredArea(string[] input)
    {
        var total = 0;

        foreach (var box in input)
        {
            var dimensions = box.Split("x").Select(x => Convert.ToInt32(x)).ToArray();
            var side1 = dimensions[0] * dimensions[1];
            var side2 = dimensions[0] * dimensions[2];
            var side3 = dimensions[1] * dimensions[2];

            total += (side1 + side2 + side3) * 2 + new[] { side1, side2, side3 }.Min();
        }

        return total;
    }
    
    public static int GetRequiredLength(string[] input)
    {
        var total = 0;

        foreach (var box in input)
        {
            var dimensions = box.Split("x").Select(x => Convert.ToInt32(x)).ToArray();
            
            var volume = dimensions.Aggregate(1, (current, next) => current * next);
            var boxLength = dimensions.OrderBy(x => x).Take(2).Select(x => new[] { x, x }).SelectMany(y => y).Sum();
            
            total += volume + boxLength;
        }

        return total;
    }
}