
namespace AdventOfCode2023.Day19;

public static class Day19
{
    public static long SumPartRatingsSuper(string[] input)
    {
        var blankLine = input.Select((line, idx) => (line, idx)).Single(tuple => string.IsNullOrWhiteSpace(tuple.line)).idx;
        var rules = GetRules(input[..blankLine]);
        
        // TODO: Are conditions correct?
        var conditionsToAccept = GetConditionsToAccept(rules).Order();
        // foreach (var c in conditionsToAccept)
        // {
        //     Console.WriteLine($"x: {c.x}, m: {c.m}, a: {c.a}, s: {c.s}");
        // }

        // TODO: Likely need to deduplicate (e.g. where a part would match multiple criteria)
        
        return conditionsToAccept
            .Sum(condition => 
                (condition.x.max - condition.x.min + 1) * 
                (condition.m.max - condition.m.min + 1) * 
                (condition.a.max - condition.a.min + 1) * 
                (condition.s.max - condition.s.min + 1));
    }

    private static bool Evaluate(IOrderedEnumerable<((long min, long max) x, (long min, long max) m, (long min, long max) a, (long min, long max) s)> conditionsToAccept, int x, int m, int a, int s)
    {
        foreach (var condition in conditionsToAccept)
        {
            if (x < condition.x.min || x > condition.x.max)
            {
                continue;
            }
            
            if (m < condition.m.min || m > condition.m.max)
            {
                continue;
            }
            
            if (a < condition.a.min || a > condition.a.max)
            {
                continue;
            }
            
            if (s < condition.s.min || s > condition.s.max)
            {
                continue;
            }

            return true;    
        }

        return false;
    }

    private static List<((long min, long max) x, (long min, long max) m, (long min, long max) a, (long min, long max) s)> Recurse(
        Dictionary<string,Rule> rules,
        ((long min, long max) x, (long min, long max) m, (long min, long max) a, (long min, long max) s) currentMinMax,
        string nextRule)
    {
        var result = new List<((long min, long max) x, (long min, long max) m, (long min, long max) a, (long min, long max) s)>();
        
        if (nextRule == "R")
        {
            return [];
        }
        
        var ruleToEvaluate = rules.Single(x => x.Key == nextRule);
        foreach (var step in ruleToEvaluate.Value.Steps)
        {
            var newMinMax = ruleToEvaluate.Value.GetNewMinMax(step.Key, currentMinMax);

            // Console.WriteLine($"{nextRule}: ({step.Value.result}) - " + newMinMax);
            if (step.Value.result == "A")
            {
                result.Add(newMinMax);
            }
            else
            {
                result.AddRange(Recurse(rules, newMinMax, step.Value.result));
            }
        }

        return result;
    }

    private static List<((long min, long max) x, (long min, long max) m, (long min, long max) a, (long min, long max) s)> GetConditionsToAccept(Dictionary<string,Rule> rules)
    {
        var minMax = (x: (min: 1L, max: 4000L), m: (min: 1L, max: 4000L), a: (min: 1L, max: 4000L), s: (min: 1L, max: 4000L));
        return Recurse(rules, minMax, "in");
    }
    
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
        public ((long min, long max) x, (long min, long max) m, (long min, long max) a, (long min, long max) s) GetNewMinMax(
            int stepKey,
            ((long min, long max) x, (long min, long max) m, (long min, long max) a, (long min, long max) s) currentMinMax)
        {
            for (var i = 0; i < stepKey; i++)
            {
                // apply the opposite to minMax
                var formula = Steps[i].formula!; // cannot be null since only the last step can be null which this doesn't reach
                if (formula.Source == "x")
                {
                    // if x > 2000 then we need x <= 2000 which means setting the max to 2000
                    if (formula.Symbol == ">")
                    {
                        if (formula.Value < currentMinMax.x.max)
                        {
                            currentMinMax.x.max = formula.Value;
                        }
                    }
                    
                    // if x < 2000 then we need x >= 2000 which means setting the min to 2000
                    if (formula.Symbol == "<")
                    {
                        if (formula.Value > currentMinMax.x.min)
                        {
                            currentMinMax.x.min = formula.Value;
                        }
                    }
                }
                
                if (formula.Source == "m")
                {
                    if (formula.Symbol == ">")
                    {
                        if (formula.Value < currentMinMax.m.max)
                        {
                            currentMinMax.m.max = formula.Value;
                        }
                    }
                    
                    if (formula.Symbol == "<")
                    {
                        if (formula.Value > currentMinMax.m.min)
                        {
                            currentMinMax.m.min = formula.Value;
                        }
                    }
                }
                
                if (formula.Source == "a")
                {
                    if (formula.Symbol == ">")
                    {
                        if (formula.Value < currentMinMax.a.max)
                        {
                            currentMinMax.a.max = formula.Value;
                        }
                    }
                    
                    if (formula.Symbol == "<")
                    {
                        if (formula.Value > currentMinMax.a.min)
                        {
                            currentMinMax.a.min = formula.Value;
                        }
                    }
                }
                
                if (formula.Source == "s")
                {
                    if (formula.Symbol == ">")
                    {
                        if (formula.Value < currentMinMax.s.max)
                        {
                            currentMinMax.s.max = formula.Value;
                        }
                    }
                    
                    if (formula.Symbol == "<")
                    {
                        if (formula.Value > currentMinMax.s.min)
                        {
                            currentMinMax.s.min = formula.Value;
                        }
                    }
                }
            }
            
            // apply the current step to minMax
            var finalFormula = Steps[stepKey].formula;
            if (finalFormula is null)
            {
                return currentMinMax;
            }
            
            // otherwise apply to minmax
            if (finalFormula.Source == "x")
            {
                // if x > 2000 then we need the min to be > 2000
                if (finalFormula.Symbol == ">")
                {
                    if (finalFormula.Value > currentMinMax.x.min)
                    {
                        currentMinMax.x.min = finalFormula.Value + 1;
                    }
                }
                
                // if x < 2000 then we need the max to be < 2000
                if (finalFormula.Symbol == "<")
                {
                    if (finalFormula.Value < currentMinMax.x.max)
                    {
                        currentMinMax.x.max = finalFormula.Value - 1;
                    }
                }
            }
            
            if (finalFormula.Source == "m")
            {
                if (finalFormula.Symbol == ">")
                {
                    if (finalFormula.Value > currentMinMax.m.min)
                    {
                        currentMinMax.m.min = finalFormula.Value + 1;
                    }
                }
                
                if (finalFormula.Symbol == "<")
                {
                    if (finalFormula.Value < currentMinMax.m.max)
                    {
                        currentMinMax.m.max = finalFormula.Value - 1;
                    }
                }
            }
            
            if (finalFormula.Source == "a")
            {
                if (finalFormula.Symbol == ">")
                {
                    if (finalFormula.Value > currentMinMax.a.min)
                    {
                        currentMinMax.a.min = finalFormula.Value + 1;
                    }
                }
                
                if (finalFormula.Symbol == "<")
                {
                    if (finalFormula.Value < currentMinMax.a.max)
                    {
                        currentMinMax.a.max = finalFormula.Value - 1;
                    }
                }
            }
            
            if (finalFormula.Source == "s")
            {
                if (finalFormula.Symbol == ">")
                {
                    if (finalFormula.Value > currentMinMax.s.min)
                    {
                        currentMinMax.s.min = finalFormula.Value + 1;
                    }
                }
                
                if (finalFormula.Symbol == "<")
                {
                    if (finalFormula.Value < currentMinMax.s.max)
                    {
                        currentMinMax.s.max = finalFormula.Value - 1;
                    }
                }
            }

            return currentMinMax;
        }
        
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