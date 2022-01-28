using AdventOfCode2015.Day1;
using AdventOfCode2015.Day2;

long Day1A() => Day1.GetTargetFloor(File.ReadAllText("./Day1/input.txt"));
long Day1B() => Day1.GetBasementEntry(File.ReadAllText("./Day1/input.txt"));

long Day2A() => Day2.GetRequiredArea(File.ReadAllLines("./Day2/input.txt").ToArray());
long Day2B() => Day2.GetRequiredLength(File.ReadAllLines("./Day2/input.txt").ToArray());

Console.WriteLine(Day2B());