using AdventOfCode2024.Day1;
using AdventOfCode2024.Day2;
using AdventOfCode2024.Day3;
using AdventOfCode2024.Day4;
using AdventOfCode2024.Day5;
using AdventOfCode2024.Day6;

long Day1A() => Day1.Part1(File.ReadAllLines("./Day1/input.txt"));
long Day1B() => Day1.Part2(File.ReadAllLines("./Day1/input.txt"));

long Day2A() => Day2.Part1(File.ReadAllLines("./Day2/input.txt"));
long Day2B() => Day2.Part2(File.ReadAllLines("./Day2/input.txt"));

long Day3A() => Day3.Part1(File.ReadAllLines("./Day3/input.txt"));
long Day3B() => Day3.Part2(File.ReadAllLines("./Day3/input.txt"));

long Day4A() => Day4.Part1(File.ReadAllLines("./Day4/input.txt"));
long Day4B() => Day4.Part2(File.ReadAllLines("./Day4/input.txt"));

long Day5A() => Day5.Part1(File.ReadAllLines("./Day5/input.txt"));
long Day5B() => Day5.Part2(File.ReadAllLines("./Day5/input.txt"));

long Day6A() => Day6.Part1(File.ReadAllLines("./Day6/input.txt"));
long Day6B() => Day6.Part2(File.ReadAllLines("./Day6/input.txt"));


// 396 is too low

Console.WriteLine(Day6B());