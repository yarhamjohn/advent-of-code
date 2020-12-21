using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day18
    {
        public static long Calculate(List<string> input)
        {
            return input.Sum(GetTotal);
        }
        
        public static long CalculateWithPrecedence(List<string> input)
        {
            return input.Sum(GetTotalWithPrecedence);
        }

        private static long GetTotal(string line)
        {
            var calculation = line.Select(x => x.ToString()).Where(x => x != " ").ToList();

            while (calculation.Count > 1)
            {
                UpdateCalculation(calculation);
            }

            return Convert.ToInt64(calculation.Single());
        }
        
        private static long GetTotalWithPrecedence(string line)
        {
            var calculation = line.Select(x => x.ToString()).Where(x => x != " ").ToList();

            while (calculation.Count > 1)
            {
                UpdateCalculationWithPrecedence(calculation);
            }

            return Convert.ToInt64(calculation.Single());
        }

        private static void UpdateCalculation(List<string> calculation)
        {
            var index = GetOperatorIndex(calculation);
            var result = EvaluateCalculation(calculation, index);
            ReplaceSubCalculationWithResult(calculation, index, result);
            RemoveSingleNumberParentheses(calculation, index);
        }
        
        private static void UpdateCalculationWithPrecedence(List<string> calculation)
        {
            var index = GetOperatorIndexWithPrecedence(calculation);
            var result = EvaluateCalculation(calculation, index);
            ReplaceSubCalculationWithResult(calculation, index, result);
            RemoveSingleNumberParentheses(calculation, index);
        }

        private static long EvaluateCalculation(List<string> calculation, int index)
        {
            var leftSide = Convert.ToInt64(calculation[index - 1]);
            var rightSide = Convert.ToInt64(calculation[index + 1]);
            var result = calculation[index] == "+" ? leftSide + rightSide : leftSide * rightSide;
            return result;
        }

        private static void ReplaceSubCalculationWithResult(List<string> calculation, int index, long result)
        {
            calculation.RemoveAt(index + 1);
            calculation.RemoveAt(index);
            calculation.RemoveAt(index - 1);
            calculation.Insert(index - 1, result.ToString());
        }

        private static void RemoveSingleNumberParentheses(List<string> calculation, int index)
        {
            if (index >= 2 && calculation[index - 2] == "(" && calculation[index] == ")")
            {
                calculation.RemoveAt(index);
                calculation.RemoveAt(index - 2);
            }
        }

        private static int GetOperatorIndex(List<string> calculation)
        {
            for (var i = 0; i < calculation.Count; i++)
            {
                if (calculation[i] == "+" || calculation[i] == "*")
                {
                    if (calculation[i + 1] != "(")
                    {
                        return i;
                    }
                }
            }

            throw new Exception($"Shouldn't be here. Invalid calculation: {calculation}");
        }

        private static int GetOperatorIndexWithPrecedence(List<string> calculation)
        {
            for (var i = 0; i < calculation.Count; i++)
            {
                if (calculation[i] == "+")
                {
                    if (calculation[i + 1] != "(")
                    {
                        return i;
                    }
                }

                if (calculation[i] == "*")
                {
                    if (i == calculation.Count - 2 || calculation[i + 2] == "*" || calculation[i + 2] == ")")
                    {
                        return i;
                    }
                }
            }

            throw new Exception($"Shouldn't be here. Invalid calculation: {calculation}");
        }
    }
}