using AdventOfCode2015.Day1;
using AdventOfCode2015.Day2;
using AdventOfCode2015.Day3;

long Day1A() => Day1.GetTargetFloor(File.ReadAllText("./Day1/input.txt"));
long Day1B() => Day1.GetBasementEntry(File.ReadAllText("./Day1/input.txt"));

long Day2A() => Day2.GetRequiredArea(File.ReadAllLines("./Day2/input.txt").ToArray());
long Day2B() => Day2.GetRequiredLength(File.ReadAllLines("./Day2/input.txt").ToArray());

long Day3A() => Day3.GetNumberOfHouses(File.ReadAllText("./Day3/input.txt"));
long Day3B() => Day3.GetNumberOfHousesWithRoboSanta(File.ReadAllText("./Day3/input.txt"));

Console.WriteLine(Day3B());