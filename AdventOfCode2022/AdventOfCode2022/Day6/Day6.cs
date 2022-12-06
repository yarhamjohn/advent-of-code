namespace AdventOfCode2022.Day6;

public static class Day6
{
    public static long GetStartOfPacket(string input)
    {
        var queue = new Queue<char>();

        var position = 0;
        while (true)
        {
            if (queue.Count == 4)
            {
                if (queue.ToArray().Distinct().Count() == 4)
                {
                    return position;
                }

                queue.Dequeue();
            }
            
            queue.Enqueue(input[position]);

            position++;
        }
    }
}