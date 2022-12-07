namespace AdventOfCode2022.Day7;

public static class Day7
{
    public static long GetFileSizes(string[] input)
    {
        var currentDirectory = new Directory("/", null);
        var directories = new List<Directory> { currentDirectory };
        
        foreach (var line in input[1..])
        {
            var segments = line.Split(" ");
            switch (segments[0])
            {
                case "$" when segments[1] == "cd":
                {
                    if (segments[2] == "..")
                    {
                        currentDirectory = currentDirectory.ParentDirectory;
                    }
                    else
                    {
                        var newDirectory = new Directory(segments[2], currentDirectory);
                        currentDirectory.Directories.Add(newDirectory);

                        currentDirectory = newDirectory;
                        directories.Add(newDirectory);
                    }

                    break;
                }
                case "$" when segments[1] == "ls":
                    // do nothing
                    break;
                case "dir":
                    // do noting
                    break;
                default:
                    currentDirectory.Files.Add(new File(segments[1], Convert.ToInt32(segments[0])));
                    break;
            }
        }
        
        return directories
            .Select(d => d.GetTotalFileSize())
            .Where(x => x < 100000)
            .Sum();
    }

    private class Directory
    {
        private readonly string _name;
        public Directory? ParentDirectory;
        public List<Directory> Directories = new();
        public List<File> Files = new();

        public Directory(string name, Directory? parentDirectory)
        {
            _name = name;
            ParentDirectory = parentDirectory;
        }
        
        public long GetTotalFileSize()
            => Directories.Sum(x => x.GetTotalFileSize())
               + Files.Sum(x => x.Size);
    }

    private record File(string Name, long Size);
}