using System.Text;

namespace AdventOfCode2015.Day19;

public static class Day19
{
    public static long CountDistinctMolecules(IEnumerable<string> input, string molecule)
    {
        var moleculeElements = GetMoleculeElements(molecule);
        
        var segments = ParseInput(input);
            
        var replacements = GetAllReplacements(segments);

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

    private static Dictionary<string, List<string>> GetAllReplacements(string[][] segments)
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
                .Select(replacement => string.Join("", BuildNewMolecule(moleculeElements, i, replacement))));
        }

        return molecules;
    }

    private static IEnumerable<string> BuildNewMolecule(string[] moleculeElements, int indexToReplace, string replacement) =>
        moleculeElements[..indexToReplace]
            .Concat(new[] {replacement})
            .Concat(moleculeElements[(indexToReplace + 1)..]);

    public static long GetFewestSteps(IEnumerable<string> input, string molecule)
    {
        var segments = ParseInput(input);

        var replacements = GetAllReplacements(segments);

        var startElectrons = segments.Where(s => s[0] == "e");

        return startElectrons
            .Select(start => GetSteps(start[1], replacements, molecule))
            .Prepend(int.MaxValue)
            .Min();
    }

    private static int GetSteps(string startMolecule, Dictionary<string, List<string>> replacements, string targetMolecule)
    {
        var steps = 1;

        var molecules = new List<string> { startMolecule};

        while (true)
        {
            Console.WriteLine(steps);
            Console.WriteLine(molecules.Count);
            
            // foreach (var m in molecules) Console.WriteLine(m);
            
            molecules = molecules.SelectMany(x => GetReplacementMolecules(GetMoleculeElements(x), replacements)).ToList();
            // Console.WriteLine("----");

            // foreach (var m in molecules) Console.WriteLine(m);
            Console.WriteLine(molecules.Count);
            // Console.WriteLine("----");

            
            // TODO Probably need to search deep via recursion rather than broad here
                molecules = molecules
                .Where(x => GetMoleculeElements(x).Last() != "Ar" && GetMoleculeElements(x).Last() != "Y" && GetMoleculeElements(x).Last() != "C" && GetMoleculeElements(x).Last() != "Rn")
                .Where(x => 
                    !x.Contains("YY") && 
                    !x.Contains("YAr") && 
                    !x.Contains("ArY") && 
                    !x.Contains("RnRn") && 
                    !x.Contains("RnY") && 
                    !x.Contains("YRn") && 
                    !x.Contains("ArRn") && 
                    !x.Contains("RnAr") && 
                    !x.Contains("CAr") && 
                    !x.Contains("CC") && 
                    !x.Contains("CY") &&
                    !x.Contains("ArCRn") &&
                    !x.Contains("RnCRn")) 
                // ArAr is possible
                // YC is possible
                // CRn not at front
                // ArCa RnCa is possible but not ArC or RnC
                .Distinct()
                .ToList();
                
                // foreach (var m in molecules) Console.WriteLine(m);

                Console.WriteLine(molecules.Count);
                Console.WriteLine("----------------------------------------");

            if (molecules.Contains(targetMolecule))
            {
                return ++steps;
            }

            if (molecules.All(x => x.Length >= targetMolecule.Length))
            {
                return int.MaxValue;
            }
            
            steps++;

            // if (steps == 3) break;
        }

        return steps;
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