using AdventOfCode2023.Day1;
using AdventOfCode2023.Day2;
using AdventOfCode2023.Day3;
using AdventOfCode2023.Day4;
using AdventOfCode2023.Day5;

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

// Console.WriteLine(Day5A());
Console.WriteLine(Day5B());
