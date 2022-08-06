using System.Text;

namespace AdventOfCode2015.Day8
{
    public static class Day8
    {
        public static int CalculateCharacters(string[] input)
        {
            var totalChars = GetTotalChars(input);
            var inMemoryChars = GetInMemoryChars(input);

            return totalChars - inMemoryChars;
        }
        
        public static int CalculateCharacters2(string[] input)
        {
            var totalChars = GetTotalChars(input);
            var newEncodedChars = GetNewEncodedChars(input);

            return newEncodedChars - totalChars;
        }

        private static int GetNewEncodedChars(string[] input)
        {
            var count = 0;
            foreach (var line in input)
            {
                var newLine = new StringBuilder();
                
                foreach (var ch in line)
                {
                    if (ch is '"' or '\\')
                    {
                        newLine.Append('\\');
                    }
                    newLine.Append(ch);
                }

                count += newLine.Length + 2;
            }

            return count;
        }

        private static int GetInMemoryChars(string[] input)
        {
            var count = 0;
            foreach (var line in input)
            {
                var workingLine = line.Substring(1, line.Length - 2);
                var tempCount = workingLine.Length;

                // var escapeIndexes = Enumerable.Range(0, workingLine.Length).Where(x => workingLine[x] == '\\');
                for (var i = 0; i < workingLine.Length; i++)
                {
                    if (workingLine[i] == '\\')
                    {
                        if (workingLine[i + 1] == '\\')
                        {
                            tempCount -= 1;
                            i += 1;
                        }

                        else if (workingLine[i + 1] == '"')
                        {
                            tempCount -= 1;
                            i += 1;
                        }

                        else if (workingLine[i + 1] == 'x')
                        {
                            tempCount -= 3;
                            i += 3;
                        }

                        else
                        {
                            throw new Exception($"unexpected escape char in {workingLine}");
                        }
                    }
                }

                count += tempCount;
            }

            return count;
        }

        private static int GetTotalChars(string[] input)
        {
            return input.Sum(line => line.Length);
        }
    }
}