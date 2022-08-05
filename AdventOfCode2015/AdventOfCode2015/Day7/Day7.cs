using System.Runtime.CompilerServices;

namespace AdventOfCode2015.Day7
{
    public static class Day7
    {
        private static string[] Gates = { "AND", "OR", "NOT", "LSHIFT", "RSHIFT" };

        public static int GetWireSignal(string wire, string[] input)
        {
            var wireDefinitions = GetWireDefinitions(input);
            foreach (var w in wireDefinitions)
            {
                Console.WriteLine($"[{w.Key}, [{string.Join(", ", w.Value)}]]");
            }

            var definition = ConsolidateDefinitions(wireDefinitions, wireDefinitions[wire]);

            return Calculate(definition);
        }

        private static string[] ConsolidateDefinitions2(Dictionary<string, string[]> wireDefinitions,
            string[] definition)
        {
            // Find every definition that is a number.
            // Replace all uses in other definitions.
            // Do any possible calculation.
            // Repeat


            // foreach element, replace with definition if not gate or number
            var result = new[] { definition };
            while (result.SelectMany(x => x).Any(y => !Gates.Contains(y) && !int.TryParse(y, out _)))
            {
                for (var i = result.Length - 1; i >= 0; i--)
                {
                    var replacement = new List<string>();
                    foreach (var elem in result[i])
                    {
                        if (!Gates.Contains(elem) && !int.TryParse(elem, out _))
                        {
                            replacement.AddRange(wireDefinitions[elem]);
                        }

                        else
                        {
                            replacement.Add(elem);
                        }
                    }

                    result[i] = replacement.ToArray();
                }
            }

            return result.SelectMany(x => x).ToArray();
        }

        private static string[] ConsolidateDefinitions(Dictionary<string, string[]> wireDefinitions,
            string[] definition)
        {
            // Find every definition that is a number.
            // Replace all uses in other definitions.
            // Do any possible calculation.
            // Repeat
                var knownKeys = wireDefinitions.Where(x => x.Value.Length == 1 && int.TryParse(x.Value[0], out _))
                    .Select(y => y.Key).ToArray();
                foreach (var key in knownKeys)
                {
                    var val = wireDefinitions[key][0];
                    foreach (var def in wireDefinitions)
                    {
                        wireDefinitions[def.Key] = def.Value.Select(x => x == key ? val : x).ToArray();
                    }
                }

                foreach (var w in wireDefinitions)
                {
                    Console.WriteLine($"[{w.Key}, [{string.Join(", ", w.Value)}]]");
                }

            return Array.Empty<string>();
        }

        private static string[] ConsolidateDefinitions1(Dictionary<string, string[]> wireDefinitions,
            string[] definition)
        {
            if (definition.Length == 1)
            {
                if (int.TryParse(definition[0], out var value))
                {
                    return new[] { value.ToString() };
                }

                return wireDefinitions[definition[0]];
            }

            if (definition.Length == 2)
            {
                var gate = new[] { definition[0] };
                var remainder = ConsolidateDefinitions(wireDefinitions, new[] { definition[1] });

                return gate.Concat(remainder).ToArray();
            }

            if (definition.Length == 3)
            {
                var partOne = ConsolidateDefinitions(wireDefinitions, new[] { definition[0] });
                var gate = new[] { definition[1] };
                var partTwo = ConsolidateDefinitions(wireDefinitions, new[] { definition[2] });

                return partOne.Concat(gate).Concat(partTwo).ToArray();
            }

            throw new Exception($"Unexpected definition: {string.Join(",", definition)}");
        }

        private static int Calculate(string[] definition)
        {
            if (definition.Length == 1)
            {
                return Convert.ToInt32(definition[0]);
            }

            if (definition[0] == "NOT")
            {
                var notResult = GetBitwiseComplement(definition);
                return Calculate(new[] { notResult.ToString() }.Concat(definition[2..]).ToArray());
            }

            var gateResult = CalculateBitwiseOperation(definition[0], definition[1], definition[2]);
            return Calculate(new[] { gateResult.ToString() }.Concat(definition[3..]).ToArray());
        }

        private static int CalculateBitwiseOperation(string left, string gate, string right)
        {
            return gate switch
            {
                "AND" => Convert.ToInt32(left) & Convert.ToInt32(right),
                "OR" => Convert.ToInt32(left) | Convert.ToInt32(right),
                "LSHIFT" => Convert.ToInt32(left) << Convert.ToInt32(right),
                "RSHIFT" => Convert.ToInt32(left) >> Convert.ToInt32(right),
                _ => throw new Exception("Unknown gate")
            };
        }

        private static int GetBitwiseComplement(string[] definition)
        {
            var twosComplement = Convert.ToString(~Convert.ToInt32(definition[1]), 2);
            var sixteenBit = twosComplement[^16..].PadLeft(32, '0');
            return Convert.ToInt32(sixteenBit, 2);
        }

        private static Dictionary<string, string[]> GetWireDefinitions(string[] input)
        {
            var dict = new Dictionary<string, string[]>();
            foreach (var line in input)
            {
                var elems = line.Split("->").Select(x => x.Trim()).ToArray();
                dict[elems.Last()] = elems.First().Split(" ");
            }

            return dict;
        }
    }
}