using AdventOfCode2015.Day1;
using AdventOfCode2015.Day2;
using AdventOfCode2015.Day3;
using AdventOfCode2015.Day4;
using AdventOfCode2015.Day5;
using AdventOfCode2015.Day6;
using AdventOfCode2015.Day7;
using AdventOfCode2015.Day8;
using AdventOfCode2015.Day9;
using AdventOfCode2015.Day10;
using AdventOfCode2015.Day11;
using AdventOfCode2015.Day12;
using AdventOfCode2015.Day13;
using AdventOfCode2015.Day14;
using AdventOfCode2015.Day15;
using AdventOfCode2015.Day16;
using AdventOfCode2015.Day17;
using AdventOfCode2015.Day18;
using AdventOfCode2015.Day19;
using AdventOfCode2015.Day20;
using AdventOfCode2015.Day21;
using AdventOfCode2015.Day23;

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

long Day7A() => Day7.GetWireSignal("a", File.ReadAllLines("./Day7/input.txt"));
long Day7B() => Day7.GetWireSignal2("a", File.ReadAllLines("./Day7/input.txt"));

long Day8A() => Day8.CalculateCharacters(File.ReadAllLines("./Day8/input.txt"));
long Day8B() => Day8.CalculateCharacters2(File.ReadAllLines("./Day8/input.txt"));

long Day9A() => Day9.GetMinDistance(File.ReadAllLines("./Day9/input.txt"));
long Day9B() => Day9.GetMaxDistance(File.ReadAllLines("./Day9/input.txt"));

long Day10A() => Day10.GetLength("1113222113", 40);
long Day10B() => Day10.GetLength("1113222113", 50);

string Day11A() => Day11.GetNextPassword("hepxcrrq");
string Day11B() => Day11.GetNextPassword("hepxxyzz");

long Day12A() => Day12.GetSum(File.ReadAllText("./Day12/input.json"));
long Day12B() => Day12.GetSumWithoutRed(File.ReadAllText("./Day12/input.json"));

long Day13A() => Day13.CalculateOptimalHappiness(File.ReadAllLines("./Day13/input.txt"));
long Day13B() => Day13.CalculateOptimalHappiness(File.ReadAllLines("./Day13/input2.txt"));

long Day14A() => Day14.GetWinningDistance(File.ReadAllLines("./Day14/input.txt"), 2503);
long Day14B() => Day14.GetWinningPoints(File.ReadAllLines("./Day14/input.txt"), 2503);

long Day15A() => Day15.GetHighestScoringCookie(File.ReadAllLines("./Day15/input.txt"));
long Day15B() => Day15.GetHighestScoringCookieWithCalories(File.ReadAllLines("./Day15/input.txt"));

long Day16A() => Day16.GetCorrectAuntSue(File.ReadAllLines("./Day16/input.txt"));
long Day16B() => Day16.GetCorrectAuntSueWithRecalibration(File.ReadAllLines("./Day16/input.txt"));

long Day17A() => Day17.GetContainerCombinations(File.ReadAllLines("./Day17/input.txt"), 150);
long Day17B() => Day17.GetNumberOfWays(File.ReadAllLines("./Day17/input.txt"), 150);

long Day18A() => Day18.CountLights(File.ReadAllLines("./Day18/input.txt"), 100);
long Day18B() => Day18.CountLightsWithStuckOn(File.ReadAllLines("./Day18/input.txt"), 100);

long Day19A() => Day19.CountDistinctMolecules(File.ReadAllLines("./Day19/input.txt"), "CRnSiRnCaPTiMgYCaPTiRnFArSiThFArCaSiThSiThPBCaCaSiRnSiRnTiTiMgArPBCaPMgYPTiRnFArFArCaSiRnBPMgArPRnCaPTiRnFArCaSiThCaCaFArPBCaCaPTiTiRnFArCaSiRnSiAlYSiThRnFArArCaSiRnBFArCaCaSiRnSiThCaCaCaFYCaPTiBCaSiThCaSiThPMgArSiRnCaPBFYCaCaFArCaCaCaCaSiThCaSiRnPRnFArPBSiThPRnFArSiRnMgArCaFYFArCaSiRnSiAlArTiTiTiTiTiTiTiRnPMgArPTiTiTiBSiRnSiAlArTiTiRnPMgArCaFYBPBPTiRnSiRnMgArSiThCaFArCaSiThFArPRnFArCaSiRnTiBSiThSiRnSiAlYCaFArPRnFArSiThCaFArCaCaSiThCaCaCaSiRnPRnCaFArFYPMgArCaPBCaPBSiRnFYPBCaFArCaSiAl");
long Day19B() => Day19.GetFewestSteps(File.ReadAllLines("./Day19/input.txt"), "CRnSiRnCaPTiMgYCaPTiRnFArSiThFArCaSiThSiThPBCaCaSiRnSiRnTiTiMgArPBCaPMgYPTiRnFArFArCaSiRnBPMgArPRnCaPTiRnFArCaSiThCaCaFArPBCaCaPTiTiRnFArCaSiRnSiAlYSiThRnFArArCaSiRnBFArCaCaSiRnSiThCaCaCaFYCaPTiBCaSiThCaSiThPMgArSiRnCaPBFYCaCaFArCaCaCaCaSiThCaSiRnPRnFArPBSiThPRnFArSiRnMgArCaFYFArCaSiRnSiAlArTiTiTiTiTiTiTiRnPMgArPTiTiTiBSiRnSiAlArTiTiRnPMgArCaFYBPBPTiRnSiRnMgArSiThCaFArCaSiThFArPRnFArCaSiRnTiBSiThSiRnSiAlYCaFArPRnFArSiThCaFArCaCaSiThCaCaCaSiRnPRnCaFArFYPMgArCaPBCaPBSiRnFYPBCaFArCaSiAl");

long Day20A() => Day20.GetLowestHouseNumber(36000000);
long Day20B() => Day20.GetLowestHouseNumberNonInfinite(36000000);

long Day21A() => Day21.GetMinimumGoldSpend();
long Day21B() => Day21.GetMaximumGoldSpend();

long Day23A() => Day23.CalculateRegisterB(File.ReadAllLines("./Day23/input.txt"), 0);
long Day23B() => Day23.CalculateRegisterB(File.ReadAllLines("./Day23/input.txt"), 1);

Console.WriteLine(Day23B());