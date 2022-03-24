namespace AdventOfCode2015.Day7
{
    public static class Day7
    {
        public static Dictionary<string, int> WireValues;
        
        public static long GetWireASignal(string wire, string[] input)
        {
            var circuit = Parse(input);

            return circuit[wire].GetValue(circuit);
        }

        private static Dictionary<string, Operation> Parse(string[] input)
        {
            var wires = new Dictionary<string, Operation>();

            foreach (var line in input)
            {
                var splitLine = line.Split(" -> ");
                wires[splitLine[1]] = GetOperation(splitLine[0]);
            }

            return wires;
        }

        private static Operation GetOperation(string input)
        {
            var splitInput = input.Split(" ");
        
            if (input.Contains("AND"))
            {
                return new BitwiseAnd(splitInput[0], splitInput[2]);
            }

            if (input.Contains("OR"))
            {
                return new BitwiseOr(splitInput[0], splitInput[2]);
            }
        
            if (input.Contains("LSHIFT"))
            {
                return new LeftShift(splitInput[0], Convert.ToInt32(splitInput[2]));
            }
        
            if (input.Contains("RSHIFT"))
            {
                return new RightShift(splitInput[0], Convert.ToInt32(splitInput[2]));
            }
        
            if (input.Contains("NOT"))
            {
                return new BitwiseComplement(splitInput[1]);
            }

            return new Signal(splitInput[0]);
        }

        public abstract record Operation
        {
            public abstract int GetValue(Dictionary<string, Operation> circuit);
        }

        private record BitwiseAnd(string WireOrValueOne, string WireOrValueTwo) : Operation
        {
            public override int GetValue(Dictionary<string, Operation> circuit)
            {                
                var leftValue = int.TryParse(WireOrValueOne, out var valueOne) 
                    ? valueOne 
                    : circuit[WireOrValueOne].GetValue(circuit);
                var rightValue = int.TryParse(WireOrValueTwo, out var valueTwo) 
                    ? valueTwo 
                    : circuit[WireOrValueTwo].GetValue(circuit);


                return leftValue & rightValue;
            }
        }

        private record BitwiseOr(string WireOrValueOne, string WireOrValueTwo) : Operation
        {
            public override int GetValue(Dictionary<string, Operation> circuit)
            {
                var leftValue = int.TryParse(WireOrValueOne, out var valueOne) 
                    ? valueOne 
                    : circuit[WireOrValueOne].GetValue(circuit);
                var rightValue = int.TryParse(WireOrValueTwo, out var valueTwo) 
                    ? valueTwo 
                    : circuit[WireOrValueTwo].GetValue(circuit);

                return leftValue | rightValue;
            }
        }

        private record LeftShift(string Wire, int Value) : Operation
        {
            public override int GetValue(Dictionary<string, Operation> circuit)
            {
                return circuit[Wire].GetValue(circuit) << Value;
            }
        }

        private record RightShift(string Wire, int Value) : Operation
        {
            public override int GetValue(Dictionary<string, Operation> circuit)
            {
                return circuit[Wire].GetValue(circuit) >> Value;
            }
        }

        private record BitwiseComplement(string Wire) : Operation
        {
            public override int GetValue(Dictionary<string, Operation> circuit)
            {
                // using tilde (~), bitwise complement doesn't work as we need
                // the binary to be 16 digits long. For instance:
                // 3 in binary is 11 but we need it to be 0000000000000011 for
                // this operation. This can be achieved using ^ (logical exclusive OR)
                // with a mask of the maximum number (16 digits, all 1).
                return 65535 ^ circuit[Wire].GetValue(circuit);
            }
        }

        private record Signal(string WireOrValue) : Operation
        {
            public override int GetValue(Dictionary<string, Operation> circuit)
            {
                return int.TryParse(WireOrValue, out var value) 
                    ? value
                    : circuit[WireOrValue].GetValue(circuit);
            }
        }
    }
}