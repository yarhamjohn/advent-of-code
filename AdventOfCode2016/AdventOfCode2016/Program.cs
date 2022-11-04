
using AdventOfCode2016.Day1;
using AdventOfCode2016.Day2;
using AdventOfCode2016.Day3;
using AdventOfCode2016.Day4;
using AdventOfCode2016.Day5;

long Day1A() => Day1.GetBlockCount(File.ReadAllText("./Day1/input.txt"));
long Day1B() => Day1.GetBlockCountToDoubleVisitedBlock(File.ReadAllText("./Day1/input.txt"));

string Day2A() => Day2.GetBathroomCode(File.ReadAllLines("./Day2/input.txt"));
string Day2B() => Day2.GetExtendedBathroomCode(File.ReadAllLines("./Day2/input.txt"));

long Day3A() => Day3.CountPossibleTriangles(File.ReadAllLines("./Day3/input.txt"));
long Day3B() => Day3.CountPossibleTrianglesVertically(File.ReadAllLines("./Day3/input.txt"));

long Day4A() => Day4.GetSectorIdSum(File.ReadAllLines("./Day4/input.txt"));
long Day4B() => Day4.GetRoomSectorId(File.ReadAllLines("./Day4/input.txt"), "north");

string Day5A() => Day5.GetPassword("reyedfim");

Console.WriteLine(Day5A());