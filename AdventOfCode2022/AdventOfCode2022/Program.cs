using AdventOfCode2022.Day1;
using AdventOfCode2022.Day2;

long Day1A() => Day1.GetTotalCalories(File.ReadAllLines("./Day1/input.txt"), 1);
long Day1B() => Day1.GetTotalCalories(File.ReadAllLines("./Day1/input.txt"), 3);

long Day2A() => Day2.GetTotalScore(File.ReadAllLines("./Day2/input.txt"));

Console.WriteLine(Day2A());