namespace AdventOfCode2023.Day15;

public static class Day15
{
    public static long SumHashAlgorithm(string input)
    {
        return input.Split(",").Sum(CalculateHash);
    }

    public static long CalculateFocusingPower(string input)
    {
        var boxes = Enumerable.Range(0, 256).ToDictionary(x => (long) x, _ => new List<Lens>());
        foreach (var step in input.Split(","))
        {
            var lensOp = step.Contains('=') ? "add" : "remove";
            var label = lensOp == "add" ? step.Split("=")[0] : step.Split("-")[0];
            var box = CalculateHash(label);

            if (lensOp == "add")
            {
                var focalLength = int.Parse(step.Split("=")[1]);

                var found = false;
                foreach (var lens in boxes[box].Where(lens => lens.Label == label))
                {
                    lens.Power = focalLength;
                    found = true;
                    break;
                }

                if (!found)
                {
                    boxes[box].Add(new Lens(label, focalLength));
                }
            }
            else
            {
                var lensesInBox = boxes[box];
                for (var i = 0; i < lensesInBox.Count; i++)
                {
                    if (lensesInBox[i].Label == label)
                    {
                        lensesInBox.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        
        return CalculateResult(boxes);
    }

    private static long CalculateHash(string step)
    {
        var currentVal = 0L;

        foreach (var ch in step)
        {
            currentVal += ch;
            currentVal *= 17;
            currentVal %= 256;
        }

        return currentVal;
    }

    private static long CalculateResult(Dictionary<long, List<Lens>> boxes)
    {
        var result = 0L;
        foreach (var (box, lenses) in boxes)
        {
            result += lenses.Select((t, slot) => (box + 1) * (slot + 1) * t.Power).Sum();
        }

        return result;
    }

    private class Lens(string label, int power)
    {
        public readonly string Label = label;
        public int Power = power;
    }
}