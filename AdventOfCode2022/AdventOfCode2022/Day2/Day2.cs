namespace AdventOfCode2022.Day2;

public static class Day2
{
    public static int GetTotalScore(IEnumerable<string> input)
        => input
            .Select(line => line.Split(" "))
            .Select(elements => (GetShape(elements[0]), GetShape(elements[1])))
            .Select(hand => (int) PlayRound(hand.Item1, hand.Item2) + (int) hand.Item2)
            .Sum();
    
    public static int GetTotalScoreRevised(IEnumerable<string> input)
        => input
            .Select(line => line.Split(" "))
            .Select(elements => (GetShape(elements[0]), GetOutcome(elements[1])))
            .Select(round => (int)round.Item2 + (int)ChooseHand(round.Item1, round.Item2))
            .Sum();

    private static Shape ChooseHand(Shape opponent, Outcome outcome)
    {
        return outcome switch
        {
            Outcome.Win when opponent == Shape.Rock => Shape.Paper,
            Outcome.Win when opponent == Shape.Paper => Shape.Scissors,
            Outcome.Win when opponent == Shape.Scissors => Shape.Rock,
            Outcome.Lose when opponent == Shape.Rock => Shape.Scissors,
            Outcome.Lose when opponent == Shape.Paper => Shape.Rock,
            Outcome.Lose when opponent == Shape.Scissors => Shape.Paper,
            Outcome.Draw => opponent,
            _ => throw new ArgumentOutOfRangeException(nameof(outcome), outcome, null)
        };
    }
    
    private static Outcome PlayRound(Shape opponent, Shape player)
    {
        if (opponent == player)
        {
            return Outcome.Draw;
        }

        switch (opponent)
        {
            case Shape.Rock when player == Shape.Paper:
            case Shape.Paper when player == Shape.Scissors:
            case Shape.Scissors when player == Shape.Rock:
                return Outcome.Win;
            default:
                return Outcome.Lose;
        }
    }

    private static Shape GetShape(string input)
        => input switch
        {
            "A" or "X" => Shape.Rock,
            "B" or "Y" => Shape.Paper,
            "C" or "Z" => Shape.Scissors,
            _ => throw new ArgumentOutOfRangeException(nameof(input))
        };
    
    private static Outcome GetOutcome(string input)
        => input switch
        {
            "X" => Outcome.Lose,
            "Y" => Outcome.Draw,
            "Z" => Outcome.Win,
            _ => throw new ArgumentOutOfRangeException(nameof(input))
        };

    private enum Outcome
    {
        Win = 6,
        Draw = 3,
        Lose = 0
    }
    
    private enum Shape
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }
}