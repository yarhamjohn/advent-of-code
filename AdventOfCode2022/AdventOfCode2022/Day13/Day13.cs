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
            if (IsCorrectOrder(pairs[i].left, pairs[i].right) != State.False)
            {
                indexes += i + 1;
            }
        }
        
        return indexes;
    }

    public static long CalculateDividerIndices(string[] input)
    {
        var extendedInput = input.Append("").Append("[[2]]").Append("[[6]]");
        var pairs = GetPairs(extendedInput.ToArray());

        var orderedPackets = OrderPackets(pairs);

        var result = 1;
        for (var i = 0; i < orderedPackets.Count; i++)
        {
            if (orderedPackets[i] is List<object> { Count: 1 } x && x[0] is List<object> { Count: 1 } y && y[0] is int and (2 or 6))
            {
                result *= i + 1;
            }
        }

        return result;
    }

    private static List<object> OrderPackets(List<(List<object> left, List<object> right)> pairs)
    {
        var packets = new List<object>();
        foreach (var (left, right) in pairs)
        {
            if (!packets.Any())
            {
                if (IsCorrectOrder(left, right) == State.False)
                {
                    packets.Add(right);
                    packets.Add(left);
                }
                else
                {
                    packets.Add(left);
                    packets.Add(right);
                }
            }
            else
            {
                for (var i = 0; i < packets.Count; i++)
                {
                    if (IsCorrectOrder(left, (List<object>)packets[i]) == State.False)
                    {
                        if (i == packets.Count - 1)
                        {
                            packets.Add(left);
                            break;
                        }
                        
                        continue;
                    }

                    packets.Insert(i, left);
                    break;
                }
                
                for (var i = 0; i < packets.Count; i++)
                {
                    if (IsCorrectOrder(right, (List<object>)packets[i]) == State.False)
                    {
                        if (i == packets.Count - 1)
                        {
                            packets.Add(right);
                            break;
                        }
                        
                        continue;
                    }

                    packets.Insert(i, right);
                    break;
                }
            }
        }

        return packets;
    }

    private enum State
    {
        True,
        False,
        Equal
    }

    private static State IsCorrectOrder(List<object> left, List<object> right)
    {
        // Ran out of items on the left side
        if (!left.Any() && right.Any())
        {
            return State.True;
        }

        // Ran out of items on the right side
        if (left.Any() && !right.Any())
        {
            return State.False;
        }
        
        // Iterate over each item
        for (var i = 0; i < Math.Max(left.Count, right.Count); i++)
        {
            // Comparing two ints
            if (left[i] is int leftInt && right[i] is int rightInt)
            {
                // ints match each other
                if (leftInt == rightInt)
                {
                    // End of list reached
                    if (i + 1 == left.Count && i + 1 == right.Count)
                    {
                        return State.Equal;
                    }
                    
                    // Ran out of left list
                    if (i + 1 >= left.Count)
                    {
                        return State.True;
                    }
                    
                    // Ran out of right list
                    if (i + 1 >= right.Count)
                    {
                        return State.False;
                    }
                    
                    // Not reached end of list yet
                    continue;
                }

                // Ints didn't match so return comparison
                return leftInt < rightInt ? State.True : State.False;
            }

            var x = State.Equal;
            
            // left side is an int but right is a list
            if (left[i] is int l1 && right[i] is List<object> r1)
            {
                x = IsCorrectOrder(new List<object> { l1 }, r1);
            }
        
            // right side is an int but left is a list
            if (left[i] is List<object> l2 && right[i] is int r2)
            {
                x = IsCorrectOrder(l2, new List<object> { r2 });
            }

            // Both sides are lists
            if (left[i] is List<object> l3 && right[i] is List<object> r3)
            {
                x = IsCorrectOrder(l3, r3);
            }

            // the lists were equal
            if (x == State.Equal)
            {
                // There are no more items
                if (i + 1 == left.Count && i + 1 == right.Count)
                {
                    return State.Equal;
                }
                
                // Left side ran out
                if (i + 1 >= left.Count)
                {
                    return State.True;
                }

                // Right side ran out
                if (i + 1 >= right.Count)
                {
                    return State.False;
                }

                // Not reached the end of the list yet
                continue;
            }

            // Lists comparison returned result
            return x;
        }

        // Order matches so they are equal
        return State.Equal;
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
        return (List<object>) new Interpreter(new Lexer(p0)).Interpret();
    }


    public interface IToken
    {
        string GetType();
    }

    public class Integer : IToken
    {
        public readonly int Value;

        public Integer(int num)
        {
            Value = num;
        }

        public new string GetType() => "integer";
    }

    public class LParens : IToken
    {
        public new string GetType() => "lparens";
    }

    public class RParens : IToken
    {
        public new string GetType() => "rparens";
    }

    public class Comma : IToken
    {
        public new string GetType() => "comma";
    }

    public class Termination : IToken
    {
        public new string GetType() => "termination";
    }

    public class EmptyArray : IToken
    {
        public new string GetType() => "empty";
    }

    public class Interpreter
    {
        private readonly Lexer _lexer;
        private IToken _currentToken;
            
        public Interpreter(Lexer lexer)
        {
            _lexer = lexer;
            _currentToken = lexer.GetNextToken();
        }

        public object Interpret()
        {
            var result = new List<object>();
            if (_currentToken is LParens)
            {
                Eat(_currentToken);
                result.Add(Interpret());
                
                while (_currentToken is Comma)
                {
                    Eat(_currentToken);
                    result.Add(Interpret());
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

        private void Eat(IToken token)
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

        public IToken GetNextToken()
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