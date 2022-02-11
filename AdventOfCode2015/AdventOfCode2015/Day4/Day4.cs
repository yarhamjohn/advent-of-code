using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2015.Day4;

public static class Day4
{
    public static long GetLowestHashableNumber(string input, int numZeros)
    {
        var num = 1L;
        
        while (true)
        {
            var hash = GetHash(input, num);

            if (hash[..numZeros] == "0".PadLeft(numZeros, '0'))
            {
                break;
            };
            
            num++;
        }


        return num;
    }

    private static string GetHash(string input, long num)
    {
        var fullInput = Encoding.ASCII.GetBytes(input + num);
        var hash = MD5.HashData(fullInput);
        return BitConverter.ToString(hash).Replace("-","");
    }
}