using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020.Day16
{
    public static class Day16
    {
        public static long CalculateScanningErrorRate(List<string> input)
        {
            var emptyIndexes = GetIndexesOfEmptyLines(input);
            var rules = GetRules(input, emptyIndexes);
            var nearbyTickets = GetTickets(input, emptyIndexes);

            var (_, invalidTickets) = GetTicketValidities(nearbyTickets, rules);
            return invalidTickets.SelectMany(x => x.Value).Sum();
        }

        public static long CalculateDepartureFields(List<string> input, string rule)
        {
            var emptyIndexes = GetIndexesOfEmptyLines(input);
            var rules = GetRules(input, emptyIndexes);
            var nearbyTickets = GetTickets(input, emptyIndexes);
            var myTicket = GetTicket(input.Where((x, i) => i > emptyIndexes.First() + 1 && i < emptyIndexes.Last())
                .Single());

            var (validTickets, _) = GetTicketValidities(nearbyTickets, rules);
            var rulePositionsOnTicket = GetRulePositionsOnTicket(rules, validTickets);

            var targetIndexes = rulePositionsOnTicket.Where(x => x.rule.StartsWith(rule)).Select(y => y.position);
            var enumerable = myTicket.Where((_, i) => targetIndexes.Contains(i));
            return enumerable.Aggregate(1L, (a, b) => a * b);
        }

        private static List<(string rule, long position)> GetRulePositionsOnTicket(Dictionary<string, List<long>> rules, List<List<long>> validTickets)
        {
            var ruleNames = rules.Keys;
            var rulePositionsOnTicket = new List<(string rule, long position)>();
            while (rulePositionsOnTicket.Count < ruleNames.Count)
            {
                var remainingRules = rules.Where(y => !rulePositionsOnTicket.Select(x => x.rule).Contains(y.Key))
                    .ToDictionary(z => z.Key, z => z.Value);
                var possibleRulesByPosition = GetPossibleRulesByPosition(validTickets, remainingRules);
                foreach (var pos in possibleRulesByPosition)
                {
                    var possibleRules = pos.Value.Skip(1).Aggregate(pos.Value.First(), (a, b) => a.Intersect(b).ToList());
                    if (possibleRules.Count == 1)
                    {
                        rulePositionsOnTicket.Add((possibleRules.Single(), pos.Key));
                    }
                }
            }

            return rulePositionsOnTicket;
        }

        private static Dictionary<long, List<List<string>>> GetPossibleRulesByPosition(List<List<long>> validTickets, Dictionary<string, List<long>> rules)
        {
            var possibleRulesByPosition = new Dictionary<long, List<List<string>>>();
            foreach (var ticket in validTickets)
            {
                for (var i = 0; i < ticket.Count; i++)
                {
                    if (possibleRulesByPosition.TryGetValue(i, out var existingRules))
                    {
                        existingRules.Add(GetPossibleRules(ticket[i], rules).ToList());
                    }
                    else
                    {
                        possibleRulesByPosition[i] = new List<List<string>> {GetPossibleRules(ticket[i], rules).ToList()};
                    }
                }
            }

            return possibleRulesByPosition;
        }

        private static IEnumerable<string> GetPossibleRules(long pos, Dictionary<string,List<long>> rules)
        {
            foreach (var (key, value) in rules)
            {
                if (value.Contains(pos))
                {
                    yield return key;
                }
            }
        }

        private static (List<List<long>> validTickets, Dictionary<List<long>, List<long>> invalidTickets) GetTicketValidities(List<List<long>> nearbyTickets, Dictionary<string, List<long>> rules)
        {
            var validTickets = new List<List<long>>();
            var invalidTickets = new Dictionary<List<long>, List<long>>();
            foreach (var ticket in nearbyTickets)
            {
                var invalidNums = ticket.Where(num => NumIsInvalid(rules, num)).ToList();
                if (invalidNums.Any())
                {
                    invalidTickets[ticket] = invalidNums;
                }
                else
                {
                    validTickets.Add(ticket);
                }
            }

            return (validTickets, invalidTickets);
        }

        private static List<long> GetIndexesOfEmptyLines(List<string> input)
        {
            return input.Select((x, i) => (x, i)).Where(y => string.IsNullOrWhiteSpace(y.Item1)).Select(z => (long) z.Item2).ToList();
        }

        private static List<List<long>> GetTickets(List<string> input, List<long> emptyIndexes)
        {
            return input.Where((x, i) => i > emptyIndexes.Last() + 1).Select((x, i) => GetTicket(x)).ToList();
        }

        private static bool NumIsInvalid(Dictionary<string, List<long>> rules, long num)
        {
            return rules.All(rule => !rule.Value.Contains(num));
        }

        private static List<long> GetTicket(string line)
        {
            return line.Split(",").Select(x => Convert.ToInt64(x)).ToList();
        }

        private static Dictionary<string, List<long>> GetRules(IEnumerable<string> input, List<long> emptyIndexes)
        {
            var rules = input.Where((x, i) => i < emptyIndexes.First());
            var result = new Dictionary<string, List<long>>();
            foreach (var line in rules)
            {
                var regex = new Regex(
                    "^(?<rule>[a-z ]+): (?<startOne>[0-9]+)-(?<startTwo>[0-9]+) or (?<startThree>[0-9]+)-(?<startFour>[0-9]+)$");
                var match = regex.Match(line);
                var rule = match.Groups["rule"].Value;
                var startOne = Convert.ToInt32(match.Groups["startOne"].Value);
                var startTwo = Convert.ToInt32(match.Groups["startTwo"].Value);
                var startThree = Convert.ToInt32(match.Groups["startThree"].Value);
                var startFour = Convert.ToInt32(match.Groups["startFour"].Value);
                var value = Enumerable.Range(startOne, startTwo - startOne + 1).ToList();
                value.AddRange(Enumerable.Range(startThree, startFour - startThree + 1));
                result[rule] = value.Select(x => (long) x).ToList();
            }

            return result;
        }
    }
}