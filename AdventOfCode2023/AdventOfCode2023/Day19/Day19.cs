
namespace AdventOfCode2023.Day19;

public static class Day19
{
    public static long SumPartRatings(string[] input)
    {
        var blankLine = input.Select((line, idx) => (line, idx)).Single(tuple => string.IsNullOrWhiteSpace(tuple.line)).idx;
        var rules = GetRules(input[..blankLine]);
        var parts = GetParts(input[(blankLine + 1)..]);

        var acceptedParts = new List<Part>();
        foreach (var part in parts)
        {
            var nextRule = "in";
            while (true)
            {
                nextRule = part.ApplyRule(rules[nextRule]);

                if (nextRule == "A")
                {
                    acceptedParts.Add(part);
                    break;
                }

                if (nextRule == "R")
                {
                    break;
                }
            }
        }
        
        return acceptedParts.Sum(part => part.Sum());
    }

    private static IEnumerable<Part> GetParts(IEnumerable<string> input)
    {
        return input
            .Select(line => line
                .Replace("{", "")
                .Replace("}", "")
                .Split(",")
                .Select(x => (x.Split("=")[0], int.Parse(x.Split("=")[1]))).ToArray())
            .Select(splitLine => new Part(
                splitLine[0].Item2, 
                splitLine[1].Item2, 
                splitLine[2].Item2, 
                splitLine[3].Item2));
    }

    private static Dictionary<string, Rule> GetRules(string[] input)
    {
        var rules = new Dictionary<string, Rule>();
        foreach (var line in input)
        {
            var name = line.Split("{")[0];
            var steps = new Dictionary<int, (Formula? formula, string result)>();
            
            var formulae = line.Split("{")[1].Replace("}", "").Split(",");
            for (var i = 0; i < formulae.Length; i++)
            {
                if (!formulae[i].Contains(":"))
                {
                    steps.Add(i, (null, formulae[i]));
                    break;
                }
                
                var splitFormula = formulae[i].Split(":");
                string[] splitCalculation;
                string symbol;
                
                if (splitFormula[0].Contains(">"))
                {
                    splitCalculation = splitFormula[0].Split(">");
                    symbol = ">";
                }
                else
                {
                    splitCalculation = splitFormula[0].Split("<");
                    symbol = "<";
                }
                
                var result = splitFormula[1];
                steps.Add(i, (new Formula(splitCalculation[0], symbol, int.Parse(splitCalculation[1])), result));
            }
            
            rules.Add(name, new Rule(name, steps));
        }

        return rules;
    }

    private record Rule(string Name, Dictionary<int, (Formula? formula, string result)> Steps)
    {
        public string Evaluate(int x, int m, int a, int s)
        {
            foreach (var step in Steps.Keys.Order())
            {
                var expectation = Steps[step];
                if (expectation.formula is null)
                {
                    return expectation.result;
                }
                
                if (expectation.formula.Source == "x" && expectation.formula.Evaluate(x))
                {
                    return expectation.result;
                }
                
                if (expectation.formula.Source == "m" && expectation.formula.Evaluate(m))
                {
                    return expectation.result;
                }
                
                if (expectation.formula.Source == "a" && expectation.formula.Evaluate(a))
                {
                    return expectation.result;
                }
                
                if (expectation.formula.Source == "s" && expectation.formula.Evaluate(s))
                {
                    return expectation.result;
                }
            }
            
            throw new ArgumentException("Ran out of steps");
        }
    }

    private record Part(int X, int M, int A, int S)
    {
        public string ApplyRule(Rule rule)
        {
            return rule.Evaluate(X, M, A, S);
        }
        
        public long Sum() => X + M + A + S;
    }

    private record Formula(string Source, string Symbol, int Value)
    {
        public bool Evaluate(int input)
        {
            return Symbol switch
            {
                ">" => input > Value,
                "<" => input < Value,
                _ => throw new ArgumentException("Unexpected symbol")
            };
        }
    }
}