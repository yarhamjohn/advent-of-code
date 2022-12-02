namespace AdventOfCode2016.Day15;

public static class Day15
{
    public static long GetEarliestTime(IEnumerable<string> input)
    {
        var discs = ParseInput(input);

        var elapsedTime = 0;

        while (!DiscsAligned(discs, elapsedTime))
        {
            elapsedTime++;
        }
        
        return elapsedTime;
    }

    private static bool DiscsAligned(IEnumerable<Disc> discs, int elapsedTime)
    {
        return discs.All(x1 => x1.IsAligned(elapsedTime));
    }

    private static IEnumerable<Disc> ParseInput(IEnumerable<string> input)
    {
        return input
            .Select(line => line.Split(" "))
            .Select(elements => new Disc(
                Convert.ToInt32(elements[1].Replace("#","")), 
                Convert.ToInt32(elements[3]), 
                Convert.ToInt32(elements[11].Replace(".", ""))));
    }

    private class Disc
    {
        private readonly int _id;
        private readonly int _numPositions;
        private readonly int _startPosition;

        public Disc(int id, int numPositions, int currentPosition)
        {
            _id = id;
            _numPositions = numPositions;
            _startPosition = currentPosition;
        }

        public bool IsAligned(int buttonPressTime)
        {
            return (_startPosition + buttonPressTime + _id) % _numPositions == 0;
        }
    }
}