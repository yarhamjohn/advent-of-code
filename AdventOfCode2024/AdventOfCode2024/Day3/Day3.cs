using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day3;

public static class Day3
{
    public static int Part1(IEnumerable<string> input)
    {
        return input
            .SelectMany(line => GetGroups(line, @"mul\([0-9]+,[0-9]+\)"))
            .Sum(EvaluateMul);
    }

    public static int Part2(IEnumerable<string> input)
    {
        var result = 0;
        var active = true;

        foreach (var match in GetMatches(input))
        {
            switch (match.type)
            {
                case "do":
                    active = true;
                    break;
                case "dont":
                    active = false;
                    break;
                case "mul" when active:
                {
                    result += EvaluateMul(match.group);
                    break;
                }
            }
        }
        
        return result;
    }

    private static int EvaluateMul(Group group)
    {
        var leftOperand = int.Parse(group.Value.Split("mul(")[1].Split(",")[0]);
        var rightOperand = int.Parse(group.Value.Split(",")[1].Split(")")[0]);
        
        return leftOperand * rightOperand;
    }

    private static IOrderedEnumerable<(string type, Group group)> GetMatches(IEnumerable<string> input)
    {
        var megaLine = string.Join("", input);

        var mulGroups = GetGroups(megaLine, @"mul\([0-9]+,[0-9]+\)").Select(m => ("mul", group: m));
        var doGroups = GetGroups(megaLine, @"do\(\)").Select(m => ("do", group: m));
        var dontGroups = GetGroups(megaLine, @"don't\(\)").Select(m => ("dont", group: m));
        
        return mulGroups.Concat(doGroups).Concat(dontGroups).OrderBy(m => m.group.Index);
    }

    private static IEnumerable<Group> GetGroups(string input, string mulPattern)
    {
        return Regex.Matches(input, mulPattern).Select(m => m.Groups[0]);
    }
}