using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day5
{
    public static class Day5
    {
        public static int GetHighestSeatId(List<string> input)
        {
            return input.Select(GetSeatId).Max();
        }

        public static int GetMySeatId(List<string> input)
        {
            var seats = input.Select(GetSeatId).ToList();
            for (var i = seats.Min(); i < seats.Max(); i++)
            {
                if (!seats.Contains(i))
                {
                    return i;
                }
            }

            throw new InvalidOperationException("There was no missing seat.");
        }

        private static int GetSeatId(string seat)
        {
            var rowInfo = seat.Substring(0, 7);
            var columnInfo = seat.Substring(7, 3);

            var row = GetRow(rowInfo);
            var column = GetColumn(columnInfo);

            return CalculateSeatId(row, column);
        }

        private static int CalculateSeatId(int row, int column) => row * 8 + column;

        private static int GetColumn(string columnInfo)
        {
            var columns = Enumerable.Range(0, 8).ToList();
            return columnInfo.Aggregate(columns, (current, step) => step switch
            {
                'L' => current.Where(x => x <= (current.Max() - current.Min()) / 2 + current.Min()).ToList(),
                'R' => current.Where(x => x > (current.Max() - current.Min()) / 2 + current.Min()).ToList(),
                _ => throw new InvalidOperationException($"Not a valid character: {step}")
            }).Single();
        }

        private static int GetRow(string rowInfo)
        {
            var rows = Enumerable.Range(0, 128).ToList();
            return rowInfo.Aggregate(rows, (current, step) => step switch
            {
                'F' => current.Where(x => x <= (current.Max() - current.Min()) / 2 + current.Min()).ToList(),
                'B' => current.Where(x => x > (current.Max() - current.Min()) / 2 + current.Min()).ToList(),
                _ => throw new InvalidOperationException($"Not a valid character: {step}")
            }).Single();
        }
    }
}