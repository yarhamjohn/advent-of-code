namespace AdventOfCode.Day9
{
    public static class Day9
    {
        public static int CalculateRiskLevel(string[] input)
        {
            var heightMap = BuildHeightMap(input);

            var lowPoints = CalculateLowPoints(heightMap);

            return lowPoints.Sum(p => ++heightMap[p.x, p.Y]);
        }
        
        public static int CalculateBasins(string[] input)
        {
            var heightMap = BuildHeightMap(input);

            var basins = CalculateBasinSizes(heightMap);

            return basins.OrderByDescending(p => p).Take(3).Aggregate(1, (current, next) => current * next);
        }

        private static IEnumerable<int> CalculateBasinSizes(int[,] heightMap)
        {
            var lowPoints = CalculateLowPoints(heightMap);

            foreach (var point in lowPoints)
            {
                var visitedPoints = new List<(int x, int y)> { point };
                var points = VisitNeighbours(point, heightMap, visitedPoints);
                yield return points.Length;
            }
        }

        private static (int x, int y)[] VisitNeighbours(
            (int x, int y) point,
            int[,] heightMap,
            List<(int x, int y)> visitedPoints)
        {
            var unvisitedNeighbours = GetNeighbourPositions(point.x, point.y)
                .Where(x => !visitedPoints.Contains((x.row, x.col)));

            foreach (var neighbour in unvisitedNeighbours)
            {
                var score = GetScore(neighbour.row, neighbour.col, heightMap);
                if (score != 9 && score is not null)
                {
                    visitedPoints.Add(neighbour);
                    VisitNeighbours(neighbour, heightMap, visitedPoints);
                }
            }

            return visitedPoints.ToArray();
        }

        private static IEnumerable<(int x, int Y)> CalculateLowPoints(int[,] heightMap)
        {
            for (var row = 0; row < heightMap.GetLength(0); row++)
            {
                for (var col = 0; col < heightMap.GetLength(1); col++)
                {
                    if (IsLowPoint(row, col, heightMap))
                    {
                        yield return (row, col);
                    }
                }
            }
        }

        private static bool IsLowPoint(int row, int col, int[,] heightMap)
        {
            var neighbours = GetNeighbourPositions(row, col);
            
            var scores = neighbours.Select(x => GetScore(x.row, x.col, heightMap));
            var targetScore = GetScore(row, col, heightMap);

            return scores.All(x => x == null || x > targetScore);
        }

        private static IEnumerable<(int row, int col)> GetNeighbourPositions(int row, int col) =>
            new[]
            {
                (row: row - 1, col),
                (row: row + 1, col),
                (row, col: col - 1),
                (row, col: col + 1)
            };

        private static int? GetScore(int row, int col, int[,] heightMap)
        {
            var rowIsOffMap = row < 0 || row >= heightMap.GetLength(0);
            var colIsOffMap = col < 0 || col >= heightMap.GetLength(1);
            
            return rowIsOffMap || colIsOffMap ? null : heightMap[row, col];
        }

        private static int[,] BuildHeightMap(IReadOnlyList<string> input)
        {
            var heightmap = new int[input.Count, input[0].Length];
            for (var row = 0; row < input.Count; row++)
            {
                var points = input[row].ToArray();
                for (var col = 0; col < points.Length; col++)
                {
                    heightmap[row, col] = points[col] - '0';
                }
            }
            
            return heightmap;
        }
    }
}