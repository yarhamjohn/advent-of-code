namespace AdventOfCode2022.Day6;

public static class Day6
{
    public static long GetStartOfSection(string input, int sectionLength)
    {
        var queue = new Queue<char>();

        var position = 0;
        while (true)
        {
            if (queue.Count == sectionLength)
            {
                if (queue.Distinct().Count() == sectionLength)
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