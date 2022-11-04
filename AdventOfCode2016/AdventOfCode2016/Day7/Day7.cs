namespace AdventOfCode2016.Day7;

public static class Day7
{
    public static long CountSslIps(string[] input)
    {
        var possibleAbaBabPairs = GetPossibleAbaBabPairs();
        var ips = SplitIps(input);

        var counter = 0;
        foreach (var ip in ips)
        {
            foreach (var pair in possibleAbaBabPairs)
            {
                if (ip.supernet.Any(x => x.Contains(pair.aba)) && ip.hypernet.Any(x => x.Contains(pair.bab)))
                {
                    counter++;
                    break;
                }
                
                if (ip.supernet.Any(x => x.Contains(pair.bab)) && ip.hypernet.Any(x => x.Contains(pair.aba)))
                {
                    counter++;
                    break;
                }
            }
        }
        
        return counter;
    }

    private static List<(string aba, string bab)> GetPossibleAbaBabPairs()
    {
        var possibleChars = Enumerable.Range(97, 26).Select(x => (char) x).ToArray();

        return possibleChars
            .SelectMany(ch => possibleChars.Select(ch2 => (First: ch, Second: ch2)))
            .Where(y => y.First != y.Second)
            .Select(z => ($"{z.First}{z.Second}{z.First}", $"{z.Second}{z.First}{z.Second}"))
            .ToList();
    }
    
    public static long CountTlsIps(string[] input)
    {
        var possibleAbbas = GetPossibleAbbas();

        var ips = SplitIps(input);

        return ips.Count(ip => 
            ip.supernet.Any(x => possibleAbbas.Any(x.Contains)) && 
            ip.hypernet.All(x => !possibleAbbas.Any(x.Contains)));
    }

    private static List<(string[] supernet, string[] hypernet)> SplitIps(string[] input)
    {
        var indexes = input.Select(GetIndexes).ToArray();

        var result = new List<(string[] supernet, string[] hypernet)>();
        for (var i = 0; i < input.Length; i++)
        {
            var ips = new List<string>();
            var tls = new List<string>();
            for (var j = 0; j < indexes[i].Count; j++)
            {
                // Add first IP
                if (j == 0)
                {
                    ips.Add(input[i][..indexes[i][j].openIdx]);
                }

                // Add last IP
                if (j == indexes[i].Count - 1)
                {
                    ips.Add(input[i][(indexes[i][j].closeIdx + 1)..]);
                }
                else
                {
                    // Add following IP
                    ips.Add(input[i][(indexes[i][j].closeIdx + 1)..indexes[i][j + 1].openIdx]);
                }
                
                // Add TLS
                tls.Add(input[i][(indexes[i][j].openIdx + 1)..indexes[i][j].closeIdx]);
            }
            
            result.Add((ips.ToArray(), tls.ToArray()));
        }

        return result;
    }

    private static List<(int openIdx, int closeIdx)> GetIndexes(string ip)
    {
        var result = new List<(int openIdx, int closeIdx)>();

        var openIdx = 0;
        for (var i = 0; i < ip.Length; i++)
        {
            if (ip[i] == '[')
            {
                openIdx = i;
            }

            if (ip[i] == ']')
            {
                result.Add((openIdx, i));
            }
        }

        return result;
    }

    private static List<string> GetPossibleAbbas()
    {
        var possibleChars = Enumerable.Range(97, 26).Select(x => (char) x).ToArray();

        return possibleChars
            .SelectMany(ch => possibleChars.Select(ch2 => (First: ch, Second: ch2)))
            .Where(y => y.First != y.Second)
            .Select(z => $"{z.First}{z.Second}{z.Second}{z.First}")
            .ToList();
    }
}