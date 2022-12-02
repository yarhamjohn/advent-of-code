using System.Security.Cryptography;

namespace AdventOfCode2016.Day17;

public static class Day17
{
    public static string GetShortestPath(string input)
    {
        ValidPaths = new List<string>();
        ShortestPath = int.MaxValue;
        
        GetPath("", input, (0, 0));

        return ValidPaths.OrderBy(x => x.Length).First();
    }

    private static List<string> ValidPaths = new();
    private static int ShortestPath = int.MaxValue;
    
    private static void GetPath(string path, string passcode, (int, int) location)
    {
        if (path.Length >= ShortestPath)
        {
            return;
        }
        
        if (location is { Item1: 3, Item2: 3 })
        {
            if (path.Length < ShortestPath)
            {
                ShortestPath = path.Length;
            }
            
            ValidPaths.Add(path);
        }

        var availableDoors = GetDoors(location);
        var doorState = GetMd5Hash(path, passcode);

        foreach (var door in availableDoors)
        {
            if (IsOpen(doorState, door))
            {
                GetPath(path + door, passcode, GetNextLocation(location, door));
            }
        }
    }

    private static (int, int) GetNextLocation((int, int) location, string door)
    {
        return door switch
        {
            "U" => (location.Item1 - 1, location.Item2),
            "D" => (location.Item1 + 1, location.Item2),
            "L" => (location.Item1, location.Item2 - 1),
            "R" => (location.Item1, location.Item2 + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(door))
        };
    }

    private static bool IsOpen(string doorState, string door)
    {
        var openChars = new[] { 'B', 'C', 'D', 'E', 'F' };
        return door switch
        {
            "U" => openChars.Contains(doorState[0]),
            "D" => openChars.Contains(doorState[1]),
            "L" => openChars.Contains(doorState[2]),
            "R" => openChars.Contains(doorState[3]),
            _ => throw new ArgumentOutOfRangeException(nameof(door))
        };
    }

    private static IEnumerable<string> GetDoors((int, int) location)
    {
        if (location.Item1 - 1 >= 0)
        {
            yield return "U";
        }

        if (location.Item1 + 1 < 4)
        {
            yield return "D";
        }

        if (location.Item2 - 1 >= 0)
        {
            yield return "L";
        }

        if (location.Item2 + 1 < 4)
        {
            yield return "R";
        }
    }

    private static string GetMd5Hash(string path, string passcode)
    {
        var md5 = MD5.HashData(System.Text.Encoding.ASCII.GetBytes($"{passcode}{path}"));
        
        return BitConverter.ToString(md5).Replace("-", "");
    }
}