using System.Text;

namespace AdventOfCode2015.Day19;

public static class Day19
{
    public static long CountDistinctMolecules(IEnumerable<string> input, string molecule)
    {
        var replacements = input.Select(line => line.Split(" => ")).GroupBy(x => x[0]).ToArray();

        var molecules = new List<string>();

        for (var i = 0; i < molecule.Length; i++)
        {
            var matchingGroup = replacements.Single(x => x.Key == molecule[i].ToString());
            foreach (var item in matchingGroup)
            {
                
                var newMolecule = new StringBuilder();
                newMolecule.Append(molecule[..i]);
                newMolecule.Append(item[1]);
                newMolecule.Append(molecule[(i + 1)..]);
                
                molecules.Add(newMolecule.ToString());
            }
        }
        
        foreach (var group in replacements)
        {
            foreach (var item in group)
            {
                Console.WriteLine($"{group.Key}: {item[1]}");
            }
        }

        return molecules.Distinct().Count();
    }
}