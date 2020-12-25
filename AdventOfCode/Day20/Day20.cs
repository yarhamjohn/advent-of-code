using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day20
    {
        public static long SumCorners(List<string> input)
        {
            var tiles = GetTiles(input);
            var tileEdges = GetTileEdges(tiles);
            var cornerTileIds = GetCornerTiles(tileEdges);
            return cornerTileIds.Aggregate(1L, (x, y) => x * y);
        }

        private static Dictionary<long, List<string>> GetTileEdges(Dictionary<long, List<string>> tiles)
        {
            var edges = new Dictionary<long, List<string>>();
            foreach (var tile in tiles)
            {
                var listOfEdges = new List<string>
                {
                    tile.Value.First(),
                    tile.Value.Last(),
                    tile.Value.Select(x => x[^1].ToString()).Aggregate("", (x, y) => x + y),
                    tile.Value.Select(x => x[^1].ToString()).Aggregate("", (x, y) => x + y)
                };

                edges[tile.Key] = listOfEdges;
            }

            return edges;
        }

        private static IEnumerable<long> GetCornerTiles(Dictionary<long, List<string>> tileEdges)
        {
            foreach (var (key, value) in tileEdges)
            {
                var edges = tileEdges.Where(x => x.Key != key).SelectMany(x => x.Value).ToList();
                if (CountPairedEdges(value, edges) == 2)
                {
                    yield return key;
                }
            }
        }

        private static int CountPairedEdges(List<string> value, List<string> tileEdges)
        {
            return value.Count(x => !tileEdges.Contains(x) && !tileEdges.Contains(x.Reverse()));
        }

        private static Dictionary<long, List<string>> GetTiles(List<string> input)
        {
            var tiles = new Dictionary<long, List<string>>();

            for (var i = 0; i < input.Count; i += 12)
            {
                var tileId = Convert.ToInt64(input[i].Split(" ")[1].Replace(":", "").Trim());
                var tileContents = input.GetRange(i + 1, 10);
                tiles[tileId] = tileContents;
            }

            return tiles;
        }
    }
}