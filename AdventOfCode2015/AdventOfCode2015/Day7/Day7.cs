namespace AdventOfCode2015.Day7
{
    public static class Day7
    {
        public static int GetWireSignal(string wire, string[] input)
        {
            var wireDefinitions = GetWireDefinitions(input);

            var wireSignals = CalculateSignals(wireDefinitions);

            return wireSignals[wire];
        }
        
        public static int GetWireSignal2(string wire, string[] input)
        {
            var wireDefinitions = GetWireDefinitions(input);
            wireDefinitions["b"] = new[] {"16076"};

            var wireSignals = CalculateSignals(wireDefinitions);

            return wireSignals[wire];
        }

        private static Dictionary<string, int> CalculateSignals(Dictionary<string, string[]> wireDefinitions)
        {
            var knownWires = new Dictionary<string, int>();

            while (wireDefinitions.Any())
            {
                foreach (var (wire, definition) in wireDefinitions)
                {
                    if (definition.Length == 1)
                    {
                        if (int.TryParse(definition[0], out var val))
                        {
                            knownWires[wire] = val;
                            wireDefinitions.Remove(wire);
                        }
                        else if (knownWires.ContainsKey(definition[0]))
                        {
                            knownWires[wire] = knownWires[definition[0]];
                            wireDefinitions.Remove(wire);
                        }

                        continue;
                    }

                    if (definition.Length == 2)
                    {
                        if (int.TryParse(definition[1], out _))
                        {
                            knownWires[wire] = GetBitwiseComplement(definition[1]);
                            wireDefinitions.Remove(wire);
                        }
                        else if (knownWires.ContainsKey(definition[1]))
                        {
                            knownWires[wire] = GetBitwiseComplement(knownWires[definition[1]].ToString());
                            wireDefinitions.Remove(wire);
                        }

                        continue;
                    }

                    if (int.TryParse(definition[0], out _) && int.TryParse(definition[2], out _))
                    {
                        knownWires[wire] = CalculateBitwiseOperation(definition[0], definition[1], definition[2]);
                        wireDefinitions.Remove(wire);
                    }
                    else if (int.TryParse(definition[0], out _) && knownWires.ContainsKey(definition[2]))
                    {
                        knownWires[wire] =
                            CalculateBitwiseOperation(definition[0], definition[1], knownWires[definition[2]].ToString());
                        wireDefinitions.Remove(wire);
                    }
                    else if (int.TryParse(definition[2], out _) && knownWires.ContainsKey(definition[0]))
                    {
                        knownWires[wire] =
                            CalculateBitwiseOperation(knownWires[definition[0]].ToString(), definition[1], definition[2]);
                        wireDefinitions.Remove(wire);
                    }
                    else if (knownWires.ContainsKey(definition[0]) && knownWires.ContainsKey(definition[2]))
                    {
                        knownWires[wire] = CalculateBitwiseOperation(knownWires[definition[0]].ToString(), definition[1],
                            knownWires[definition[2]].ToString());
                        wireDefinitions.Remove(wire);
                    }
                }
            }

            return knownWires;
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
        
        private static int GetBitwiseComplement(string value)
        {
            var twosComplement = Convert.ToString(~Convert.ToInt32(value), 2);
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