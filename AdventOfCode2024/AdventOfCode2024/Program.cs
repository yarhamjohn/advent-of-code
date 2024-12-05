using AdventOfCode2024.Day1;
using AdventOfCode2024.Day2;

long Day1A() => Day1.Part1(File.ReadAllLines("./Day1/input.txt"));
long Day1B() => Day1.Part2(File.ReadAllLines("./Day1/input.txt"));

long Day2A() => Day2.Part1(File.ReadAllLines("./Day2/input.txt"));
long Day2B() => Day2.Part2(File.ReadAllLines("./Day2/input.txt"));

Console.WriteLine(Day2B());