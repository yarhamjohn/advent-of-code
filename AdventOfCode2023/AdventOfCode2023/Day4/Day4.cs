namespace AdventOfCode2023.Day4;

public static class Day4
{
    public static int CountCardPoints(IEnumerable<string> input)
    {
        var parsedInput = ParseInput(input);

        var result = 0;
        foreach (var (_, value) in parsedInput)
        {
            var numMatches = value.winningNumbers.Intersect(value.playedNumbers).Count();
            if (numMatches == 0)
            {
                continue;
            }

            if (numMatches == 1)
            {
                result += 1;
                continue;
            }

            result += (int) Math.Pow(2, numMatches - 1);
        }
        
        return result;
    }

    private static Dictionary<int, (IEnumerable<int> winningNumbers, IEnumerable<int> playedNumbers)>
        ParseInput(IEnumerable<string> input)
    {
        var result = new Dictionary<int, (IEnumerable<int> winningNumbers, IEnumerable<int> playedNumbers)>();
        foreach (var line in input)
        {
            var splitOne = line.Split(":");
            var splitTwo = splitOne[1].Split("|");
            
            var cardNum = int.Parse(splitOne[0].Split("Card")[1].Trim());

            var winningNumbers =
                splitTwo[0]
                    .Trim()
                    .Split(" ")
                    .Where(x => x != "")
                    .Select(int.Parse);
            var playedNumbers =
                splitTwo[1]
                    .Trim()
                    .Split(" ")
                    .Where(x => x != "")
                    .Select(int.Parse);

            result[cardNum] = (winningNumbers, playedNumbers);
        }

        return result;
    }
}
