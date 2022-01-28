namespace AdventOfCode2021.Day4;

public static class Day4
{
    public static int CalculateBingoWinner(string[] input)
    {
        var game = BingoParser.Parse(input);
        // game.PrintGame();

        while (!game.IsWon())
        {
            game.DrawNextNumber();
        }

        var winner = game.GetWinningBoard();
        winner.PrintBoard();

        return game.GetLastDrawnNumber() * winner.SumOfUnMarkedSquares();
    }
    
    public static int CalculateBingoLoser(string[] input)
    {
        var game = BingoParser.Parse(input);
        // game.PrintGame();

        while (!game.OneBoardIsLeft())
        {
            game.DrawNextNumber();
        }

        var loser = game.GetRemainingBoard();
        while (!loser.IsWon())
        {
            game.DrawNextNumber();
        }
        loser.PrintBoard();

        return game.GetLastDrawnNumber() * loser.SumOfUnMarkedSquares();
    }

    private static class BingoParser
    {
        public static BingoGame Parse(string[] input)
        {
            return new BingoGame
            {
                Draw = ParseDraw(input), 
                Boards = ParseBoards(input.Skip(1).ToArray())
            };
        }

        private static BingoBoard[] ParseBoards(string[] input)
        {
            return input.Where(x => x.Trim() is not "").Chunk(5).Select(y => new BingoBoard(y)).ToArray();
        }

        private static int[] ParseDraw(string[] input)
        {
            return input.First().Split(",").Select(x => Convert.ToInt32(x)).ToArray();
        }
    }

    private class BingoGame
    {
        private int _nextIndexToDraw;
        public int[] Draw { get; set; }
        public BingoBoard[] Boards { get; set; }

        public void PrintGame()
        {
            Console.WriteLine(string.Join(",", Draw));
            foreach (var x in Boards)
            {
                for (var row = 0; row < x.Squares.GetLength(0); row++)
                {
                    for (var col = 0; col < x.Squares.GetLength(1); col++)
                    {
                        if (x.Squares[row, col].IsMarked)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }

                        Console.Write(x.Squares[row, col].Value.ToString().PadLeft(3));
                        
                        Console.ResetColor();
                    }
                    Console.WriteLine();
                }
            
                Console.WriteLine();
            }
        }

        public bool IsWon()
        {
            return Boards.Any(x => x.IsWon());
        }

        public void DrawNextNumber()
        {
            var num = Draw[_nextIndexToDraw];

            foreach (var board in Boards)
            {
                board.ApplyDraw(num);
            }

            _nextIndexToDraw++;
        }

        public BingoBoard GetWinningBoard()
        {
            return Boards.Single(x => x.IsWon());
        }

        public int GetLastDrawnNumber()
        {
            return Draw[_nextIndexToDraw - 1];
        }

        public bool OneBoardIsLeft()
        {
            return Boards.Count(x => !x.IsWon()) == 1;
        }

        public BingoBoard GetRemainingBoard()
        {
            return Boards.Single(x => !x.IsWon());
        }
    }

    private class BingoBoard
    {
        public BingoSquare[,] Squares { get; } = new BingoSquare[5, 5];

        public BingoBoard(string[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                var nums = input[i].Split(" ").Where(x => x != "").Select(y => Convert.ToInt32(y)).ToArray();
                for (var j = 0; j < nums.Length; j++)
                {
                    Squares[i, j] = new BingoSquare { Value = nums[j] };
                }
            }
        }

        public int SumOfUnMarkedSquares()
        {
            var total = 0;
            foreach (var square in Squares)
            {
                if (!square.IsMarked)
                {
                    total += square.Value;
                }
            }

            return total;
        }

        public void ApplyDraw(int num)
        {
            foreach (var square in Squares)
            {
                if (square.Value == num)
                {
                    square.IsMarked = true;
                    break;
                }
            }
        }
        
        public void PrintBoard()
        {
            for (var row = 0; row < Squares.GetLength(0); row++)
            {
                for (var col = 0; col < Squares.GetLength(1); col++)
                {
                    if (Squares[row, col].IsMarked)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    Console.Write(Squares[row, col].Value.ToString().PadLeft(3));
                    
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public bool IsWon()
        {
            return CheckRowsForWinner() || CheckColsForWinner();
        }

        private bool CheckRowsForWinner()
        {
            for (var row = 0; row < Squares.GetLength(0); row++)
            {
                var count = 0;
                for (var col = 0; col < Squares.GetLength(1); col++)
                {
                    if (Squares[row, col].IsMarked)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (count == 5)
                {
                    return true;
                }
            }

            return false;
        }
        
        private bool CheckColsForWinner()
        {
            for (var col = 0; col < Squares.GetLength(1); col++)
            {
                var count = 0;
                for (var row = 0; row < Squares.GetLength(0); row++)
                {
                    if (Squares[row, col].IsMarked)
                    {
                        count++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (count == 5)
                {
                    return true;
                }
            }

            return false;
        }
    }

    private class BingoSquare
    {
        public bool IsMarked { get; set; }
        public int Value { get; set; }
    }
}

