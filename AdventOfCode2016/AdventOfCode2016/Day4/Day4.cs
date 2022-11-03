using System.Text.RegularExpressions;

namespace AdventOfCode2016.Day4;

public static class Day4
{
    public static int GetSectorIdSum(IEnumerable<string> input)
    {
        var regex = new Regex(@"(?<name>[a-z-]+)(?<sectorId>[0-9]+)\[(?<checksum>[a-z]+)\]");
        var rooms = input.Select(x => regex.Match(x).Groups);

        var validRoomSectorIds = new List<int>();
        foreach (var room in rooms)
        {
            if (IsValidRoom(room))
            {
                validRoomSectorIds.Add(Convert.ToInt32(room["sectorId"].Value));
            }
        }
        
        return validRoomSectorIds.Sum();
    }

    private static bool IsValidRoom(GroupCollection room)
    {
        return !room["name"].Value
            .Replace("-", "")
            .GroupBy(x => x)
            .OrderByDescending(x => x.Count())
            .ThenBy(x => x.Key)
            .Take(5)
            .Select(x => x.Key)
            .Except(room["checksum"].Value.ToCharArray())
            .Any();
    }
}