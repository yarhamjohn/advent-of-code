namespace AdventOfCode.Day8
{
    public static class Day8
    {
        public static int CalculateUniqueDigitAppearance(IEnumerable<string> input) =>
            input
                .Select(line => line.Split("|")[1])
                .SelectMany(result => result.Split(" "))
                .Count(digit => new[] { 2, 3, 4, 7 }.Contains(digit.Length));
    
        public static int CalculateOutputs(IEnumerable<string> input)
        {
            var parsedInput = input.Select(line =>
            {
                var parts = line.Split("|").Select(part => part.Split(" ")).ToArray();
                return (output: parts[0], display: parts[1]);
            });

            var result = 0;
            foreach (var (output, display) in parsedInput)
            {
                var actualMapping = CalculateMapping(output);
                var actualDisplay = CalculateDisplay(display, actualMapping);
                result += Convert.ToInt32(actualDisplay);
            }

            return result;
        }

        private static List<(string map, int digit)> CalculateMapping(string[] output)
        {
            var one = output.Single(x => x.Length == 2).ToArray();
            var four = output.Single(x => x.Length == 4).ToArray();
            var seven = output.Single(x => x.Length == 3).ToArray();
            var eight = output.Single(x => x.Length == 7).ToArray();

            var top = seven.Except(one);
            
            var three = output.Single(x => x.Length == 5 && x.ToArray().Except(one).Count() == 3).ToArray();
            var nine = output.Single(x => x.Length == 6 && x.ToArray().Except(three).Count() == 1).ToArray();
            var two = output.Single(x => x.Length == 5 && x.ToArray().Except(nine).Count() == 1).ToArray();
            
            var bottomLeft = eight.Except(nine);
            var bottom = eight.Except(four).Except(top).Except(bottomLeft);
            var bottomRight = one.Except(two);
            var topRight = one.Except(bottomRight);
            var middle = three.Except(seven).Except(bottom);

            var zero = output.Single(x => x.Length == 6 && x.ToArray().Except(middle).Count() == 6).ToArray();
            var six = output.Single(x => x.Length == 6 && x.ToArray().Except(topRight).Count() == 6).ToArray();
            var five = output.Single(x => x.Length == 5 && x.ToArray().Except(topRight).Except(bottomLeft).Count() == 5)
                .ToArray();

            Array.Sort(zero);
            Array.Sort(one);
            Array.Sort(two);
            Array.Sort(three);
            Array.Sort(four);
            Array.Sort(five);
            Array.Sort(six);
            Array.Sort(seven);
            Array.Sort(eight);
            Array.Sort(nine);

            return new()
            {
                (map: new string(zero), digit: 0),
                (map: new string(one), digit: 1),
                (map: new string(two), digit: 2),
                (map: new string(three), digit: 3),
                (map: new string(four), digit: 4),
                (map: new string(five), digit: 5),
                (map: new string(six), digit: 6),
                (map: new string(seven), digit: 7),
                (map: new string(eight), digit: 8),
                (map: new string(nine), digit: 9)
            };
        }

        private static string CalculateDisplay(string[] display, List<(string map, int digit)> actualMapping)
        {
            return string.Join("", display
                .Where(item => item.Trim().Length != 0)
                .Select(x => actualMapping
                    .Single(mapping =>
                    {
                        var characters = x.ToArray();
                        Array.Sort(characters);
                        return mapping.map == new string(characters);
                    }).digit
                    .ToString()));
        }
    }
}