using System.Text;

namespace AdventOfCode2022.Day13;

public static class Day13
{
    public static long CalculateOrderedIndices(string[] input)
    {
        var pairs = GetPairs(input);

        var indexes = 0;
        for (var i = 0; i < pairs.Count; i++)
        {
            if (Thing((List<object>)pairs[i].left, (List<object>)pairs[i].right));
            {
                indexes += i + 1;
            }
        }
        
        return indexes;
    }

    private static bool Thing(List<object> left, List<object> right)
    {
        if (!left.Any() && right.Any())
        {
            return true;
        }

        if (left.Any() && !right.Any())
        {
            return false;
        }
        
        for (var i = 0; i < Math.Min(left.Count, right.Count); i++)
        {
            if (!CorrectlyOrdered((left[i], right[i])))
            {
                return false;
            }
        }
        
        return true;
    }

    public static bool CorrectlyOrdered((object left, object right) pair)
    {        
        if (pair is { left: int pairLeft, right: int pairRight })
        {
            return pairLeft <= pairRight;
        }
        
        if (pair is { left: int x, right: List<object> y })
        {
            return Thing(new List<object> {x}, y);
        }
        
        if (pair is { left: List<object> a, right: int b })
        {
            return Thing(a, new List<object> { b });
        }

        return Thing((List<object>)pair.left, (List<object>)pair.right);
    }

    private static List<(object left, object right)> GetPairs(string[] input)
    {
        var pairs = new List<(object left, object right)>();
        for (var i = 0; i < input.Length; i += 3)
        {
            pairs.Add((Parse(input[i]), Parse(input[i + 1])));
        }

        return pairs;
    }

    public static object Parse(string p0)
    {
        var lexer = new Lexer(p0);
        var interpreter = new Interpreter(lexer);
        var result = interpreter.Thing();
        return result;
    }


    public interface Token
    {
        string GetType();
    }

    public class Integer : Token
    {
        public readonly int Value;

        public Integer(int num)
        {
            Value = num;
        }

        public new string GetType() => "integer";
    }

    public class LParens : Token
    {
        public new string GetType() => "lparens";
    }

    public class RParens : Token
    {
        public new string GetType() => "rparens";
    }

    public class Comma : Token
    {
        public new string GetType() => "comma";
    }

    public class Termination : Token
    {
        public new string GetType() => "termination";
    }

    public class EmptyArray : Token
    {
        public new string GetType() => "empty";
    }

    public class Interpreter
    {
        private readonly Lexer _lexer;
        private Token _currentToken;
            
        public Interpreter(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = lexer.GetNextToken();
        }

        public object Thing()
        {
            var result = new List<object>();
            if (_currentToken is LParens)
            {
                Eat(_currentToken);
                result.Add(Thing());
                
                while (_currentToken is Comma)
                {
                    Eat(_currentToken);
                    result.Add(Thing());
                }
                
                Eat(new RParens());
            }
            else if (_currentToken is EmptyArray)
            {
                Eat(_currentToken);
                return new List<object>();
            }
            else if (_currentToken is Integer integer)
            {
                Eat(_currentToken);
                return integer.Value;
            }

            return result;
        }

        private void Eat(Token token)
        {
            if (_currentToken.GetType() == token.GetType())
            {
                _currentToken = _lexer.GetNextToken();
            }
        }
    }

    public class Lexer
    {
        private readonly string _text;
        private int _position = 0;
        
        public Lexer(string text)
        {
            _text = text;
        }

        private void Advance() => _position++;

        private Integer GetInteger()
        {
            var result = new StringBuilder();
            while (int.TryParse(_text[_position].ToString(), out var num))
            {
                result.Append(num);
                Advance();
            }

            return new Integer(Convert.ToInt32(result.ToString()));
        }

        public Token GetNextToken()
        {
            if (_position == _text.Length)
            {
                return new Termination();
            }

            switch (_text[_position])
            {
                case '[' when _text[_position + 1] != ']':
                    Advance();
                    return new LParens();
                case ']' when _text[_position - 1] != '[':
                    Advance();
                    return new RParens();
                case '[':
                    Advance();
                    Advance();
                    return new EmptyArray();
                case ',':
                    Advance();
                    return new Comma();
                default:
                    return GetInteger();
            }
        }
    }
}