using AdventOfCode.Day1;
using AdventOfCode.Day2;
using AdventOfCode.Day3;
using AdventOfCode.Day4;
using AdventOfCode.Day5;
using AdventOfCode.Day6;
using AdventOfCode.Day7;
using AdventOfCode.Day8;
using AdventOfCode.Day9;
using AdventOfCode.Day10;
using AdventOfCode.Day11;
using AdventOfCode.Day12;
using AdventOfCode.Day13;
using AdventOfCode.Day14;
using AdventOfCode.Day15;
using AdventOfCode.Day16;
using AdventOfCode.Day17;
using AdventOfCode.Day18;
using AdventOfCode.Day19;
using AdventOfCode.Day20;
using AdventOfCode.Day21;
using AdventOfCode.Day22;
using AdventOfCode.Day23;
using AdventOfCode.Day24;
using AdventOfCode.Day25;

int Day1A() => Day1.CalculateTimesDepthIncreased(File.ReadLines(@"./Day1/input.txt").ToArray());
int Day1B() => Day1.CalculateTimesWindowDepthIncreased(File.ReadLines(@"./Day1/input.txt").ToArray());

int Day2A() => Day2.CalculatePosition(File.ReadLines(@"./Day2/input.txt").ToArray());
int Day2B() => Day2.CalculatePositionWithAim(File.ReadLines(@"./Day2/input.txt").ToArray());

int Day3A() => Day3.CalculatePowerConsumption(File.ReadLines(@"./Day3/input.txt").ToArray());
int Day3B() => Day3.CalculateLifeSupportRating(File.ReadLines(@"./Day3/input.txt").ToArray());

int Day4A() => Day4.CalculateBingoWinner(File.ReadLines(@"./Day4/input.txt").ToArray());
int Day4B() => Day4.CalculateBingoLoser(File.ReadLines(@"./Day4/input.txt").ToArray());

int Day5A() => Day5.CalculateDangerousVents(File.ReadLines(@"./Day5/input.txt").ToArray());
int Day5B() => Day5.CalculateDangerousVentsWithDiagonals(File.ReadLines(@"./Day5/input.txt").ToArray());

long Day6A() => Day6.CalculateLanternFish(File.ReadAllText(@"./Day6/input.txt"), 80);
long Day6B() => Day6.CalculateLanternFish(File.ReadAllText(@"./Day6/input.txt"), 256);

long Day7A() => Day7.CalculateFuelRequired(File.ReadAllText(@"./Day7/input.txt"));
long Day7B() => Day7.CalculateFuelRequiredIncludingIncrease(File.ReadAllText(@"./Day7/input.txt"));

int Day8A() => Day8.CalculateUniqueDigitAppearance(File.ReadLines(@"./Day8/input.txt"));
int Day8B() => Day8.CalculateOutputs(File.ReadLines(@"./Day8/input.txt"));

int Day9A() => Day9.CalculateRiskLevel(File.ReadLines(@"./Day9/input.txt").ToArray());
int Day9B() => Day9.CalculateBasins(File.ReadLines(@"./Day9/input.txt").ToArray());

int Day10A() => Day10.CalculateSyntaxErrorScore(File.ReadLines(@"./Day10/input.txt").ToArray());
long Day10B() => Day10.CalculateMiddleCompletionScore(File.ReadLines(@"./Day10/input.txt").ToArray());

long Day11A() => Day11.CalculateNumberOfFlashes(File.ReadLines(@"./Day11/input.txt").ToArray());
long Day11B() => Day11.CalculateFirstSimultaneousFlash(File.ReadLines(@"./Day11/input.txt").ToArray());

int Day12A() => Day12.CalculateNumberOfPaths(File.ReadLines(@"./Day12/input.txt").ToArray());
int Day12B() => Day12.CalculateNumberOfPathsWithDoubleVisit(File.ReadLines(@"./Day12/input.txt").ToArray());

int Day13A() => Day13.CalculateFirstFold(File.ReadLines(@"./Day13/input.txt").ToArray());
int Day13B() => Day13.CalculateAllFolds(File.ReadLines(@"./Day13/input.txt").ToArray());

long Day14A() => Day14.CalculatePolymerTemplate(File.ReadLines(@"./Day14/input.txt").ToArray(), 10);
long Day14B() => Day14.CalculatePolymerTemplateForFortyTurns(File.ReadLines(@"./Day14/input.txt").ToArray());

long Day15A() => Day15.CalculatePath(File.ReadLines(@"./Day15/input.txt").ToArray());
long Day15B() => Day15.CalculatePathBig(File.ReadLines(@"./Day15/input.txt").ToArray());

long Day16A() => Day16.CalculateVersionSum(File.ReadAllText(@"./Day16/input.txt"));
long Day16B() => Day16.CalculateValue(File.ReadAllText(@"./Day16/input.txt"));

long Day17A() => Day17.CalculateMaximumY(File.ReadAllText(@"./Day17/input.txt"));
long Day17B() => Day17.CalculateAllVelocities(File.ReadAllText(@"./Day17/input.txt"));

long Day18A() => Day18.CalculateMagnitude(File.ReadLines(@"./Day18/input.txt").ToArray());
long Day18B() => Day18.CalculateBiggestMagnitude(File.ReadLines(@"./Day18/input.txt").ToArray());

long Day19A() => Day19.CalculateBeacons(File.ReadLines(@"./Day19/input.txt").ToArray());

long Day20A() => Day20.CalculateLitPixels(File.ReadLines(@"./Day20/input.txt").ToArray(), 2);
long Day20B() => Day20.CalculateLitPixels(File.ReadLines(@"./Day20/input.txt").ToArray(), 50);

long Day21A() => Day21.CalculateDeterministicScore(File.ReadLines(@"./Day21/input.txt").ToArray());
long Day21B() => Day21.CalculateQuantumScore(File.ReadLines(@"./Day21/input.txt").ToArray());

long Day22A() => Day22.CalculateOnCubes(File.ReadLines(@"./Day22/input.txt").ToArray());

long Day23A() => Day23.CalculateLowestEnergy(File.ReadLines(@"./Day23/input.txt").ToArray());
long Day23B() => Day23.CalculateLowestEnergy(File.ReadLines(@"./Day23/input2.txt").ToArray());

long Day24A() => Day24.CalculateLargestModelNumbers(File.ReadLines(@"./Day24/input.txt").ToArray());

long Day25A() => Day25.CalculateStepsWithoutCucumbers(File.ReadLines(@"./Day25/input.txt").ToArray());

Console.WriteLine(Day24A());

