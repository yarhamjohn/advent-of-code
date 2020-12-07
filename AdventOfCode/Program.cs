using System;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Day1A(args[0]);
            Day1B(args[0]);
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
        }
    }
}
