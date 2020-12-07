using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    public static class Day4
    {
        public static int CountPassportsWithExpectedFields(IEnumerable<string> input)
        {
            var passports = GetPassports(input);
            return passports.Count(x => x.ContainsAllRequiredFields());
        }

        public static int CountPassportsWithValidFields(IEnumerable<string> input)
        {
            var passports = GetPassports(input);
            return passports.Count(x => x.AllFieldsAreValid());
        }

        private static IEnumerable<Passport> GetPassports(IEnumerable<string> input)
        {
            var currentPassport = new StringBuilder();
            foreach (var line in input)
            {
                if (line.Trim().Length == 0)
                {
                    currentPassport.Append(';');
                    continue;
                }

                currentPassport.Append(' ');
                currentPassport.Append(line);
            }

            return currentPassport.ToString().Split(";").Select(CreatePassport).ToList();
        }

        private static Passport CreatePassport(string passportLine)
        {
            var pairs = passportLine.Split(" ").Where(x => x.Trim() != "").Select(x =>
            {
                var keyValuePair = x.Split(":");
                return KeyValuePair.Create(keyValuePair[0], keyValuePair[1]);
            }).ToDictionary(x => x.Key, x => x.Value);

            return new Passport(pairs);
        }
    }
}