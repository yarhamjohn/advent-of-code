using System.Security.Cryptography;
    
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

            if (hexString[..5] == "00000")
            {
                password.Add(hexString[5]);
            }
            
            seed++;
        }
        
        return string.Join("", password).ToLower();
    }
}