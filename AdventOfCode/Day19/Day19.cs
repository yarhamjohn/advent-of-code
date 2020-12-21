using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day19
    {
        public static long CountValidMessages(List<string> input)
        {
            /*
             * Part 2 involves creating a recursive regex but .NET doesn't appear to support this.
             * In order to solve it therefore, I constructed the basic regex as for Part 1 then edited it
             * to make it recursive. I then tested the input manually on www.regex101.com to get the answer.
             *
             * The initial regex that was produced pre-editing was:
             * ^((((b(a(b((bb|(b|a)a)a|(ab|aa)b)|a((bb|(b|a)a)a|(bb)b))|b(a((ba|aa)a|(bb)b)|b(b(bb|(b|a)a)|a(b(b|a)|ab))))|a(((b(bb|aa)|a(bb|ba))a|(a(bb|aa)|b(b(b|a)|ab))b)b|(((b|a)((b|a)(b|a)))a|(b(bb|aa)|a(bb|(b|a)a))b)a))a|(a((a(((b|a)(b|a))a|(ab|aa)b)|b(b(ab)|a(bb|aa)))a|(((bb)a|(ab|aa)b)a|((ab)a|(ab|aa)b)b)b)|b((a(b(ba|ab)|a(ba))|b(b(bb)|a(b(b|a)|ab)))b|(((bb|ba)a|((b|a)a|ab)b)b|((ba|ab)a|(ab|aa)b)a)a))b)a|(a((((a((b|a)a|ab)|b(b(b|a)|ab))a|(b(ab)|a(ab))b)b|((((b|a)(b|a))a|(ab|aa)b)b|(a(bb)|b(ab))a)a)b|((((bb|ba)b|(ba)a)b|((bb|ba)b)a)a|(a(b(bb|ab)|a(ab|aa))|b((b|a)(bb|ba)))b)a)|b(a(a((a(bb)|b(ab))b|(b(ab)|a(bb|aa))a)|b(b(((b|a)(b|a))a|(bb|ab)b)|a(((b|a)a|ab)b|(ab|aa)a)))|b(b(a(a(bb|ab)|b(b(b|a)|ab))|b((ba)b))|a(a((ab|aa)a|(ba)b)|b((ab)b|(ba|ab)a)))))b)|(((b(a(b((bb|(b|a)a)a|(ab|aa)b)|a((bb|(b|a)a)a|(bb)b))|b(a((ba|aa)a|(bb)b)|b(b(bb|(b|a)a)|a(b(b|a)|ab))))|a(((b(bb|aa)|a(bb|ba))a|(a(bb|aa)|b(b(b|a)|ab))b)b|(((b|a)((b|a)(b|a)))a|(b(bb|aa)|a(bb|(b|a)a))b)a))a|(a((a(((b|a)(b|a))a|(ab|aa)b)|b(b(ab)|a(bb|aa)))a|(((bb)a|(ab|aa)b)a|((ab)a|(ab|aa)b)b)b)|b((a(b(ba|ab)|a(ba))|b(b(bb)|a(b(b|a)|ab)))b|(((bb|ba)a|((b|a)a|ab)b)b|((ba|ab)a|(ab|aa)b)a)a))b)a|(a((((a((b|a)a|ab)|b(b(b|a)|ab))a|(b(ab)|a(ab))b)b|((((b|a)(b|a))a|(ab|aa)b)b|(a(bb)|b(ab))a)a)b|((((bb|ba)b|(ba)a)b|((bb|ba)b)a)a|(a(b(bb|ab)|a(ab|aa))|b((b|a)(bb|ba)))b)a)|b(a(a((a(bb)|b(ab))b|(b(ab)|a(bb|aa))a)|b(b(((b|a)(b|a))a|(bb|ab)b)|a(((b|a)a|ab)b|(ab|aa)a)))|b(b(a(a(bb|ab)|b(b(b|a)|ab))|b((ba)b))|a(a((ab|aa)a|(ba)b)|b((ab)b|(ba|ab)a)))))b)8)((((b(a(b((bb|(b|a)a)a|(ab|aa)b)|a((bb|(b|a)a)a|(bb)b))|b(a((ba|aa)a|(bb)b)|b(b(bb|(b|a)a)|a(b(b|a)|ab))))|a(((b(bb|aa)|a(bb|ba))a|(a(bb|aa)|b(b(b|a)|ab))b)b|(((b|a)((b|a)(b|a)))a|(b(bb|aa)|a(bb|(b|a)a))b)a))a|(a((a(((b|a)(b|a))a|(ab|aa)b)|b(b(ab)|a(bb|aa)))a|(((bb)a|(ab|aa)b)a|((ab)a|(ab|aa)b)b)b)|b((a(b(ba|ab)|a(ba))|b(b(bb)|a(b(b|a)|ab)))b|(((bb|ba)a|((b|a)a|ab)b)b|((ba|ab)a|(ab|aa)b)a)a))b)a|(a((((a((b|a)a|ab)|b(b(b|a)|ab))a|(b(ab)|a(ab))b)b|((((b|a)(b|a))a|(ab|aa)b)b|(a(bb)|b(ab))a)a)b|((((bb|ba)b|(ba)a)b|((bb|ba)b)a)a|(a(b(bb|ab)|a(ab|aa))|b((b|a)(bb|ba)))b)a)|b(a(a((a(bb)|b(ab))b|(b(ab)|a(bb|aa))a)|b(b(((b|a)(b|a))a|(bb|ab)b)|a(((b|a)a|ab)b|(ab|aa)a)))|b(b(a(a(bb|ab)|b(b(b|a)|ab))|b((ba)b))|a(a((ab|aa)a|(ba)b)|b((ab)b|(ba|ab)a)))))b)(a(a(a(a(a((bb|ba)a|(ab|aa)b)|b(b(bb|ba)|a(ab)))|b((a(ba)|b(ba|aa))a|(b(ba|aa)|a((b|a)a|ab))b))|b((a(b(ba|ab)|a(bb))|b((ba)b))a|(b(b(bb|aa)|a(bb|(b|a)a))|a(((b|a)a|ab)a|(bb|aa)b))b))|b((((a(bb|aa)|b(bb|(b|a)a))a|(b(bb|ab)|a(ab|aa))b)b|(((ba|aa)(b|a))b|((ba)b|(ba|aa)a)a)a)b|((b((ab)a|(aa)b)|a(b(ba|ab)|a(bb)))a|((a((b|a)(b|a))|b(b(b|a)|ab))b|((bb|aa)b|(b(b|a)|ab)a)a)b)a))|b(b((b(((bb|ba)b|(ba)a)b|((bb|(b|a)a)b|((b|a)(b|a))a)a)|a((b(bb|aa)|a(bb|ba))a|(b(bb|(b|a)a)|a(bb|a(b|a)))b))b|((((bb|(b|a)a)a|(ba)b)b|((bb)b|(b(b|a)|ab)a)a)b|(a(b(ba|ab)|a(b(b|a)|ab))|b((ba)b|(aa)a))a)a)|a((((b(bb|ba)|a((b|a)(b|a)))a|(b(ab)|a(bb|aa))b)b|(b((ab)b|(ab)a)|a((bb|aa)b|(b(b|a)|ab)a))a)b|(b((b(bb|ab)|a(ba))a|((ab)a|(bb|aa)b)b)|a(a(b((b|a)(b|a))|a(b(b|a)|ab))|b(a(ba)|b(bb))))a)))|(((b(a(b((bb|(b|a)a)a|(ab|aa)b)|a((bb|(b|a)a)a|(bb)b))|b(a((ba|aa)a|(bb)b)|b(b(bb|(b|a)a)|a(b(b|a)|ab))))|a(((b(bb|aa)|a(bb|ba))a|(a(bb|aa)|b(b(b|a)|ab))b)b|(((b|a)((b|a)(b|a)))a|(b(bb|aa)|a(bb|(b|a)a))b)a))a|(a((a(((b|a)(b|a))a|(ab|aa)b)|b(b(ab)|a(bb|aa)))a|(((bb)a|(ab|aa)b)a|((ab)a|(ab|aa)b)b)b)|b((a(b(ba|ab)|a(ba))|b(b(bb)|a(b(b|a)|ab)))b|(((bb|ba)a|((b|a)a|ab)b)b|((ba|ab)a|(ab|aa)b)a)a))b)a|(a((((a((b|a)a|ab)|b(b(b|a)|ab))a|(b(ab)|a(ab))b)b|((((b|a)(b|a))a|(ab|aa)b)b|(a(bb)|b(ab))a)a)b|((((bb|ba)b|(ba)a)b|((bb|ba)b)a)a|(a(b(bb|ab)|a(ab|aa))|b((b|a)(bb|ba)))b)a)|b(a(a((a(bb)|b(ab))b|(b(ab)|a(bb|aa))a)|b(b(((b|a)(b|a))a|(bb|ab)b)|a(((b|a)a|ab)b|(ab|aa)a)))|b(b(a(a(bb|ab)|b(b(b|a)|ab))|b((ba)b))|a(a((ab|aa)a|(ba)b)|b((ab)b|(ba|ab)a)))))b)(b(ab)|a(bb|aa))(b(ab)|a(bb|aa))(a(a(a(a(a((bb|ba)a|(ab|aa)b)|b(b(bb|ba)|a(ab)))|b((a(ba)|b(ba|aa))a|(b(ba|aa)|a((b|a)a|ab))b))|b((a(b(ba|ab)|a(bb))|b((ba)b))a|(b(b(bb|aa)|a(bb|(b|a)a))|a(((b|a)a|ab)a|(bb|aa)b))b))|b((((a(bb|aa)|b(bb|(b|a)a))a|(b(bb|ab)|a(ab|aa))b)b|(((ba|aa)(b|a))b|((ba)b|(ba|aa)a)a)a)b|((b((ab)a|(aa)b)|a(b(ba|ab)|a(bb)))a|((a((b|a)(b|a))|b(b(b|a)|ab))b|((bb|aa)b|(b(b|a)|ab)a)a)b)a))|b(b((b(((bb|ba)b|(ba)a)b|((bb|(b|a)a)b|((b|a)(b|a))a)a)|a((b(bb|aa)|a(bb|ba))a|(b(bb|(b|a)a)|a(bb|a(b|a)))b))b|((((bb|(b|a)a)a|(ba)b)b|((bb)b|(b(b|a)|ab)a)a)b|(a(b(ba|ab)|a(b(b|a)|ab))|b((ba)b|(aa)a))a)a)|a((((b(bb|ba)|a((b|a)(b|a)))a|(b(ab)|a(bb|aa))b)b|(b((ab)b|(ab)a)|a((bb|aa)b|(b(b|a)|ab)a))a)b|(b((b(bb|ab)|a(ba))a|((ab)a|(bb|aa)b)b)|a(a(b((b|a)(b|a))|a(b(b|a)|ab))|b(a(ba)|b(bb))))a))))$
             *
             * After editing, the final regex was:
             * ^((((b(a(b((bb|(b|a)a)a|(ab|aa)b)|a((bb|(b|a)a)a|(bb)b))|b(a((ba|aa)a|(bb)b)|b(b(bb|(b|a)a)|a(b(b|a)|ab))))|a(((b(bb|aa)|a(bb|ba))a|(a(bb|aa)|b(b(b|a)|ab))b)b|(((b|a)((b|a)(b|a)))a|(b(bb|aa)|a(bb|(b|a)a))b)a))a|(a((a(((b|a)(b|a))a|(ab|aa)b)|b(b(ab)|a(bb|aa)))a|(((bb)a|(ab|aa)b)a|((ab)a|(ab|aa)b)b)b)|b((a(b(ba|ab)|a(ba))|b(b(bb)|a(b(b|a)|ab)))b|(((bb|ba)a|((b|a)a|ab)b)b|((ba|ab)a|(ab|aa)b)a)a))b)a|(a((((a((b|a)a|ab)|b(b(b|a)|ab))a|(b(ab)|a(ab))b)b|((((b|a)(b|a))a|(ab|aa)b)b|(a(bb)|b(ab))a)a)b|((((bb|ba)b|(ba)a)b|((bb|ba)b)a)a|(a(b(bb|ab)|a(ab|aa))|b((b|a)(bb|ba)))b)a)|b(a(a((a(bb)|b(ab))b|(b(ab)|a(bb|aa))a)|b(b(((b|a)(b|a))a|(bb|ab)b)|a(((b|a)a|ab)b|(ab|aa)a)))|b(b(a(a(bb|ab)|b(b(b|a)|ab))|b((ba)b))|a(a((ab|aa)a|(ba)b)|b((ab)b|(ba|ab)a)))))b)+)(?<eleven>(((b(a(b((bb|(b|a)a)a|(ab|aa)b)|a((bb|(b|a)a)a|(bb)b))|b(a((ba|aa)a|(bb)b)|b(b(bb|(b|a)a)|a(b(b|a)|ab))))|a(((b(bb|aa)|a(bb|ba))a|(a(bb|aa)|b(b(b|a)|ab))b)b|(((b|a)((b|a)(b|a)))a|(b(bb|aa)|a(bb|(b|a)a))b)a))a|(a((a(((b|a)(b|a))a|(ab|aa)b)|b(b(ab)|a(bb|aa)))a|(((bb)a|(ab|aa)b)a|((ab)a|(ab|aa)b)b)b)|b((a(b(ba|ab)|a(ba))|b(b(bb)|a(b(b|a)|ab)))b|(((bb|ba)a|((b|a)a|ab)b)b|((ba|ab)a|(ab|aa)b)a)a))b)a|(a((((a((b|a)a|ab)|b(b(b|a)|ab))a|(b(ab)|a(ab))b)b|((((b|a)(b|a))a|(ab|aa)b)b|(a(bb)|b(ab))a)a)b|((((bb|ba)b|(ba)a)b|((bb|ba)b)a)a|(a(b(bb|ab)|a(ab|aa))|b((b|a)(bb|ba)))b)a)|b(a(a((a(bb)|b(ab))b|(b(ab)|a(bb|aa))a)|b(b(((b|a)(b|a))a|(bb|ab)b)|a(((b|a)a|ab)b|(ab|aa)a)))|b(b(a(a(bb|ab)|b(b(b|a)|ab))|b((ba)b))|a(a((ab|aa)a|(ba)b)|b((ab)b|(ba|ab)a)))))b)(?&eleven)*(a(a(a(a(a((bb|ba)a|(ab|aa)b)|b(b(bb|ba)|a(ab)))|b((a(ba)|b(ba|aa))a|(b(ba|aa)|a((b|a)a|ab))b))|b((a(b(ba|ab)|a(bb))|b((ba)b))a|(b(b(bb|aa)|a(bb|(b|a)a))|a(((b|a)a|ab)a|(bb|aa)b))b))|b((((a(bb|aa)|b(bb|(b|a)a))a|(b(bb|ab)|a(ab|aa))b)b|(((ba|aa)(b|a))b|((ba)b|(ba|aa)a)a)a)b|((b((ab)a|(aa)b)|a(b(ba|ab)|a(bb)))a|((a((b|a)(b|a))|b(b(b|a)|ab))b|((bb|aa)b|(b(b|a)|ab)a)a)b)a))|b(b((b(((bb|ba)b|(ba)a)b|((bb|(b|a)a)b|((b|a)(b|a))a)a)|a((b(bb|aa)|a(bb|ba))a|(b(bb|(b|a)a)|a(bb|a(b|a)))b))b|((((bb|(b|a)a)a|(ba)b)b|((bb)b|(b(b|a)|ab)a)a)b|(a(b(ba|ab)|a(b(b|a)|ab))|b((ba)b|(aa)a))a)a)|a((((b(bb|ba)|a((b|a)(b|a)))a|(b(ab)|a(bb|aa))b)b|(b((ab)b|(ab)a)|a((bb|aa)b|(b(b|a)|ab)a))a)b|(b((b(bb|ab)|a(ba))a|((ab)a|(bb|aa)b)b)|a(a(b((b|a)(b|a))|a(b(b|a)|ab))|b(a(ba)|b(bb))))a))))$
             *
             * Essentially, element 8 was originally '42 | 42 8' which simplifies to '42 +'.
             * Element 11 was originally '42 31 | 42 11 31`. This can be handled with recursion like this:
             * (?<named_group>(42 (?&named_group)* 31))
             */
            
            var rules = GetRules(input);
            var fullRule = GetFullRule(rules);
            var regex = GetRuleZeroRegex(fullRule);
            var messages = GetMessages(input);

            return messages.Count(x => regex.IsMatch(x));
        }

        private static string GetFullRule(Dictionary<int, string> rules)
        {
            while (rules.Count > 1)
            {
                var keyToProcess = rules.Keys.Max();
                rules.TryGetValue(keyToProcess, out var ruleToProcess);
                var replacementString = " ( " + ruleToProcess + " ) ";

                foreach (var key in rules.Keys)
                {
                    rules.TryGetValue(key, out var ruleToUpdate);
                    rules[key] = ruleToUpdate!.Replace($"{keyToProcess}", replacementString);
                }

                rules.Remove(keyToProcess);
            }

            rules.TryGetValue(0, out var ruleZero);
            return ruleZero!;
        }

        private static Regex GetRuleZeroRegex(string fullRule)
            =>
                new($"^{fullRule.Replace(" ", "").Replace("\"", "").Replace("(a)", "a").Replace("(b)", "b")}$");

        private static List<string> GetMessages(List<string> input)
        {
            var splitIndex = input.FindIndex(string.IsNullOrWhiteSpace);
            return input.GetRange(splitIndex + 1, input.Count - splitIndex - 1);
        }

        private static Dictionary<int, string> GetRules(List<string> input)
        {
            var splitIndex = input.FindIndex(string.IsNullOrWhiteSpace);

            var rules = new Dictionary<int, string>();
            for (var i = 0; i < splitIndex; i++)
            {
                var splitRules = input[i].Split(":");
                var key = Convert.ToInt32(splitRules[0]);

                rules[key] = splitRules[1];
            }

            return rules;
        }
    }
}