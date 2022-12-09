namespace AdventOfCode2022.Day9;

public static class Day9
{
    public static long CalculatePositionsVisited(string[] input)
    {
        var currentTailPosition = (x: 0, y: 0);
        var currentHeadPosition = (x: 0, y: 0);
     
        var positionsVisited = new HashSet<(int, int)> {currentTailPosition};
        
        foreach (var instruction in input.Select(x => x.Split(" ")))
        {
            for (var step = 0; step < Convert.ToInt32(instruction[1]); step++)
            {
                currentHeadPosition = GetNewHeadPosition(currentHeadPosition, instruction[0]);

                if (NotAdjacent(currentHeadPosition, currentTailPosition))
                {
                    currentTailPosition = GetNewTailPosition(currentTailPosition, currentHeadPosition, instruction[0]);
                }
                
                positionsVisited.Add(currentTailPosition);
            }
        }
        
        return positionsVisited.Count;
    }

    private static (int x, int y) GetNewTailPosition(
        (int x, int y) currentTailPosition, 
        (int x, int y) currentHeadPosition, 
        string direction)
        => direction switch
        {
            "U" => (currentTailPosition.x - 1, GetNewYPosition(currentHeadPosition.y, currentTailPosition.y)),
            "D" => (currentTailPosition.x + 1, GetNewYPosition(currentHeadPosition.y, currentTailPosition.y)),
            "L" => (GetNewXPosition(currentHeadPosition.x, currentTailPosition.x), currentTailPosition.y - 1),
            "R" => (GetNewXPosition(currentHeadPosition.x, currentTailPosition.x), currentTailPosition.y + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

    private static (int x, int y) GetNewHeadPosition((int x, int y) currentHeadPosition, string direction)
        => direction switch
        {
            "U" => (currentHeadPosition.x - 1, currentHeadPosition.y),
            "D" => (currentHeadPosition.x + 1, currentHeadPosition.y),
            "L" => (currentHeadPosition.x, currentHeadPosition.y - 1),
            "R" => (currentHeadPosition.x, currentHeadPosition.y + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };

    private static int GetNewYPosition(int headYPosition, int tailYPosition) 
        => headYPosition == tailYPosition ? tailYPosition : headYPosition;

    private static int GetNewXPosition(int headXPosition, int tailXPosition) 
        => headXPosition == tailXPosition ? tailXPosition : headXPosition;
    
    private static bool NotAdjacent((int x, int y) currentHeadPosition, (int x, int y) currentTailPosition)
        => Math.Abs(currentHeadPosition.x - currentTailPosition.x) >= 2
           || Math.Abs(currentHeadPosition.y - currentTailPosition.y) >= 2;
}