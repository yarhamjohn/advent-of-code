using System.Text.RegularExpressions;

namespace AdventOfCode2016.Day4;

public static class Day4
{
    public static int GetRoomSectorId(IEnumerable<string> input, string roomClue)
    {
        var regex = new Regex(@"(?<name>[a-z-]+)(?<sectorId>[0-9]+)\[(?<checksum>[a-z]+)\]");
        var rooms = input.Select(x => regex.Match(x).Groups);

        var roomNames = new Dictionary<string, int>();
        foreach (var room in rooms)
        {
            var sectorId = Convert.ToInt32(room["sectorId"].Value);

            var result = new List<char>();

            foreach (var ch in room["name"].Value)
            {
                if (ch == '-')
                {
                    result.Add(' ');
                }
                else
                {
                    var newCharNum = ch + sectorId % 26;
                    result.Add(newCharNum > 122 ? (char)(newCharNum - 26) : (char)newCharNum);
                }
            } 
            
            roomNames.Add(string.Join("", result), sectorId);
        }

        return roomNames.Single(name => name.Key.Contains(roomClue)).Value;
    }
    
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