using System.Text;

namespace AdventOfCode2015.Day19;

public static class Day19
{
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

    public static long GetFewestSteps(IEnumerable<string> input, string molecule)
    {
        numStepsTaken = int.MaxValue;
        rejected = new List<string>();
        counter = 0;
        
        var moleculeElements = GetMoleculeElements(molecule);

        var segments = ParseInput(input);

        var replacements = GetAllReplacements(segments, moleculeElements);

        var startElectrons = segments.Where(s => s[0] == "e");

        foreach (var electron in startElectrons)
        {
            Something(GetMoleculeElements(electron[1]), 0, replacements, GetMoleculeElements(molecule), 1);
        }

        Console.WriteLine($"counter: {counter}");
        return numStepsTaken;
    }

    private static int numStepsTaken;

    private static List<string> rejected;

    private static int counter;
    
    private static bool Something(string[] inputElements, int index,
        IReadOnlyDictionary<string, List<string>> replacements, string[] targetMoleculeElements, int step)
    {
        counter++;
      
        if (step == numStepsTaken) return false;


        if (rejected.Contains(string.Join("", inputElements)))
        {
            return false;
        }
        
        if (inputElements.Length > targetMoleculeElements.Length)
        {
            rejected.Add(string.Join("", inputElements));

            return false;
        }

        if (inputElements.Length == targetMoleculeElements.Length)
        {
            if (inputElements.SequenceEqual(targetMoleculeElements))
            {
                numStepsTaken = Math.Min(step, numStepsTaken);
                // Console.WriteLine(numStepsTaken);

                return true;
            }

            rejected.Add(string.Join("", inputElements));

            return false;
        }

        var anyResultFound = false;
        
        for (var i = index; i < inputElements.Length; i++)
        {
            var possibleReplacements = replacements[inputElements[i]];

            foreach (var repl in possibleReplacements)
            {
                var replacementMolecule = BuildNewMolecule(inputElements, i, repl);

                var resultFound = Something(GetMoleculeElements(replacementMolecule), 0, replacements, targetMoleculeElements, step + 1);

                if (resultFound)
                {
                    anyResultFound = resultFound;
                }
            }
        }

        if (!anyResultFound)
        {
            rejected.Add(string.Join("", inputElements));
        }

        return anyResultFound;
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