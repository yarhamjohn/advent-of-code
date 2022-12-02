namespace AdventOfCode2022.Day2;

public static class Day2
{
    public static int GetTotalScore(IEnumerable<string> input)
    {
        var score = 0;

        foreach (var line in input)
        {
            var choices = line.Split(" ");

            var outcome = PlayRound(choices);
            switch (outcome)
            {
                case Outcome.Win:
                    score += 6;
                    break;
                case Outcome.Draw:
                    score += 3;
                    break;
                case Outcome.Lose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            score += choices[1] switch
            {
                "X" => 1,
                "Y" => 2,
                _ => 3
            };
        }
        return score;
    }

    private static Outcome PlayRound(string[] choices)
    {
        var opponent = choices[0];
        var player = choices[1];

        return opponent switch
        {
            "A" => player switch
            {
                "X" => Outcome.Draw,
                "Y" => Outcome.Win,
                _ => Outcome.Lose
            },
            "B" => player switch
            {
                "X" => Outcome.Lose,
                "Y" => Outcome.Draw,
                _ => Outcome.Win
            },
            _ => player switch
            {
                "X" => Outcome.Win,
                "Y" => Outcome.Lose,
                _ => Outcome.Draw
            }
        };
    }

    private enum Outcome
    {
        Win,
        Lose,
        Draw
    }
}