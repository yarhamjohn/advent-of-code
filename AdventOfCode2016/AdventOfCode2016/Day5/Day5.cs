using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AdventOfCode2016.Day5;

public static class Day5
{
    public static string GetPassword(string input)
    {
        var password = new List<char>();

        var seed = 0L;
        while (password.Count < 8)
        {
            var md5Hash = MD5.HashData(System.Text.Encoding.ASCII.GetBytes($"{input}{seed}"));
            var hexString = Convert.ToHexString(md5Hash);

            if (IsInterestingHash(hexString))
            {
                password.Add(hexString[5]);
            }
            
            seed++;
        }
        
        return string.Join("", password).ToLower();
    }

    public static string GetComplexPassword(string input)
    {
        var password = new char [8];

        var seed = 0L;
        while (password.Any(x => x == '\0'))
        {
            var md5Hash = MD5.HashData(System.Text.Encoding.ASCII.GetBytes($"{input}{seed}"));
            var hexString = Convert.ToHexString(md5Hash);

            if (IsInterestingHash(hexString) && IsValidPosition(hexString) && IsEmptyPosition(password, hexString))
            {
                password[hexString[5] - '0'] = hexString[6];
            }
            
            seed++;
        }
        
        return string.Join("", password).ToLower();
    }

    private static bool IsEmptyPosition(char[] password, string hexString)
    {
        return password[hexString[5] - '0'] == '\0';
    }

    private static bool IsValidPosition(string hexString)
    {
        return new[] { '0', '1', '2', '3', '4', '5', '6', '7'}.Contains(hexString[5]);
    }

    private static bool IsInterestingHash(string hexString)
    {
        return hexString[..5] == "00000";
    }
}