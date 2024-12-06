namespace AdventOfCode2024.Day5;

public static class Day5
{
    public static int Part1(string[] input)
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

       var result = 0;

       foreach (var update in updates)
       {
           var isValid = true;
           for (var pageIdx = 0; pageIdx < update.Length; pageIdx++)
           {
               var page = update[pageIdx];
               var relevantRules = rules.Where(x => x.first == page && update.Contains(x.second) || x.second == page && update.Contains(x.first)).ToList();

               foreach (var rule in relevantRules)
               {
                   if (rule.first == page && update[..pageIdx].Contains(rule.second))
                   {
                       isValid = false;
                       break;
                   }
                   
                   if (rule.second == page && update[(pageIdx + 1)..].Contains(rule.first))
                   {
                       isValid = false;
                       break;
                   }
               }

               if (!isValid)
               {
                   break;
               }
           }

           if (isValid)
           {
               result += update[(update.Length - 1) / 2];
           }
       }

       return result;
    }
}