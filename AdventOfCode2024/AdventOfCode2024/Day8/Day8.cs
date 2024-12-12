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

    public static long Part2(string[] input)
    {
        return GetResonantAntiNodes(input, ParseInput(input))
            .SelectMany(x => x.Value)
            .Distinct()
            .Count();
    }

    private static Dictionary<char, List<(int row, int col)>> GetResonantAntiNodes(string[] input, Dictionary<char, (int row, int col)[]> antennas)
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
                
                antinodes[antenna].Add(first);

                var distance = (first.row - second.row, first.col - second.col);

                var candidate = (first.row + distance.Item1, first.col + distance.Item2);

                while (true)
                {
                    if (candidate.Item1 < 0 || candidate.Item1 >= input.Length ||
                        candidate.Item2 < 0 || candidate.Item2 >= input.First().Length)
                    {
                        break;
                    }
                    
                    antinodes[antenna].Add(candidate);
                    
                    candidate = (candidate.Item1 + distance.Item1, candidate.Item2 + distance.Item2);
                }
            }
        }

        return antinodes;
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
}