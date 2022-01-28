using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day11
{
    public static class Day11
    {
        public static int CountEmptySeats(List<string> input)
        {
            var floor = GetFloor(input);

            while (true)
            {
                var seatsUpdated = UpdateSeats(floor);
                if (!seatsUpdated)
                {
                    break;
                }
            }

            return floor.SelectMany(x => x).Count(y => y == "#");
        }

        public static int CountEmptySeatsNewRules(List<string> input)
        {
            var floor = GetFloor(input);

            while (true)
            {
                var seatsUpdated = UpdateSeatsNewRules(floor);
                if (!seatsUpdated)
                {
                    break;
                }
            }

            return floor.SelectMany(x => x).Count(y => y == "#");
        }
        
        private static string[][] GetFloor(List<string> input)
        {
            return input.Select(x => x.ToArray().Select(y => y.ToString()).ToArray()).ToArray();
        }
        
        private static bool UpdateSeats(string[][] floor)
        {
            var occupiedSeats = new List<(int, int)>();
            var unOccupiedSeats = new List<(int, int)>();
            for (var i = 0; i < floor.GetLength(0); i++)
            {
                for (var j = 0; j < floor[0].Length; j++)
                {
                    var numOccupiedNeighbours = CountNeighbouringOccupiedSeats(floor, i, j);
                    var seatShouldBeOccupied = floor[i][j] == "L" && numOccupiedNeighbours == 0;
                    if (seatShouldBeOccupied)
                    {
                        occupiedSeats.Add((i, j));
                    }
                    
                    var seatShouldBeUnOccupied = floor[i][j] == "#" && numOccupiedNeighbours >= 4;
                    if (seatShouldBeUnOccupied)
                    {
                        unOccupiedSeats.Add((i, j));
                    }
                }

            }

            foreach (var seat in occupiedSeats)
            {
                floor[seat.Item1][seat.Item2] = "#";
            }

            foreach (var seat in unOccupiedSeats)
            {
                floor[seat.Item1][seat.Item2] = "L";
            }

            return occupiedSeats.Any() || unOccupiedSeats.Any();
        }
        
        private static bool UpdateSeatsNewRules(string[][] floor)
        {
            var occupiedSeats = new List<(int, int)>();
            var unOccupiedSeats = new List<(int, int)>();
            for (var i = 0; i < floor.GetLength(0); i++)
            {
                for (var j = 0; j < floor[0].Length; j++)
                {
                    var numOccupiedSeats = CountOccupiedSeatsWithinView(floor, i, j);
                    var seatShouldBeOccupied = floor[i][j] == "L" && numOccupiedSeats == 0;
                    if (seatShouldBeOccupied)
                    {
                        occupiedSeats.Add((i, j));
                    }
                    
                    var seatShouldBeUnOccupied = floor[i][j] == "#" && numOccupiedSeats >= 5;
                    if (seatShouldBeUnOccupied)
                    {
                        unOccupiedSeats.Add((i, j));
                    }
                }

            }

            foreach (var seat in occupiedSeats)
            {
                floor[seat.Item1][seat.Item2] = "#";
            }

            foreach (var seat in unOccupiedSeats)
            {
                floor[seat.Item1][seat.Item2] = "L";
            }

            return occupiedSeats.Any() || unOccupiedSeats.Any();
        }

        private static int CountOccupiedSeatsWithinView(string[][] floor, int row, int col)
        {
            var count = 0;
            var vectors = new List<(int, int)>
                {(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 0), (0, 1), (1, -1), (1, 0), (1, 1)};

            foreach (var vector in vectors)
            {
                var currentX = row + vector.Item1;
                var currentY = col + vector.Item2;
                while (true)
                {
                    if (currentX < 0 || currentY < 0 || currentX >= floor.GetLength(0) || currentY >= floor[0].Length || currentX == row && currentY == col)
                    {
                        // do nothing as cell is off the grid
                        break;
                    }

                    if (floor[currentX][currentY] == ".")
                    {
                        // do nothing as there is no seat
                        currentX += vector.Item1;
                        currentY += vector.Item2;
                        continue;
                    }

                    if (floor[currentX][currentY] == "#")
                    {
                        count++;
                        break;
                    }

                    if (floor[currentX][currentY] == "L")
                    {
                        break;
                    }
                }
            }
            
            return count;
        }

        private static int CountNeighbouringOccupiedSeats(string[][] floor, int row, int col)
        {
            var count = 0;
            for (var i = row - 1; i <= row + 1; i++)
            {
                for (var j = col - 1; j <= col + 1; j++)
                {
                    if (i < 0 || j < 0 || i >= floor.GetLength(0) || j >= floor[0].Length || (i == row && j == col))
                    {
                        // do nothing as cell is off the grid
                        continue;
                    }

                    if (floor[i][j] == ".")
                    {
                        // do nothing as there is no seat
                    }

                    if (floor[i][j] == "#")
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}