using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
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
            Day7B(args[0]);
        }

        private static void Day1A(string path)
        {
            var input = File.ReadLines(path);
            var total = Day1.CalculateTotalForTwoNumbers(input, 2020);
            Console.WriteLine(total);
        }

        private static void Day1B(string path)
        {
            var input = File.ReadLines(path);
            var total = Day1.CalculateTotalForThreeNumbers(input);
            Console.WriteLine(total);
        }
        
        private static void Day2A(string path)
        {
            var input = File.ReadLines(path);
            var total = Day2.CountValidPasswordsWrongPolicy(input);
            Console.WriteLine(total);
        }
        
        private static void Day2B(string path)
        {
            var input = File.ReadLines(path);
            var total = Day2.CountValidPasswordsCorrectPolicy(input);
            Console.WriteLine(total);
        }
        
        private static void Day3A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day3.CountTreesHit(input, 3, 1);
            Console.WriteLine(total);
        }
        
        private static void Day3B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total1 = Day3.CountTreesHit(input, 1, 1);
            var total2 = Day3.CountTreesHit(input, 3, 1);
            var total3 = Day3.CountTreesHit(input, 5, 1);
            var total4 = Day3.CountTreesHit(input, 7, 1);
            var total5 = Day3.CountTreesHit(input, 1, 2);
            Console.WriteLine((long)total1 * total2 * total3 * total4 * total5);
        }
        
        private static void Day4A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day4.CountPassportsWithExpectedFields(input);
            Console.WriteLine(total);
        }
        
        private static void Day4B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day4.CountPassportsWithValidFields(input);
            Console.WriteLine(total);
        }
        
        private static void Day5A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day5.GetHighestSeatId(input);
            Console.WriteLine(total);
        }
        
        private static void Day5B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day5.GetMySeatId(input);
            Console.WriteLine(total);
        }
        
        private static void Day6A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day6.CalculateQuestionSumAllQuestions(input);
            Console.WriteLine(total);
        }
        
        private static void Day6B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day6.CalculateQuestionSumSameQuestions(input);
            Console.WriteLine(total);
        }
        
        private static void Day7A(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day7.CountContainingBagColours(input);
            Console.WriteLine(total);
        }
        
        private static void Day7B(string path)
        {
            var input = File.ReadLines(path).ToList();
            var total = Day7.CountContainingBags(input);
            Console.WriteLine(total);
        }
    }
}