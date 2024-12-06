namespace AdventOfCode2024.Day5;

public static class Day5
{
    public static int Part1(string[] input)
    {
        var (rules, updates) = ParseInput(input);

        return updates
           .Where(update => !UpdateIsInvalid(update, rules))
           .Sum(update => update[(update.Length - 1) / 2]);
    }

    public static int Part2(string[] input)
    {
        var (rules, updates) = ParseInput(input);

        return updates
            .Where(update => UpdateIsInvalid(update, rules))
            .Select(update => ReorderUpdate(update, rules))
            .Sum(update => update[(update.Length - 1) / 2]);
    }
    
    private static int[] ReorderUpdate(int[] update, List<(int first, int second)> rules)
    {
        return update
            .Select(page => (page, count: GetRelevantRules(rules, page, update).Count(r => r.first == page)))
            .OrderByDescending(x => x.count)
            .Select(x => x.page)
            .ToArray();
    }

    private static (List<(int first, int second)>, List<int[]>) ParseInput(string[] input)
    {
        var rules = new List<(int first, int second)>();
        var updates = new List<int[]>();
       
        foreach (var line in input)
        {
            if (line.Contains('|'))
            {
                var ruleSplit = line.Split("|").Select(int.Parse).ToArray();
                rules.Add((ruleSplit[0], ruleSplit[1]));
            }

            if (line.Contains(','))
            {
                var updateSplit = line.Split(",").Select(int.Parse).ToArray();
                updates.Add(updateSplit);
            }
        }

        return (rules, updates);
    }

    private static bool UpdateIsInvalid(int[] update, List<(int first, int second)> rules)
    {
        return update
            .Where((page, pageIdx) => PageIsIncorrectlyPositioned(rules, page, update, pageIdx))
            .Any();
    }

    private static bool PageIsIncorrectlyPositioned(List<(int first, int second)> rules, int page, int[] update, int pageIdx)
    {
        return GetRelevantRules(rules, page, update)
            .Any(rule => RuleIsBroken(rule, page, pageIdx, update));
    }

    private static IEnumerable<(int first, int second)> GetRelevantRules(List<(int first, int second)> rules, int page, int[] update)
    {
        return rules.Where(x =>
            x.first == page && update.Contains(x.second) ||
            x.second == page && update.Contains(x.first));
    }

    private static bool RuleIsBroken((int first, int second) rule, int page, int pageIdx, int[] update)
    {
        return rule.first == page && update[..pageIdx].Contains(rule.second) || 
               rule.second == page && update[(pageIdx + 1)..].Contains(rule.first);
    }
}