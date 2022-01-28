namespace AdventOfCode2021.Day11
{
    public static class Day11
    {
        public static int CalculateNumberOfFlashes(string[] input)
        {
            var octoGrid = CreateOctoGrid(input);

            var turns = 100;
            var count = 0;
            while (turns > 0)
            {
                count += ProcessTurn(octoGrid);

                turns--;
            }
            return count;
        }

        public static int CalculateFirstSimultaneousFlash(string[] input)
        {
            var octoGrid = CreateOctoGrid(input);

            var turns = 0;
            while (true)
            {
                var extra = ProcessTurn(octoGrid);
                
                if (extra == octoGrid.GetLength(0) * octoGrid.GetLength(1))
                {
                    break;
                }

                turns++;
            }
            
            return turns + 1;
        }

        private static void PrintOctoGrid(int[,] octoGrid)
        {
            for (var row = 0; row < octoGrid.GetLength(0); row++)
            {
                for (var col = 0; col < octoGrid.GetLength(1); col++)
                {
                    Console.Write(octoGrid[row, col].ToString().PadLeft(2));
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private static int[,] CreateOctoGrid(string[] input)
        {
            var octoGrid = new int[input.Length, input.Length];
            for (var row = 0; row < input.Length; row++)
            {
                var cells = input[row].ToArray().Select(x => x - '0').ToArray();
                for (var col = 0; col < cells.Length; col++)
                {
                    octoGrid[row, col] = cells[col];
                }
            }

            return octoGrid;
        }

        private static int ProcessTurn(int[,] octoGrid)
        {
            IncrementEachOctopus(octoGrid);

            var count = 0;
            var flashed = new HashSet<(int row, int col)>();
            while (AnyOctopusCanFlash(octoGrid))
            {
                var latest = FlashOctopi(octoGrid);
                foreach (var octopus in latest)
                {
                    flashed.Add(octopus);
                }
                
                foreach (var (row, col) in latest)
                {
                    octoGrid[row, col] = 0;
                }
            }
            
            foreach (var (row, col) in flashed)
            {
                count++;
                octoGrid[row, col] = 0;
            }

            return count;
        }

        private static void IncrementEachOctopus(int[,] octoGrid)
        {
            for (var row = 0; row < octoGrid.GetLength(0); row++)
            {
                for (var col = 0; col < octoGrid.GetLength(1); col++)
                {
                    octoGrid[row, col]++;
                }
            }
        }

        private static HashSet<(int row, int col)> FlashOctopi(int[,] octoGrid)
        {
            var flashed = new HashSet<(int row, int col)>();
            for (var row = 0; row < octoGrid.GetLength(0); row++)
            {
                for (var col = 0; col < octoGrid.GetLength(1); col++)
                {
                    if (octoGrid[row, col] > 9)
                    {
                        flashed.Add((row, col));

                        IncrementNeighbours(octoGrid, row, col);
                    }
                }
            }
            
            return flashed;
        }

        private static void IncrementNeighbours(int[,] octoGrid, int row, int col)
        {
            for (var i = row - 1; i <= row + 1; i++)
            {
                for (var j = col - 1; j <= col + 1; j++)
                {
                    var offGrid = i < 0 || i >= octoGrid.GetLength(0) || j < 0 || j >= octoGrid.GetLength(1);
                    if (!offGrid)
                    {
                        octoGrid[i, j]++;
                    }
                }
            }
        }

        private static bool AnyOctopusCanFlash(int[,] octoGrid) => octoGrid.Cast<int>().Any(elem => elem > 9);
    }
}
