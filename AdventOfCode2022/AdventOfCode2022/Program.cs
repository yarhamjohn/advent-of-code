using AdventOfCode2022.Day1;
using AdventOfCode2022.Day2;
using AdventOfCode2022.Day3;

long Day1A() => Day1.GetTotalCalories(File.ReadAllLines("./Day1/input.txt"), 1);
long Day1B() => Day1.GetTotalCalories(File.ReadAllLines("./Day1/input.txt"), 3);

long Day2A() => Day2.GetTotalScore(File.ReadAllLines("./Day2/input.txt"));
long Day2B() => Day2.GetTotalScoreRevised(File.ReadAllLines("./Day2/input.txt"));

long Day3A() => Day3.GetPrioritySum(File.ReadAllLines("./Day3/input.txt"));
long Day3B() => Day3.GetBadgePrioritySum(File.ReadAllLines("./Day3/input.txt"));

Console.WriteLine(Day3A());