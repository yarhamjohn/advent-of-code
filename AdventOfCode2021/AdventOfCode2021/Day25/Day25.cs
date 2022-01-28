namespace AdventOfCode2021.Day25;

public static class Day25
{
    public static long CalculateStepsWithoutCucumbers(string[] input)
    {
        var seafloor = new char[input.Length, input.First().Length];
        for (var row = 0; row < input.Length; row++)
        {
            var fullRow = input[row];
            for (var col = 0; col < fullRow.Length; col++)
            {
                seafloor[row, col] = fullRow[col];
            }
        }

        var turns = 0;
        while (AnyCucumbersCanMove(seafloor))
        {
            MoveCucumbersEast(seafloor);
            MoveCucumbersSouth(seafloor);

            turns++;
        }
        
        return turns + 1;
    }

    private static void PrintSeafloor(char[,] seafloor)
    {
        for (var row = 0; row < seafloor.GetLength(0); row++)
        {
            for (var col = 0; col < seafloor.GetLength(1); col++)
            {
                Console.Write(seafloor[row, col]);
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }

    private static void MoveCucumbersSouth(char[,] seafloor)
    {
        var moveableCucumbers = GetMoveableSouthCucumberPositions(seafloor);
        foreach (var (row, col) in moveableCucumbers)
        {
            seafloor[row, col] = '.';
            if (row == seafloor.GetLength(0) - 1)
            {
                seafloor[0, col] = 'v';
            }
            else
            {
                seafloor[row + 1, col] = 'v';
            }
        }
    }

    private static IEnumerable<(int row, int col)> GetMoveableSouthCucumberPositions(char[,] seafloor)
    {
        var targets = new List<(int row, int col)>();
        for (var row = 0; row < seafloor.GetLength(0); row++)
        {
            for (var col = 0; col < seafloor.GetLength(1); col++)
            {
                if (CucumberIsMovingSouth(seafloor, row, col) && NextSouthLocationIsEmpty(seafloor, row, col))
                {
                    targets.Add((row, col));
                }
            }
        }

        return targets;
    }

    private static IEnumerable<(int row, int col)> GetMoveableEastCucumberPositions(char[,] seafloor)
    {
        var targets = new List<(int row, int col)>();
        for (var row = 0; row < seafloor.GetLength(0); row++)
        {
            for (var col = 0; col < seafloor.GetLength(1); col++)
            {
                if (CucumberIsMovingEast(seafloor, row, col) && NextEastLocationIsEmpty(seafloor, row, col))
                {
                    targets.Add((row, col));
                }
            }
        }

        return targets;
    }
    
    private static void MoveCucumbersEast(char[,] seafloor)
    {
        var moveableCucumbers = GetMoveableEastCucumberPositions(seafloor);
        foreach (var (row, col) in moveableCucumbers)
        {
            seafloor[row, col] = '.';
            if (col == seafloor.GetLength(1) - 1)
            {
                seafloor[row, 0] = '>';
            }
            else
            {
                seafloor[row, col + 1] = '>';
            }
        }
    }

    private static bool AnyCucumbersCanMove(char[,] seafloor)
    {
        for (var row = 0; row < seafloor.GetLength(0); row++)
        {
            for (var col = 0; col < seafloor.GetLength(1); col++)
            {
                if (CucumberIsMovingEast(seafloor, row, col) && NextEastLocationIsEmpty(seafloor, row, col))
                {
                    return true;
                }

                if (CucumberIsMovingSouth(seafloor, row, col) && NextSouthLocationIsEmpty(seafloor, row, col))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool NextSouthLocationIsEmpty(char[,] seafloor, int row, int col)
    {
        var targetRow = row == seafloor.GetLength(0) - 1 ? 0 : row + 1;
        return seafloor[targetRow, col] == '.';
    }

    private static bool NextEastLocationIsEmpty(char[,] seafloor, int row, int col)
    {
        var targetCol = col == seafloor.GetLength(1) - 1 ? 0 : col + 1;
        return seafloor[row, targetCol] == '.';
    }

    private static bool CucumberIsMovingSouth(char[,] seafloor, int row, int col)
    {
        return seafloor[row, col] == 'v';
    }

    private static bool CucumberIsMovingEast(char[,] seafloor, int row, int col)
    {
        return seafloor[row, col] == '>';
    }
}