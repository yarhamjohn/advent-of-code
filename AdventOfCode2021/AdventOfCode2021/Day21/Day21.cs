namespace AdventOfCode.Day21;

public static class Day21
{
    public static long CalculateDeterministicScore(string[] input)
    {
        var players = GetPlayers(input);
        var dice = new Dice();

        var activePlayer = players.Single(x => x.Id == 1);
        var totalNumRolls = 0;

        while (!players.Any(x => x.Score >= 1000))
        {
            var playerMove = dice.GetNextThreeRolls();
            activePlayer.MovePlayer(playerMove);

            activePlayer = players.Single(x => x.Id != activePlayer.Id);
            totalNumRolls += 3;
        }


        return players.Single(x => x.Score < 1000).Score * totalNumRolls;
    }

    private static long NumPlayerOneWins = 0;
    private static long NumPlayerTwoWins = 0;

    public static long CalculateQuantumScore(string[] input)
    {
        var players = GetPlayers(input);

        RollDice(1, players[0].Score, players[0].Position, players[1].Score, players[1].Position);

        Console.WriteLine(NumPlayerOneWins);
        Console.WriteLine(NumPlayerTwoWins);
        return NumPlayerOneWins > NumPlayerTwoWins ? NumPlayerOneWins : NumPlayerTwoWins;
    }

    private static int[] diceRolls = { 3, 4, 5, 4, 5, 6, 5, 6, 7, 4, 5, 6, 5, 6, 7, 6, 7, 8, 5, 6, 7, 6, 7, 8, 7, 8, 9 };

    private static void RollDice(
        int activePlayerId,
        int playerOneScore,
        int playerOneIndex,
        int playerTwoScore,
        int playerTwoIndex)
    {
        foreach (var roll in diceRolls)
        {
            switch (activePlayerId)
            {
                case 1:
                {
                    var newPosition = (playerOneIndex + roll) % 10;
                    var adjustedPosition = newPosition == 0 ? 10 : newPosition;
                    var newScore = playerOneScore + adjustedPosition;

                    if (newScore >= 11)
                    {
                        NumPlayerOneWins++;
                    }
                    else
                    {
                        RollDice(2, newScore, adjustedPosition, playerTwoScore, playerTwoIndex);
                    }

                    break;
                }
                case 2:
                {
                    var newPosition = (playerTwoIndex + roll) % 10;
                    var adjustedPosition = newPosition == 0 ? 10 : newPosition;
                    var newScore = playerTwoScore + adjustedPosition;

                    if (newScore >= 11)
                    {
                        NumPlayerTwoWins++;
                    }
                    else
                    {
                        RollDice(1, playerOneScore, playerOneIndex, newScore, adjustedPosition);
                    }

                    break;
                }
            }
        }
    }

    private static int[] GetRolls()
    {
        var rolls = new List<int>();
        
        var possibleValues = new[] { 1, 2, 3 };
        for (var x = 0; x < possibleValues.Length; x++)
        {
            for (var y = 0; y < possibleValues.Length; y++)
            {
                for (var z = 0; z < possibleValues.Length; z++)
                {
                    rolls.Add(possibleValues[x] + possibleValues[y] + possibleValues[z]);
                }
            }
        }

        return rolls.ToArray();
    }

    private static Player[] GetPlayers(IEnumerable<string> input)
    {
        return input.Select(x =>
        {
            var segments = x.Split(" ");
            return new Player(Convert.ToInt32(segments[1]), Convert.ToInt32(segments[4]));
        }).ToArray();
    }

    private class Player
    {
        public int Id { get; }
        public int Position { get; set; }
        public int Score { get; private set; }

        public Player(int id, int position)
        {
            Id = id;
            Position = position;
        }
        
        public Player(int id, int position, int score)
        {
            Id = id;
            Position = position;
            Score = score;
        }

        public void MovePlayer(int positions)
        {
            var newPosition = (Position + positions) % 10;
            var adjustedPosition = newPosition == 0 ? 10 : newPosition;

            Position = adjustedPosition;
            Score += adjustedPosition;
        }

        public Player Clone()
        {
            return new Player(Id, Position, Score);
        }
    }

    private class Dice
    {
        private readonly int[] _diceRolls = Enumerable.Range(1, 100).ToArray();
        private int _nextRoll;

        public int GetNextThreeRolls()
        {
            var rolls = new int[3];

            for (var i = 0; i < 3; i++)
            {
                rolls[i] = _diceRolls[(_nextRoll + i) % 100];
            }

            _nextRoll = (_nextRoll + 3) % 100;
            return rolls.Sum() % 10;
        }
    }
}