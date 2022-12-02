
using AdventOfCode2016.Day1;
using AdventOfCode2016.Day10;
using AdventOfCode2016.Day11;
using AdventOfCode2016.Day12;
using AdventOfCode2016.Day13;
using AdventOfCode2016.Day14;
using AdventOfCode2016.Day15;
using AdventOfCode2016.Day2;
using AdventOfCode2016.Day3;
using AdventOfCode2016.Day4;
using AdventOfCode2016.Day5;
using AdventOfCode2016.Day6;
using AdventOfCode2016.Day7;
using AdventOfCode2016.Day8;
using AdventOfCode2016.Day9;

long Day1A() => Day1.GetBlockCount(File.ReadAllText("./Day1/input.txt"));
long Day1B() => Day1.GetBlockCountToDoubleVisitedBlock(File.ReadAllText("./Day1/input.txt"));

string Day2A() => Day2.GetBathroomCode(File.ReadAllLines("./Day2/input.txt"));
string Day2B() => Day2.GetExtendedBathroomCode(File.ReadAllLines("./Day2/input.txt"));

long Day3A() => Day3.CountPossibleTriangles(File.ReadAllLines("./Day3/input.txt"));
long Day3B() => Day3.CountPossibleTrianglesVertically(File.ReadAllLines("./Day3/input.txt"));

long Day4A() => Day4.GetSectorIdSum(File.ReadAllLines("./Day4/input.txt"));
long Day4B() => Day4.GetRoomSectorId(File.ReadAllLines("./Day4/input.txt"), "north");

string Day5A() => Day5.GetPassword("reyedfim");
string Day5B() => Day5.GetComplexPassword("reyedfim");

string Day6A() => Day6.GetMessage(File.ReadAllLines("./Day6/input.txt"));
string Day6B() => Day6.GetMessageModified(File.ReadAllLines("./Day6/input.txt"));

long Day7A() => Day7.CountTlsIps(File.ReadAllLines("./Day7/input.txt"));
long Day7B() => Day7.CountSslIps(File.ReadAllLines("./Day7/input.txt"));

long Day8A() => Day8.CountLitPixels(File.ReadAllLines("./Day8/input.txt"), 6, 50);
void Day8B() => Day8.PrintCode(File.ReadAllLines("./Day8/input.txt"), 6, 50);

long Day9A() => Day9.GetDecompressedLength(File.ReadAllText("./Day9/input.txt"));
long Day9B() => Day9.GetAdvancedDecompressedLength(File.ReadAllText("./Day9/input.txt"));

long Day10A() => Day10.GetComparerBotNumber(File.ReadAllLines("./Day10/input.txt"), 61, 17);
long Day10B() => Day10.GetOutputSummary(File.ReadAllLines("./Day10/input.txt"));

// Don't bother passing the input as it would require complex parsing but is easy enough to create in code instead.
long Day11A() => Day11.CountStepsTaken();
long Day11B() => Day11.CountExtendedStepsTaken();

long Day12A() => Day12.GetValueOfRegister(File.ReadAllLines("./Day12/input.txt"), "a");
long Day12B() => Day12.GetValueOfRegisterInitialized(File.ReadAllLines("./Day12/input.txt"), "a", new[] {("c", 1L)});

long Day13A() => Day13.CountSteps((31, 39), 1364);
long Day13B() => Day13.CountLocations(1364);

long Day14A() => Day14.Get64thKeyIndex("qzyelonm");
long Day14B() => Day14.Get64thKeyIndexStretched("qzyelonm");

long Day15A() => Day15.GetEarliestTime(File.ReadAllLines("./Day15/input.txt"));

Console.WriteLine(Day15A());