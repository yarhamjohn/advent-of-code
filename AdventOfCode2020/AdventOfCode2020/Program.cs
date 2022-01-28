using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Day1A(args[0]);
            // Day1B(args[0]);
            // Day2A(args[0]);
            // Day2B(args[0]);
            // Day3A(args[0]);
            // Day3B(args[0]);
            // Day4A(args[0]);
            // Day4B(args[0]);
            // Day5A(args[0]);
            // Day5B(args[0]);
            // Day6A(args[0]);
            // Day6B(args[0]);
            // Day7A(args[0]);
            // Day7B(args[0]);
            // Day8A(args[0]);
            // Day8B(args[0]);
            // Day9A(args[0]);
            // Day9B(args[0]);
            // Day10A(args[0]);
            // Day10B(args[0]);
            // Day11A(args[0]);
            // Day11B(args[0]);
            // Day12A(args[0]);
            // Day12B(args[0]);
            // Day13A(args[0]);
            // Day13B(args[0]);
            // Day14A(args[0]);
            // Day14B(args[0]);
            // Day15A(args[0]);
            // Day15B(args[0]);
            // Day16A(args[0]);
            // Day16B(args[0]);
            Day17A(args[0]);
        }

        private static void Day1A(string path)
        {
            var input = File.ReadLines(path);
            var total = Day1.Day1.CalculateTotalForTwoNumbers(input, 2020);
            Console.WriteLine(total);
        }

        private static void Day1B(string path)
        {
            var input = File.ReadLines(path);
            var total = Day1.Day1.CalculateTotalForThreeNumbers(input);
            Console.WriteLine(total);
        }
        
        private static void Day2A(string path)
        {
            var input = File.ReadLines(path);
            var total = Day2.Day2.CountValidPasswordsWrongPolicy(input);
            Console.WriteLine(total);
        }
        
        private static void Day2B(string path)
        {
            var input = File.ReadLines(path);
            var total = Day2.Day2.CountValidPasswordsCorrectPolicy(input);
            Console.WriteLine(total);
        }
        
        private static void Day3A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day3.Day3.CountTreesHit(input, 3, 1);
            Console.WriteLine(total);
        }
        
        private static void Day3B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total1 = Day3.Day3.CountTreesHit(input, 1, 1);
            var total2 = Day3.Day3.CountTreesHit(input, 3, 1);
            var total3 = Day3.Day3.CountTreesHit(input, 5, 1);
            var total4 = Day3.Day3.CountTreesHit(input, 7, 1);
            var total5 = Day3.Day3.CountTreesHit(input, 1, 2);
            Console.WriteLine((long)total1 * total2 * total3 * total4 * total5);
        }
        
        private static void Day4A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day4.Day4.CountPassportsWithExpectedFields(input);
            Console.WriteLine(total);
        }
        
        private static void Day4B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day4.Day4.CountPassportsWithValidFields(input);
            Console.WriteLine(total);
        }
        
        private static void Day5A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day5.Day5.GetHighestSeatId(input);
            Console.WriteLine(total);
        }
        
        private static void Day5B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day5.Day5.GetMySeatId(input);
            Console.WriteLine(total);
        }
        
        private static void Day6A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day6.Day6.CalculateQuestionSumAllQuestions(input);
            Console.WriteLine(total);
        }
        
        private static void Day6B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day6.Day6.CalculateQuestionSumSameQuestions(input);
            Console.WriteLine(total);
        }
        
        private static void Day7A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day7.Day7.CountContainingBagColours(input);
            Console.WriteLine(total);
        }
        
        private static void Day7B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day7.Day7.CountContainingBags(input);
            Console.WriteLine(total);
        }
        
        private static void Day8A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day8.Day8.GetNumBeforeInfiniteLoop(input);
            Console.WriteLine(total);
        }
        
        private static void Day8B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day8.Day8.GetNumAfterBreakingInfiniteLoop(input);
            Console.WriteLine(total);
        }
        
        private static void Day9A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day9.Day9.GetInvalidNum(input, 25);
            Console.WriteLine(total);
        }
        
        private static void Day9B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day9.Day9.GetWeakness(input, 25);
            Console.WriteLine(total);
        }
        
        private static void Day10A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day10.Day10.GetJoltDifference(input);
            Console.WriteLine(total);
        }
        
        private static void Day10B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day10.Day10.GetAdapterArrangements(input);
            Console.WriteLine(total);
        }
        
        private static void Day11A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day11.Day11.CountEmptySeats(input);
            Console.WriteLine(total);
        }
        
        private static void Day11B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day11.Day11.CountEmptySeatsNewRules(input);
            Console.WriteLine(total);
        }
        
        private static void Day12A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day12.Day12.CalculateManhattanDistance(input);
            Console.WriteLine(total);
        }
        
        private static void Day12B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day12.Day12.CalculateManhattanDistanceUsingWaypoint(input);
            Console.WriteLine(total);
        }
        
        private static void Day13A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day13.Day13.CalculateBusId(input);
            Console.WriteLine(total);
        }
        
        private static void Day13B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day13.Day13.CalculateTimestamp(input);
            Console.WriteLine(total);
        }
        
        private static void Day14A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day14.Day14.SumInMemoryValues(input);
            Console.WriteLine(total);
        }
        
        private static void Day14B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day14.Day14.SumInMemoryValuesVersion2(input);
            Console.WriteLine(total);
        }
        
        private static void Day15A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day15.Day15.FindNumber(input[0], 2020);
            Console.WriteLine(total);
        }
        
        private static void Day15B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day15.Day15.FindNumber(input[0], 30000000);
            Console.WriteLine(total);
        }
        
        private static void Day16A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day16.Day16.CalculateScanningErrorRate(input);
            Console.WriteLine(total);
        }
        
        private static void Day16B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day16.Day16.CalculateDepartureFields(input, "departure");
            Console.WriteLine(total);
        }
        
        private static void Day17A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day17.Day17.CountActiveCubes(input);
            Console.WriteLine(total);
        }
    }
}