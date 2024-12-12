namespace AdventOfCode2024.Day8;

public static class Day8
{
    public static long Part1(string[] input)
    {
        return GetAntiNodes(ParseInput(input))
            .SelectMany(x => x.Value)
            .Distinct()
            .Count(x => IsInGrid(input, x));
    }

    private static bool IsInGrid(string[] input, (int row, int col) position)
    {
        return 
            position.row >= 0 && position.row < input.Length && 
            position.col >= 0 && position.col < input.First().Length;
    }

    private static Dictionary<char, List<(int row, int col)>> GetAntiNodes(Dictionary<char, (int row, int col)[]> antennas)
    {
        var antinodes = new Dictionary<char, List<(int row, int col)>>();

        foreach (var (antenna, positions) in antennas)
        {
            var positionPairs = GetPositionPairs(positions, 2);
            
            antinodes.Add(antenna, []);

            foreach (var pair in positionPairs)
            {
                var first = pair.First();
                var second = pair.Last();

                var distance = (first.row - second.row, first.col - second.col);

                antinodes[antenna].Add((first.row + distance.Item1, first.col + distance.Item2));
            }
        }

        return antinodes;
    }

    private static IEnumerable<(int row, int col)[]> GetPositionPairs((int row, int col)[] positions, int length)
    {
        if (length == 1)
        {
            return positions.Select(pos => new[] { pos });
        }
        
        return positions.SelectMany(pos => GetPositionPairs(positions, length - 1)
            .Select(pair => pair.Append(pos).ToArray()))
            .Where(pair => pair.First().row != pair.Last().row || pair.First().col != pair.Last().col);
    }

    private static Dictionary<char, (int row, int col)[]> ParseInput(string[] input)
    {
        var antennas = new Dictionary<char, (int row, int col)[]>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input.First().Length; col++)
            {
                var antenna = input[row][col];

                if (antenna == '.')
                {
                    continue;
                }

                if (antennas.TryGetValue(antenna, out var positions))
                {
                    antennas[antenna] = positions.Append((row, col)).ToArray();
                }
                else
                {
                    antennas.Add(antenna, [(row, col)]);
                }
            }
        }

        return antennas;
    }

    public static long Part2(string[] input)
    {
        return 0;
    }
}