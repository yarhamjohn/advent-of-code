namespace AdventOfCode2022.Day15;

public static class Day15
{
    public static long CountPositionsWithoutBeacons(string[] input, int targetRow)
    {
        var beaconsAndSensors = Parse(input);
        var coveredRowRanges = GetCoveredRowRanges(beaconsAndSensors);
        
        var x = coveredRowRanges.ContainsKey(targetRow) ? CalculateCoveredPositions(targetRow, coveredRowRanges, beaconsAndSensors) : 0;
        return x;
    }

    private static long CalculateCoveredPositions(int targetRow, Dictionary<int, List<(int startCol, int endCol)>> coveredRowRanges, List<((int col, int row) sensor, (int col, int row) beacon)> sensorsAndBeacons)
    {
        var ranges = coveredRowRanges.Single(x => x.Key == targetRow).Value;

        var startCol = ranges.Min(x => x.startCol);
        var endCol = ranges.Max(x => x.endCol);
        var row = Enumerable.Range(0, endCol - startCol + 1).Select(_ => '.').ToArray();

        foreach (var (start, end) in ranges)
        {
            for (var i = start; i <= end; i++)
            {
                if (!sensorsAndBeacons.Any(x =>
                        x.sensor.row == targetRow && x.sensor.col == i ||
                        x.beacon.row == targetRow && x.beacon.col == i))
                {
                    row[i - startCol] = '#';
                }
            }
        }

        return row.Count(x => x == '#');
    }

    private static Dictionary<int, List<(int startCol, int endCol)>> GetCoveredRowRanges(List<((int col, int row) sensor, (int col, int row) beacon)> beaconsAndSensors)
    {
        var result = new Dictionary<int, List<(int startCol, int endCol)>>();

        foreach (var (sensor, beacon) in beaconsAndSensors)
        {
            var colDistance = Math.Abs(sensor.col - beacon.col);
            var rowDistance = Math.Abs(sensor.row - beacon.row);
            var manhattanDistance = colDistance + rowDistance;

            var startRow = sensor.row - manhattanDistance;
            var endRow = sensor.row + manhattanDistance;

            var additionalPlaces = 0; // first row is 1 col wide
            for (var i = startRow; i <= endRow; i++)
            {
                if (result.ContainsKey(i))
                {
                    result[i].Add((sensor.col - additionalPlaces, sensor.col + additionalPlaces));
                }
                else
                {
                    result[i] = new List<(int startCol, int endCol)>
                        { (sensor.col - additionalPlaces, sensor.col + additionalPlaces) };
                }

                if (i < sensor.row)
                {
                    additionalPlaces++;
                }
                else
                {
                    additionalPlaces--;
                }
            }
        }

        return result;
    }

    private static List<((int col, int row) sensor, (int col, int row) beacon)> Parse(IEnumerable<string> input)
    {
        return input
            .Select(line => line.Split(": "))
            .Select(x => (sensor: ParseObject(x[0][10..]), beacon: ParseObject(x[1][22..])))
            .ToList();
    }

    private static (int col, int row) ParseObject(string s)
    {
        var segments = s.Split(", ").Select(x => Convert.ToInt32(x.Split("=")[1])).ToArray();

        return (segments[0], segments[1]);
    }
}