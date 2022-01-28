using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Day1
{
    public static class Day1
    {
        public static int CalculateTotalForTwoNumbers(IEnumerable<string> input, int target)
        {
            var numList = input.Select(x => Convert.ToInt32(x)).ToList();
            numList.Sort();

            for (var i = 0; i < numList.Count - 1; i++)
            {
                for (var j = numList.Count - 1; j > 0; j--)
                {
                    var sum = numList[i] + numList[j];
                    if (sum == target)
                    {
                        return numList[i] * numList[j];
                    } 
                    
                    if (sum < target)
                    {
                        break;
                    }
                }
            }
            
            throw new InvalidOperationException("Invalid input.");
        }        
        
        public static int CalculateTotalForThreeNumbers(IEnumerable<string> input)
        {
            var numList = input.Select(x => Convert.ToInt32(x)).ToList();
            numList.Sort();

            for (var i = 0; i < numList.Count; i++)
            {
                var testList = new List<int>(numList);
                testList.RemoveAt(i);
                try
                {
                    var total = CalculateTotalForTwoNumbers(testList.Select(x => x.ToString()), 2020 - numList[i]);
                    return total * numList[i];
                }
                catch
                {
                    if (i == numList.Count - 1)
                    {
                        throw;
                    }
                }
            }
            
            throw new InvalidOperationException("Invalid");
        }
    }
}