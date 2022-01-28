using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day7
    {
        public static int CountContainingBagColours(List<string> input)
        {
            var rules = GetRules(input);
            var bags = GetContainingBagColours(rules);
            return bags.Count;
        }

        public static int CountContainingBags(List<string> input)
        {
            var rules = GetRules(input);
            return CountBags(rules, "shiny gold");
        }

        private static int CountBags(Dictionary<string, List<(int, string)>> rules, string colour)
        {
            var bags = rules[colour];
            var count = 0;
            foreach (var (numBags, bagColour) in bags)
            {
                for (var i = 0; i < numBags; i++)
                {
                    count++;
                    count += CountBags(rules, bagColour);
                }
            }

            return count;
        }

        private static HashSet<string> GetContainingBagColours(Dictionary<string, List<(int, string)>> rules)
        {
            var containingBags = GetDirectlyContainingBagColours(rules);

            var allBags = containingBags.ToHashSet();
            while (containingBags.Count > 0)
            {
                foreach (var bag in containingBags.ToList())
                {
                    allBags.Add(bag);
                    containingBags.Remove(bag);
                    UpdateContainingBags(rules, bag, containingBags);
                }
            }

            return allBags;
        }

        private static HashSet<string> GetDirectlyContainingBagColours(Dictionary<string, List<(int, string)>> rules)
        {
            var containingBags = new HashSet<string>();
            UpdateContainingBags(rules, "shiny gold", containingBags);
            return containingBags;
        }

        private static Dictionary<string, List<(int, string)>> GetRules(List<string> input)
        {
            var rules = new Dictionary<string, List<(int, string)>>();
            foreach (var line in input)
            {
                var splitLine = line.Split("bags contain");
                var containingBag = splitLine[0].Trim();

                if (splitLine[1].Trim() == "no other bags.")
                {
                    rules[containingBag] = new List<(int, string)> { (0, "") };
                }
                else
                {
                    var containedBags = GetContainedBags(splitLine[1].Split(","));
                    rules[containingBag] = containedBags;
                }
            }

            return rules;
        }

        private static List<(int, string)> GetContainedBags(string[] splitLine)
        {
            var bags = new List<(int, string)>();
            foreach (var containedBag in splitLine)
            {
                var regexp = new Regex("^(?<count>[0-9]+) (?<colour>[a-z ]+) bag");
                var match = regexp.Match(containedBag.Trim());
                var count = match.Groups["count"].Value;
                var colour = match.Groups["colour"].Value;
                bags.Add((Convert.ToInt32(count), colour));
            }

            return bags;
        }

        private static void UpdateContainingBags(Dictionary<string, List<(int, string)>> rules, string bag,
            HashSet<string> containingBags)
        {
            var rulesUsingBag = rules.Where(rule => rule.Value.Select(x => x.Item2).Contains(bag));
            foreach (var (key, _) in rulesUsingBag)
            {
                containingBags.Add(key);
            }
        }
    }
}