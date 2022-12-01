using AdventOfCode2022.Day1;

long Day1A() => Day1.GetTotalCalories(File.ReadAllLines("./Day1/input.txt"), 1);
long Day1B() => Day1.GetTotalCalories(File.ReadAllLines("./Day1/input.txt"), 3);

Console.WriteLine(Day1B());