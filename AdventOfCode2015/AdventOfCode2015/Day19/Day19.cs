using System.Text;

namespace AdventOfCode2015.Day19;

public static class Day19
{
    public static long GetFewestSteps(IEnumerable<string> input, string molecule)
    {
        var segments = ParseInput(input);
        var middleDictionary = new Dictionary<string, string>();
        var endDictionary = new Dictionary<string, string>();
        
        foreach (var segment in segments)
        {
            if (segment[0] == "e")
            {
                endDictionary[segment[1]] = segment[0];
            }
            else
            {
                middleDictionary[segment[1]] = segment[0];
            }
        }

        var result = Recurse(molecule, middleDictionary, endDictionary, new List<string>());
        return result.Min(x => x.Count);
    }

    private static IEnumerable<string> GetPreviousMolecules(string molecule, Dictionary<string, string> middleDictionary)
    {
        foreach (var (key, value) in middleDictionary.Where(x => molecule.Contains(x.Key)))
        {
            for (var i = molecule.IndexOf(key, StringComparison.Ordinal); i <= molecule.LastIndexOf(key, StringComparison.Ordinal); i++)
            {
                if (molecule[i..(i + key.Length)] == key)
                {
                    yield return molecule[..i] + value + molecule[(i + key.Length)..];
                }
            }
        }
    }

    private static List<List<string>> Recurse(string molecule, Dictionary<string, string> middleDictionary, Dictionary<string, string> endDictionary, List<string> moleculeHistory)
    {
        moleculeHistory.Add(molecule);
            
        if (endDictionary.ContainsKey(molecule))
        {
            Console.WriteLine(moleculeHistory.Count);
            
            return new List<List<string>> { moleculeHistory };
        }

        var previousMolecules = GetPreviousMolecules(molecule, middleDictionary).Distinct();
        var result = previousMolecules.SelectMany(x => Recurse(x, middleDictionary, endDictionary, new List<string>(moleculeHistory))).ToList();

        return result;
    }

    public static long CountDistinctMolecules(IEnumerable<string> input, string molecule)
    {
        var moleculeElements = GetMoleculeElements(molecule);
        
        var segments = ParseInput(input);
            
        var replacements = GetAllReplacements(segments, moleculeElements);

        var molecules = GetReplacementMolecules(moleculeElements, replacements);

        foreach (var m in molecules) Console.WriteLine(m);
        return molecules.Distinct().Count();
    }

    private static string[][] ParseInput(IEnumerable<string> input)
    {
        return input
            .Select(line => line.Split(" => "))
            .ToArray();
    }

    private static Dictionary<string, List<string>> GetAllReplacements(string[][] segments, string[] moleculeElements)
    {
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

        foreach (var element in moleculeElements)
        {
            if (!replacements.ContainsKey(element))
            {
                replacements[element] = new List<string>();
            }
        }

        return replacements;
    }

    private static List<string> GetReplacementMolecules(string[] moleculeElements, Dictionary<string, List<string>> replacements)
    {
        var molecules = new List<string>();

        for (var i = 0; i < moleculeElements.Length; i++)
        {
            var possibleReplacements = replacements.Where(x => x.Key == moleculeElements[i]).ToArray();
            if (possibleReplacements.Length != 1)
            {
                continue;
            }

            molecules.AddRange(possibleReplacements.Single().Value
                .Select(replacement => BuildNewMolecule(moleculeElements, i, replacement)));
        }

        return molecules;
    }

    private static string BuildNewMolecule(string[] moleculeElements, int indexToReplace, string replacement) =>
        string.Join("", moleculeElements[..indexToReplace]
            .Concat(new[] {replacement})
            .Concat(moleculeElements[(indexToReplace + 1)..]));

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