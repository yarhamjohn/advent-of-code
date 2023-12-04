namespace AdventOfCode2023.Day4;

public static class Day4
{
    public static int CountCardPoints(IEnumerable<string> input)
    {
        return ParseInput(input).Aggregate(0, (sum, kvp) => sum + CalculateCardPoints(kvp.Value));
    }

    public static int CountCards(IEnumerable<string> input)
    {
        var parsedInput = ParseInput(input);

        var results = parsedInput.Keys.ToDictionary(x => x, _ => 1);
        foreach (var (card, value) in parsedInput)
        {
            var numMatches = value.winningNumbers.Intersect(value.playedNumbers).Count();
            for (var i = 1; i <= numMatches; i++)
            {
                results[card + i] += results[card];
            }
        }
        
        return results.Values.Sum();
    }

    private static int CalculateCardPoints((IEnumerable<int> winningNumbers, IEnumerable<int> playedNumbers) value)
    {
        var numMatches = value.winningNumbers.Intersect(value.playedNumbers).Count();
        return numMatches switch { _ => numMatches == 0 ? 0 : (int)Math.Pow(2, numMatches - 1) };
    }

    private static Dictionary<int, (IEnumerable<int> winningNumbers, IEnumerable<int> playedNumbers)>
        ParseInput(IEnumerable<string> input)
    {
        var result = new Dictionary<int, (IEnumerable<int> winningNumbers, IEnumerable<int> playedNumbers)>();
        foreach (var line in input)
        {
            var splitOne = line.Split(":");
            var splitTwo = splitOne[1].Split("|");
            
            var cardNum = int.Parse(splitOne[0].Split("Card")[1]);
            var winningNumbers = splitTwo[0].Split(" ").Where(x => x != "").Select(int.Parse);
            var playedNumbers = splitTwo[1].Split(" ").Where(x => x != "").Select(int.Parse);

            result[cardNum] = (winningNumbers, playedNumbers);
        }

        return result;
    }
}
