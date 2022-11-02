
using AdventOfCode2016.Day1;
using AdventOfCode2016.Day2;

long Day1A() => Day1.GetBlockCount(File.ReadAllText("./Day1/input.txt"));
long Day1B() => Day1.GetBlockCountToDoubleVisitedBlock(File.ReadAllText("./Day1/input.txt"));

string Day2A() => Day2.GetBathroomCode(File.ReadAllLines("./Day2/input.txt"));

Console.WriteLine(Day2A());