namespace AdventOfCode2022.Day7;

public static class Day7
{
    public static long GetFileSizes(string[] input)
    {
        return GetDirectories(input)
            .Select(d => d.GetTotalFileSize())
            .Where(x => x < 100000)
            .Sum();
    }

    public static long GetDirectoryToDelete(string[] input)
    {
        return GetDirectories(input)
            .Select(x => x.GetTotalFileSize())
            .Where(y => y >= GetSpaceRequired(input))
            .Min();
    }

    private static long GetSpaceRequired(string[] input)
        => 30000000 - GetFreeSpace(input);

    private static long GetFreeSpace(string[] input)
        => 70000000 - GetSizeOfRootDirectory(input);

    private static long GetSizeOfRootDirectory(string[] input)
        => GetDirectories(input).Single(d => d.ParentDirectory is null).GetTotalFileSize();

    private static IEnumerable<Directory> GetDirectories(string[] input)
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
                        if (currentDirectory.ParentDirectory is not null)
                        {
                            currentDirectory = currentDirectory.ParentDirectory;
                        }
                    }
                    else
                    {
                        var newDirectory = new Directory(segments[2], currentDirectory);
                        directories.Add(newDirectory);

                        currentDirectory.Directories.Add(newDirectory);
                        currentDirectory = newDirectory;
                    }

                    break;
                }
                case "$" when segments[1] == "ls":
                case "dir":
                    break;
                default:
                    currentDirectory.Files.Add(new File(segments[1], Convert.ToInt32(segments[0])));
                    break;
            }
        }

        return directories;
    }

    private class Directory
    {
        private readonly string _name;
        public readonly Directory? ParentDirectory;
        public readonly List<Directory> Directories = new();
        public readonly List<File> Files = new();

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