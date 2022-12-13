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

    public static bool CorrectlyOrdered((object[] left, object[] right) pair)
    {
        
        
        return true;
    }

    private static List<(object[] left, object[] right)> GetPairs(string[] input)
    {
        var pairs = new List<(object[] left, object[] right)>();
        for (var i = 0; i < input.Length; i += 3)
        {
            pairs.Add((Parse(input[i]), Parse(input[i + 1])));
        }

        return pairs;
    }

    public static object[] Parse(string p0)
    {
        var lexer = new Lexer(p0);
        var interpreter = new Interpreter(lexer);
        var result = interpreter.Thing2();
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

    public class Interpreter
    {
        private readonly Lexer _lexer;
        private Token _currentToken;
            
        public Interpreter(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = lexer.GetNextToken();
        }

        private object[] Thing()
        {
            var result = Array.Empty<object>();
            if (_currentToken is LParens)
            {
                Eat(_currentToken);
                result = Thing2();
                Eat(new RParens());
            } else if (_currentToken is Integer integer)
            {
                Eat(_currentToken);
                result = new object[] { integer.Value };
            }

            return result;
        }

        public object[] Thing2()
        {
            var result = Thing();

            while (_currentToken is Comma)
            {
                Eat(_currentToken);
                result = result.Concat(Thing()).ToArray();
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
                case '[':
                    Advance();
                    return new LParens();
                case ']':
                    Advance();
                    return new RParens();
                case ',':
                    Advance();
                    return new Comma();
                default:
                    return GetInteger();
            }
        }
    }
}