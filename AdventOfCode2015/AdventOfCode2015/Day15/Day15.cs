namespace AdventOfCode2015.Day15;

public static class Day15
{
    public static long GetHighestScoringCookie(IEnumerable<string> input) =>
        CalculateTotals(input.Select(ParseIngredient));

    private static Ingredient ParseIngredient(string input)
    {
        var segments = input.Replace(",", "").Split(":").SelectMany(x => x.Split(" ")).ToArray();

        return new Ingredient(
            segments[0],
            Convert.ToInt32(segments[3]),
            Convert.ToInt32(segments[5]),
            Convert.ToInt32(segments[7]),
            Convert.ToInt32(segments[9]),
            Convert.ToInt32(segments[11]));
    }

    private static long CalculateTotals(IEnumerable<Ingredient> ingredients) =>
        GetPossibleIngredientArrangements(ingredients.ToArray())
            .Select(y =>
                CalculateTotalCapacity(y) *
                CalculateTotalDurability(y) *
                CalculateTotalFlavor(y) *
                CalculateTotalTexture(y))
            .Max();

    private static IEnumerable<Dictionary<Ingredient, int>> GetPossibleIngredientArrangements(Ingredient[] ingredients)
    {
        var possibleQuantities = Enumerable.Range(0, 101);
        var numIngredients = ingredients.Count();

        var combinations = FindCombinations(possibleQuantities, numIngredients)
            .Where(x => x.Sum() == 100);

        return combinations.Select(x =>
        {
            var result = new Dictionary<Ingredient, int>();
            
            for (var i = 0; i < numIngredients; i++)
            {
                result[ingredients[i]] = x[i];
            }
            
            return result;
        });
    }

    private static IEnumerable<int[]> FindCombinations(IEnumerable<int> possibleQuantities, int numIngredients)
    {
        if (numIngredients == 1)
        {
            return possibleQuantities.Select(x => new[] { x });
        }

        return FindCombinations(possibleQuantities, numIngredients - 1)
            .SelectMany(x => possibleQuantities.Where(y => !x.Contains(y)),
                (arr, num) => arr.Concat(new[] { num }).ToArray());
    }

    private static long CalculateTotalCapacity(Dictionary<Ingredient, int> ingredientQuantities) =>
        Math.Max(0, ingredientQuantities.Sum(x => x.Key.Capacity * x.Value));


    private static long CalculateTotalDurability(Dictionary<Ingredient, int> ingredientQuantities) =>
        Math.Max(0, ingredientQuantities.Sum(x => x.Key.Durability * x.Value));


    private static long CalculateTotalFlavor(Dictionary<Ingredient, int> ingredientQuantities) =>
        Math.Max(0, ingredientQuantities.Sum(x => x.Key.Flavor * x.Value));


    private static long CalculateTotalTexture(Dictionary<Ingredient, int> ingredientQuantities) =>
        Math.Max(0, ingredientQuantities.Sum(x => x.Key.Texture * x.Value));

    private record Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);
}