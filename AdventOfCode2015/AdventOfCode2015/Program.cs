using AdventOfCode2015.Day1;
using AdventOfCode2015.Day2;
using AdventOfCode2015.Day3;
using AdventOfCode2015.Day4;
using AdventOfCode2015.Day5;
using AdventOfCode2015.Day6;

long Day1A() => Day1.GetTargetFloor(File.ReadAllText("./Day1/input.txt"));
long Day1B() => Day1.GetBasementEntry(File.ReadAllText("./Day1/input.txt"));

long Day2A() => Day2.GetRequiredArea(File.ReadAllLines("./Day2/input.txt").ToArray());
long Day2B() => Day2.GetRequiredLength(File.ReadAllLines("./Day2/input.txt").ToArray());

long Day3A() => Day3.GetNumberOfHouses(File.ReadAllText("./Day3/input.txt"));
long Day3B() => Day3.GetNumberOfHousesWithRoboSanta(File.ReadAllText("./Day3/input.txt"));

long Day4A() => Day4.GetLowestHashableNumber(File.ReadAllText("./Day4/input.txt"), 5);
long Day4B() => Day4.GetLowestHashableNumber(File.ReadAllText("./Day4/input.txt"), 6);

long Day5A() => Day5.GetNumNiceStrings(File.ReadAllLines("./Day5/input.txt"));
long Day5B() => Day5.GetNumNiceStrings2(File.ReadAllLines("./Day5/input.txt"));

long Day6A() => Day6.GetNumLitLights(File.ReadAllLines("./Day6/input.txt"));
long Day6B() => Day6.GetTotalBrightness(File.ReadAllLines("./Day6/input.txt"));


Console.WriteLine(Day6B());