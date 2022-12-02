using System.Security.Cryptography;

namespace AdventOfCode2016.Day17;

public static class Day17
{
    public static string GetShortestPath(string input)
    {
        _shortestPath = null;
        
        GetShortestPath("", input, (0, 0));

        return _shortestPath ?? string.Empty;
    }

    public static int GetLongestPath(string input)
    {
        _longestPath = int.MinValue;
        
        GetLongestPath("", input, (0, 0));

        return _longestPath;
    }
    
    private static string? _shortestPath;
    private static int _longestPath;
    
    private static void GetShortestPath(string path, string passcode, (int, int) location)
    {
        if (_shortestPath is not null && path.Length >= _shortestPath.Length)
        {
            return;
        }
        
        if (location is { Item1: 3, Item2: 3 })
        {
            if (_shortestPath is null || path.Length < _shortestPath.Length)
            {
                _shortestPath = path;
            }
        }

        var doorState = GetMd5Hash(path, passcode);
        foreach (var door in GetDoors(location))
        {
            if (IsOpen(doorState, door))
            {
                GetShortestPath(path + door, passcode, GetNextLocation(location, door));
            }
        }
    }
    
    private static void GetLongestPath(string path, string passcode, (int, int) location)
    {
        if (location is { Item1: 3, Item2: 3 })
        {
            if (path.Length > _longestPath)
            {
                _longestPath = path.Length;
            }

            return;
        }

        var doorState = GetMd5Hash(path, passcode);
        foreach (var door in GetDoors(location))
        {
            if (IsOpen(doorState, door))
            {
                GetLongestPath(path + door, passcode, GetNextLocation(location, door));
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