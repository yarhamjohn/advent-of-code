namespace AdventOfCode2021.Day10
{
    public static class Day10
    {
        private static readonly Dictionary<char, int> CorruptScoreMap = new()
            { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        
        private static readonly Dictionary<char, int> CompletionScoreMap = new()
            { { ')', 1 }, { ']', 2 }, { '}', 3 }, { '>', 4 } };

        private static readonly char[] OpenChars = { '(', '[', '{', '<' };
        
        public static int CalculateSyntaxErrorScore(IEnumerable<string> input)
        {
            var stack = new Stack<Chunk>();

            var score = 0;
            foreach (var line in input)
            {
                var chars = line.ToArray();
                for (var i = 0; i < chars.Length; i++)
                {
                    if (OpenChars.Contains(chars[i]))
                    {
                        stack.Push(new Chunk((chars[i], i)));
                    }
                    else
                    {
                        var chunk = stack.Pop();
                        chunk.Close((chars[i], i));

                        if (chunk.IsCorrupt())
                        {
                            score += CorruptScoreMap[chars[i]];
                            break;
                        }
                    }
                }
            }
            
            return score;
        }
        
        public static long CalculateMiddleCompletionScore(IEnumerable<string> input)
        {
            var scores = new List<long>();
            foreach (var line in input)
            {
                var stack = new Stack<Chunk>();

                var isCorrupt = false;
                
                var chars = line.ToArray();
                for (var i = 0; i < chars.Length; i++)
                {
                    if (OpenChars.Contains(chars[i]))
                    {
                        stack.Push(new Chunk((chars[i], i)));
                    }
                    else
                    {
                        var chunk = stack.Pop();
                        chunk.Close((chars[i], i));

                        if (chunk.IsCorrupt())
                        {
                            stack.Clear();
                            isCorrupt = true;
                            break;
                        }
                    }
                }

                var score = 0L;
                foreach (var chunk in stack)
                {
                    score *= 5;
                    score += CompletionScoreMap[chunk.GetClosingChar()];
                }

                if (!isCorrupt)
                {
                    scores.Add(score);
                }
            }

            scores.Sort();
            return scores.Skip((scores.Count - 1) / 2).Take(1).Single();
        }
    }

    public class Chunk
    {
        private readonly (char Type, int Position) _open;
        private (char Type, int Position) _close;

        private static readonly Dictionary<char, char> CharPairs = new()
            { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };

        public Chunk((char Type, int Position) open)
        {
            _open = open;
        }

        public void Close((char Type, int Position) close) => _close = close;

        public bool IsCorrupt() => CharPairs[_open.Type] != _close.Type;

        public char GetClosingChar() => CharPairs[_open.Type];
    }
}
