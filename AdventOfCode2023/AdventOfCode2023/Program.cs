using AdventOfCode2023.Day1;
using AdventOfCode2023.Day2;

long Day1A() => Day1.GetCalibrationSum(File.ReadAllLines("./Day1/input.txt"));
long Day1B() => Day1.GetCalibrationSum(File.ReadAllLines("./Day1/input.txt"));

long Day2A() => Day2.GetGameIdSum(File.ReadAllLines("./Day2/input.txt"));
long Day2B() => Day2.GetPowerCubeSum(File.ReadAllLines("./Day2/input.txt"));

Console.WriteLine(Day2A());
Console.WriteLine(Day2B());
