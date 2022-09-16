using System.Text;

namespace AdventOfCode2015.Day19;

public static class Day19
{
    public static long CountDistinctMolecules(IEnumerable<string> input, string molecule)
    {
        var moleculeElements = GetMoleculeElements(molecule);
        
        var segments = input
            .Select(line => line.Split(" => "))
            .ToArray();
            
        var replacements = new Dictionary<string, List<string>>();

        foreach (var elem in segments)
        {
            if (replacements.ContainsKey(elem[0]))
            {
                replacements[elem[0]].Add(elem[1]);
            }
            else
            {
                replacements[elem[0]] = new List<string> { elem[1] };
            }
        }

        var molecules = new List<string>();

        for (var i = 0; i < moleculeElements.Length; i++)
        {
            var possibleReplacements = replacements.Where(x => x.Key == moleculeElements[i]).ToArray();
            if (possibleReplacements.Length != 1)
            {
                continue;
            }
            
            foreach (var repl in possibleReplacements.Single().Value)
            {
                
                var newMolecule = new StringBuilder();
                newMolecule.Append(string.Join("", moleculeElements[..i]));
                newMolecule.Append(repl);
                newMolecule.Append(string.Join("", moleculeElements[(i + 1)..]));
                
                molecules.Add(newMolecule.ToString());
            }
        }
        
        return molecules.Distinct().Count();
    }

    private static string[] GetMoleculeElements(string molecule)
    {
        var elements = new List<string>();

        for (var i = 0; i < molecule.Length; i++)
        {
            if (i == molecule.Length - 1)
            {
                elements.Add(molecule[i].ToString());
                continue;
            }

            if (char.IsLower(molecule[i + 1]))
            {
                elements.Add(string.Join("", molecule[i..(i + 2)]));
                i++;
            }
            else
            {
                elements.Add(molecule[i].ToString());
            }
        }

        return elements.ToArray();
    }
}