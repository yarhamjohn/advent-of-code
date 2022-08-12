using System.Text;

namespace AdventOfCode2015.Day10
{
    public static class Day10
    {
        public static int GetLength(string input, int iterations)
        {
            var result = input;
            
            for (var i = 0; i < iterations; i++)
            {
                var newResult = new StringBuilder();
                
                var currentDigit = result[0];
                var currentCount = 0;
                
                for (var j = 0; j < result.Length; j++)
                {
                    if (result[j] == currentDigit)
                    {
                        currentCount += 1;
                    }
                    else
                    {
                        newResult.Append(currentCount);
                        newResult.Append(currentDigit);
                        
                        currentDigit = result[j];
                        currentCount = 1;
                    }

                    if (j == result.Length - 1)
                    {
                        newResult.Append(currentCount);
                        newResult.Append(currentDigit);
                    }
                }

                result = newResult.ToString();
            }
            
            return result.Length;
        }
    }
}