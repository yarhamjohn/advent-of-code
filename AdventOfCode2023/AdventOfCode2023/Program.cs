using AdventOfCode2023.Day1;
using AdventOfCode2023.Day10;
using AdventOfCode2023.Day11;
using AdventOfCode2023.Day12;
using AdventOfCode2023.Day13;
using AdventOfCode2023.Day2;
using AdventOfCode2023.Day3;
using AdventOfCode2023.Day4;
using AdventOfCode2023.Day5;
using AdventOfCode2023.Day6;
using AdventOfCode2023.Day7;
using AdventOfCode2023.Day8;
using AdventOfCode2023.Day9;

long Day1A() => Day1.GetCalibrationSum(File.ReadAllLines("./Day1/input.txt"));
long Day1B() => Day1.GetCalibrationSum(File.ReadAllLines("./Day1/input.txt"));

long Day2A() => Day2.GetGameIdSum(File.ReadAllLines("./Day2/input.txt"));
long Day2B() => Day2.GetPowerCubeSum(File.ReadAllLines("./Day2/input.txt"));

long Day3A() => Day3.SumPartNumbers(File.ReadAllLines("./Day3/input.txt"));
long Day3B() => Day3.SumGearNumbers(File.ReadAllLines("./Day3/input.txt"));

long Day4A() => Day4.CountCardPoints(File.ReadAllLines("./Day4/input.txt"));
long Day4B() => Day4.CountCards(File.ReadAllLines("./Day4/input.txt"));

long Day5A() => Day5.GetLowestLocationNumber(File.ReadAllLines("./Day5/input.txt"));
long Day5B() => Day5.GetLowestLocationNumberRange(File.ReadAllLines("./Day5/input.txt"));

long Day6A() => Day6.GetRecords(File.ReadAllLines("./Day6/input.txt"));
long Day6B() => Day6.GetRecordsBig(File.ReadAllLines("./Day6/input.txt"));

long Day7A() => Day7.CalculateTotalWinnings(File.ReadAllLines("./Day7/input.txt"));
long Day7B() => Day7.CalculateTotalWinningsJoker(File.ReadAllLines("./Day7/input.txt"));

long Day8A() => Day8.CalculateNumSteps(File.ReadAllLines("./Day8/input.txt"));
long Day8B() => Day8.CalculateNumStepsGhosts(File.ReadAllLines("./Day8/input.txt"));

long Day9A() => Day9.SumExtrapolatedValues(File.ReadAllLines("./Day9/input.txt"));
long Day9B() => Day9.SumExtrapolatedValuesReverse(File.ReadAllLines("./Day9/input.txt"));

long Day10A() => Day10.CountSteps(File.ReadAllLines("./Day10/input.txt"));
long Day10B() => Day10.CountInternalSpaces(File.ReadAllLines("./Day10/input.txt"));

long Day11A() => Day11.SumPathLengths(File.ReadAllLines("./Day11/input.txt"), 2);
long Day11B() => Day11.SumPathLengths(File.ReadAllLines("./Day11/input.txt"), 1000000);

long Day12A() => Day12.test(File.ReadAllLines("./Day12/input.txt"));
long Day12B() => Day12.testUnfolded(File.ReadAllLines("./Day12/input.txt"));
long Day12C() => Day12.testUnfolded(new[] { "#???????????????#?? 3,2,1,5"});

long Day13A() => Day13.ReflectionSum(File.ReadAllLines("./Day13/input.txt"));
long Day13B() => Day13.ReflectionSmudgeSum(File.ReadAllLines("./Day13/input.txt"));

// Console.WriteLine(Day13A());
Console.WriteLine(Day13B());
