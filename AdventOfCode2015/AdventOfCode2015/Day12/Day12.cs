using Newtonsoft.Json.Linq;

namespace AdventOfCode2015.Day12;

public static class Day12
{
    public static int GetSum(string input) =>
        GetAllValues(JToken.Parse(input))
            .Select(x => x.ToString())
            .Where(y => int.TryParse(y, out _))
            .Sum(Convert.ToInt32);
    
    private static IEnumerable<JValue> GetAllValues(JToken json)=>
        json switch
        {
            JValue jValue => new[] { jValue },
            JArray jArray => jArray.SelectMany(GetAllValues),
            JObject { Count: 0 } => Array.Empty<JValue>(),
            JObject jObject => jObject.PropertyValues().SelectMany(GetAllValues),
            _ => throw new InvalidOperationException("Something went wrong")
        };
    
    public static int GetSumWithoutRed(string input) =>
        GetAllValuesWithoutRed(JToken.Parse(input))
            .Select(x => x.ToString())
            .Where(y => int.TryParse(y, out _))
            .Sum(Convert.ToInt32);

    private static IEnumerable<JValue> GetAllValuesWithoutRed(JToken json)=>
        json switch
        {
            JValue jValue => new[] { jValue },
            JArray jArray => jArray.SelectMany(GetAllValuesWithoutRed),
            JObject { Count: 0 } => Array.Empty<JValue>(),
            JObject jObject => jObject.PropertyValues().Contains("red") 
                ? Array.Empty<JValue>()
                : jObject.PropertyValues().SelectMany(GetAllValuesWithoutRed),
            _ => throw new InvalidOperationException("Something went wrong")
        };
}