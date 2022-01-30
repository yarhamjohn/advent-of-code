namespace AdventOfCode2015.Day3;

public static class Day3
{
    public static long GetNumberOfHouses(string input)
    {
        var housesVisited = new HashSet<(int x, int y)> { (0, 0) };
        var currentPosition = (0, 0);
        foreach (var direction in input)
        {
            currentPosition = direction switch
            {
                '^' => (currentPosition.Item1, currentPosition.Item2 - 1),
                'v' => (currentPosition.Item1, currentPosition.Item2 + 1),
                '<' => (currentPosition.Item1 - 1, currentPosition.Item2),
                _ => (currentPosition.Item1 + 1, currentPosition.Item2)
            };

            housesVisited.Add(currentPosition);
        }
        
        return housesVisited.Count;
    }
    
    public static long GetNumberOfHousesWithRoboSanta(string input)
    {
        var housesVisited = new HashSet<(int x, int y)> { (0, 0) };
        var currentPosition = (0, 0);
        var currentPositionRoboSanta = (0, 0);
        
        foreach (var direction in input.Where((x, i) => i % 2 == 0))
        {
            currentPosition = direction switch
            {
                '^' => (currentPosition.Item1, currentPosition.Item2 - 1),
                'v' => (currentPosition.Item1, currentPosition.Item2 + 1),
                '<' => (currentPosition.Item1 - 1, currentPosition.Item2),
                _ => (currentPosition.Item1 + 1, currentPosition.Item2)
            };

            housesVisited.Add(currentPosition);
        }
        
        foreach (var direction in input.Where((x, i) => i % 2 == 1))
        {
            currentPositionRoboSanta = direction switch
            {
                '^' => (currentPositionRoboSanta.Item1, currentPositionRoboSanta.Item2 - 1),
                'v' => (currentPositionRoboSanta.Item1, currentPositionRoboSanta.Item2 + 1),
                '<' => (currentPositionRoboSanta.Item1 - 1, currentPositionRoboSanta.Item2),
                _ => (currentPositionRoboSanta.Item1 + 1, currentPositionRoboSanta.Item2)
            };

            housesVisited.Add(currentPositionRoboSanta);
        }
        
        return housesVisited.Count;
    }
}