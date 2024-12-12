using System.Text;

namespace AdventOfCode2024.Day7;

public static class Day7
{
    public static long Part1(string[] input)
    {
        return Evaluate(input, ["*", "+"]);
    }

    public static long Part2(string[] input)
    {
        return Evaluate(input, ["*", "+", "||"]);
    }

    private static long Evaluate(string[] input, string[] operators)
    {
        return ParseInput(input)
            .Where(equation => IsPossiblyTrue(operators, equation))
            .Sum(equation => equation.testValue);
    }

    private static bool IsPossiblyTrue(string[] operators, (long testValue, long[] elements) equation)
    {
        var operatorCombinations = GetCombinations(
            operators, 
            equation.elements.Length - 1);

        foreach (var combination in operatorCombinations)
        {
            var runningTotal = equation.elements.First();

            for (var opIdx = 0; opIdx < combination.Length; opIdx++)
            {
                var nextElement = equation.elements[opIdx + 1];
                
                switch (combination[opIdx])
                {
                    case "*":
                        runningTotal *= nextElement;
                        break;
                    case "+":
                        runningTotal += nextElement;
                        break;
                    case "||":
                        var stringBuilder = new StringBuilder();
                        stringBuilder.Append(runningTotal);
                        stringBuilder.Append(nextElement);
                        
                        runningTotal = long.Parse(stringBuilder.ToString());
                        break;
                }
                
                if (runningTotal > equation.testValue)
                {
                    break;
                }
            }

            if (runningTotal == equation.testValue)
            {
                return true;
            }
        }

        return false;
    }

    private static IEnumerable<string[]> GetCombinations(string[] operators, int numOperatorsNeeded)
    {
        if (numOperatorsNeeded == 1)
        {
            return operators.Select(x => new[] {x});
        }

        return operators
            .SelectMany(op => GetCombinations(operators, numOperatorsNeeded - 1)
                .Select(x => new[] { op }.Concat(x).ToArray()));
    }

    private static IEnumerable<(long testValue, long[] elements)> ParseInput(string[] input)
    {
        foreach (var line in input)
        {
            var splitLine = line.Split(": ");
            
            yield return (
                testValue: long.Parse(splitLine[0]), 
                elements: splitLine[1].Split(" ").Select(long.Parse).ToArray());
        }
    }
}