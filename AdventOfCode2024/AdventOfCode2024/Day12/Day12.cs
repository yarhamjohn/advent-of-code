namespace AdventOfCode2024.Day12;

public static class Day12
{
    public static long Part1(string[] input)
    {
        var x = new Dictionary<int, (int row, int col, int perimeter)[]>();

        var counter = 0;
        
        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input.First().Length; col++)
            {
                var neighbours = new[]
                {
                    (row - 1, col),
                    (row + 1, col),
                    (row, col + 1),
                    (row, col - 1),
                }.Where(pos => 
                    pos.Item1 >= 0 && pos.Item1 < input.Length && 
                    pos.Item2 >= 0 && pos.Item2 < input.First().Length)
                .ToArray();
                
                var perimeter = 4 - neighbours.Length + neighbours.Count(n => input[n.Item1][n.Item2] != input[row][col]);

                var isInExistingArea = false;
                foreach (var position in neighbours)
                {
                    if (input[position.Item1][position.Item2] != input[row][col])
                    {
                        continue;
                    }
                    
                    var matchingArea = x.Where(xyz => xyz.Value.Any(s => s.row == position.Item1 && s.col == position.Item2)).ToArray();

                    if (matchingArea.Length == 0)
                    {
                        continue;
                    }
                    
                    isInExistingArea = true;
                    x[matchingArea.Single().Key] = x[matchingArea.Single().Key].Append((row, col, perimeter)).ToArray();
                    break;
                }

                if (!isInExistingArea)
                {
                    x[counter] = [(row, col, perimeter)];
                    counter++;
                }
            }
        }
        
        return x
            .Select(y => 
                y.Value.Length * y.Value.Sum(z => z.perimeter))
            .Sum();
    }

    public static long Part2(string[] input)
    {
        return 0;
    }
}