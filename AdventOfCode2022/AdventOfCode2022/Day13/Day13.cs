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
            if (CorrectlyOrdered(pairs[i]));
            {
                indexes += i;
            }
        }
        
        return indexes;
    }

    public static bool CorrectlyOrdered((List<object> left, List<object> right) pair)
    {
        
        return true;
    }

    private static List<(List<object> left, List<object> right)> GetPairs(string[] input)
    {
        var pairs = new List<(List<object> left, List<object> right)>();
        for (var i = 0; i < input.Length; i += 3)
        {
            pairs.Add((Parse(input[i]), Parse(input[i + 1])));
        }

        return pairs;
    }

    public static List<object> Parse(string p0)
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

        public List<object> Thing()
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

                // if (result.Count == 1 && ((List<object>)result.Single()).Count == 1)
                // {
                //     if (((List<object>)result.Single()).Single() is int)
                //     {
                //         result = (List<object>)result.Single();
                //     }
                // }
                
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
                return new List<object> {integer.Value};
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