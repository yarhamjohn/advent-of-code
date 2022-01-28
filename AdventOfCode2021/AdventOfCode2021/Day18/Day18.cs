namespace AdventOfCode2021.Day18;

public static class Day18
{
    public static long CalculateMagnitude(IEnumerable<string> input)
    {
        var lines = input.Select(Tokenize).ToList();

        var currentLine = AddAllLines(lines);

        return CalculateMagnitude(currentLine);
    }
    
    public static long CalculateBiggestMagnitude(IEnumerable<string> input)
    {
        var lines = input.Select(Tokenize).ToList();
        var combinations = new List<(List<Token> left, List<Token> right)>();

        for (var i = 0; i < lines.Count; i++)
        {
            for (var j = 0; j < lines.Count; j++)
            {
                if (i == j) continue;
                
                combinations.Add((lines[i].ToList(), lines[j].ToList()));
                combinations.Add((lines[j].ToList(), lines[i].ToList()));
            }
        }

        var currentMagnitude = 0L;
        foreach (var (left, right) in combinations)
        {
            var line = AddTwoLines(left, right);
            line = ReduceLine(line);
            
            var magnitude = CalculateMagnitude(line);
            if (magnitude > currentMagnitude)
            {
                currentMagnitude = magnitude;
            }
        }

        return currentMagnitude;
    }

    private static List<Token> AddAllLines(IList<List<Token>> lines)
    {
        var currentLine = lines.First();
        lines.RemoveAt(0);

        while (lines.Any())
        {
            var nextLine = lines.First();
            lines.RemoveAt(0);

            currentLine = AddTwoLines(currentLine, nextLine);
            currentLine = ReduceLine(currentLine);
        }

        return currentLine;
    }

    private static List<Token> ReduceLine(List<Token> line)
    {
        while (true)
        {
            if (CanExplode(line))
            {
                line = Explode(line);
            }
            else if (CanSplit(line))
            {
                line = Split(line);
            }
            else
            {
                break;
            }
        }

        return line;
    }

    private static List<Token> AddTwoLines(IEnumerable<Token> leftLine, IEnumerable<Token> rightLine)
    {
        return new List<Token> { new Open(1) }
            .Concat(leftLine
                .Select(x => x.Copy())
                .Select(y =>
                {
                    y.Level++;
                    return y;
                }))
            .Concat(new List<Token> { new Comma(1) })
            .Concat(rightLine
                .Select(x => x.Copy())
                .Select(y =>
                {
                    y.Level += 1;
                    return y;
                }))
            .Concat(new List<Token> { new Close(1) })
            .ToList();
    }

    private static long CalculateMagnitude(List<Token> currentLine)
    {
        while (currentLine.Count > 1)
        {
            var firstClosingIndex = currentLine.IndexOf(currentLine.First(x => x is Close));

            var left = ((Integer)currentLine[firstClosingIndex - 3]).Value * 3;
            var right = ((Integer)currentLine[firstClosingIndex - 1]).Value * 2;
            var newLevel = currentLine[firstClosingIndex].Level - 1;
            
            currentLine = currentLine
                .GetRange(0, firstClosingIndex - 4)
                .Concat(new List<Token> { new Integer(left + right, newLevel) })
                .Concat(currentLine.
                    GetRange(firstClosingIndex + 1, currentLine.Count - firstClosingIndex - 1))
                .ToList();
        }

        return ((Integer)currentLine.Single()).Value;
    }

    private static List<Token> Explode(List<Token> currentLine)
    {
        var numOpens = 0;
        var openIndex = 0;
        var closeIndex = 0;

        for (var i = 0; i < currentLine.Count; i++)
        {
            if (currentLine[i] is Open)
            {
                numOpens++;
                openIndex = i;
            }

            if (numOpens >= 5 && currentLine[i] is Close)
            {
                closeIndex = i;
                break;
            }

            if (currentLine[i] is Close)
            {
                numOpens--;
            }
        }

        var left = currentLine[openIndex + 1];
        var right = currentLine[closeIndex - 1];

        var nextLeft = currentLine.FindLastIndex(openIndex, x => x is Integer);
        var nextRight = currentLine.FindIndex(closeIndex, x => x is Integer);

        if (nextLeft != -1)
        {
            ((Integer)currentLine[nextLeft]).Add(((Integer)left).Value);
        }
        
        if (nextRight != -1)
        {
            ((Integer)currentLine[nextRight]).Add(((Integer)right).Value);
        }

        var newLevel = currentLine[openIndex].Level - 1;
        
        return currentLine
            .GetRange(0, openIndex)
            .Concat(new List<Token> {new Integer(0, newLevel)})
            .Concat(currentLine.GetRange(closeIndex + 1, currentLine.Count - closeIndex - 1))
            .ToList();
    }

    private static List<Token> Split(List<Token> currentLine)
    {
        var indexToSplit = currentLine.IndexOf(currentLine.First(x => x is Integer { Value: > 9 }));
        var nextLevel = currentLine[indexToSplit].Level + 1;

        var left = (int)Math.Floor(((Integer)currentLine[indexToSplit]).Value / 2.0);
        var right = (int)Math.Ceiling(((Integer)currentLine[indexToSplit]).Value / 2.0);
        
        return currentLine
            .GetRange(0, indexToSplit)
            .Concat(new List<Token>
            {
                new Open(nextLevel), 
                new Integer(left, nextLevel), 
                new Comma(nextLevel), 
                new Integer(right, nextLevel), 
                new Close(nextLevel)
            })
            .Concat(currentLine.GetRange(indexToSplit + 1, currentLine.Count - indexToSplit - 1))
            .ToList();
    }

    private static bool CanExplode(IEnumerable<Token> tokens) => tokens.Any(x => x.Level >= 5);
    
    private static bool CanSplit(IEnumerable<Token> tokens) => tokens.Any(x => x is Integer { Value: > 9 });

    private static List<Token> Tokenize(string input)
    {
        var tokens = new List<Token>();
        
        var currentNum = new List<char>();
        var currentLevel = 0;
        foreach (var element in input)
        {
            switch (element)
            {
                case '[':
                    currentLevel++;
                    tokens.Add(new Open(currentLevel));
                    break;
                case ']':
                    if (currentNum.Count > 0)
                    {
                        tokens.Add(new Integer(Convert.ToInt32(string.Join("", currentNum)), currentLevel));
                        currentNum.Clear();
                    }
                    tokens.Add(new Close(currentLevel));
                    currentLevel--;
                    break;
                case ',':
                    if (currentNum.Count > 0)
                    {
                        tokens.Add(new Integer(Convert.ToInt32(string.Join("", currentNum)), currentLevel));
                        currentNum.Clear();
                    }
                    tokens.Add(new Comma(currentLevel));
                    break;
                case '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9':
                    currentNum.Add(element);
                    break;
                default:
                    throw new InvalidOperationException($"Not a valid element: {element}");
            }
        }

        return tokens;
    }

    public abstract class Token
    {
        public abstract int Level { get; set; }
        public abstract Token Copy();
    }

    private class Open : Token
    {
        public sealed override int Level { get; set; }

        public Open(int level)
        {
            Level = level;
        }

        public override Token Copy()
        {
            return new Open(Level);
        }
    }

    private class Close : Token
    {
        public sealed override int Level { get; set; }

        public Close(int level)
        {
            Level = level;
        }
        
        public override Token Copy()
        {
            return new Close(Level);
        }
    }

    private class Comma : Token
    {
        public sealed override int Level { get; set; }

        public Comma(int level)
        {
            Level = level;
        }
        
        public override Token Copy()
        {
            return new Comma(Level);
        }
    }

    private class Integer : Token
    {
        public int Value { get; private set; }
        public sealed override int Level { get; set; }

        public Integer(int value, int level)
        {
            Value = value;
            Level = level;
        }
        
        public void Add(int value) => Value += value;
        
        public override Token Copy()
        {
            return new Integer(Value, Level);
        }
    }
}