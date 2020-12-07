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
            Day3A(args[0]);
            Day3B(args[0]);
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
        }
    }
}
