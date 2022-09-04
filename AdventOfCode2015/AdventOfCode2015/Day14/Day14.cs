namespace AdventOfCode2015.Day14;

public static class Day14
{
    public static long GetWinningDistance(IEnumerable<string> input, int time) =>
        input.Select(x => new Reindeer(x).GetDistanceTravelled(time)).Max();

    public static long GetWinningPoints(IEnumerable<string> input, int time)
    {
        var reindeer = input.Select(x => new Reindeer(x)).ToList();

        var points = reindeer.ToDictionary<Reindeer, Reindeer, long>(deer => deer, _ => 0);

        for (var s = 1; s <= time; s++)
        {
            var reindeerDistances = reindeer.Select(r => (r, r.GetDistanceTravelled(s)));

            var furthestDistance = 0L;
            var leadingReindeer = new List<Reindeer>();
            foreach (var (deer, distance) in reindeerDistances)
            {
                if (distance > furthestDistance)
                {
                    leadingReindeer = new List<Reindeer> { deer };
                    furthestDistance = distance;
                } else if (distance == furthestDistance)
                {
                    leadingReindeer.Add(deer);
                }
            }

            foreach (var d in leadingReindeer)
            {
                points[d] += 1;
            }
        }

        return points.Values.Max();
    }
    
    private class Reindeer
    {
        private readonly int _speed;
        private readonly int _travelTime;
        private readonly int _restTime;
        
        public Reindeer(string input)
        {
            var elements = input.Split(" ");
            
            _speed = Convert.ToInt32(elements[3]);
            _travelTime = Convert.ToInt32(elements[6]);
            _restTime = Convert.ToInt32(elements[^2]);
        }

        public long GetDistanceTravelled(int time)
        {
            var cycleTime = _travelTime + _restTime;
            var distanceTravelled = Math.Floor((decimal) time / cycleTime) * _speed * _travelTime;
            var remainingTime = time % cycleTime;
            
            if (remainingTime < _travelTime)
            {
                distanceTravelled += remainingTime * _speed;
            }
            else
            {
                distanceTravelled += _travelTime * _speed;
            }

            return Convert.ToInt32(distanceTravelled);
        }
    }
}