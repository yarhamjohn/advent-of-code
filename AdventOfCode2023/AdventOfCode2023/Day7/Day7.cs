namespace AdventOfCode2023.Day7;

public static class Day7
{
    public static long CalculateTotalWinnings(IEnumerable<string> input)
    {
        var hands = input.Select(l => l.Split(" ")).Select(x => new Hand(x.First(), int.Parse(x.Last()))).ToList();

        var rankedHands = RankHands(hands);

        return rankedHands.Sum(x => x.Key * x.Value.Bid);
    }

    private static Dictionary<int, Hand> RankHands(List<Hand> hands)
    {
        var sortedHands = new List<Hand>();
        
        for (var i = 0; i < hands.Count; i++)
        {
            if (i == 0)
            {
                sortedHands.Insert(0, hands[i]);
                continue;
            }

            var numToCheck = sortedHands.Count;
            for (var j = 0; j < numToCheck; j++)
            {
                if (sortedHands[j].IsStrongerThan(hands[i]))
                {
                    sortedHands.Insert(j, hands[i]);
                    break;
                }

                if (j == sortedHands.Count - 1)
                {
                    sortedHands.Add(hands[i]);
                }
            }
        }
        
        return sortedHands.Select((x, i) => new { Hand = x, Index = i + 1}).ToDictionary(x => x.Index , x => x.Hand);
    }

    private class Hand(string cards, int bid)
    {
        private readonly List<int> _cards = GetCards(cards);
        public readonly int Bid = bid;

        public readonly HandType Type = GetHandType(cards);

        public bool IsStrongerThan(Hand hand)
        {
            if (Type == hand.Type)
            {
                return OutRanks(hand);
            }

            return Type > hand.Type;
        }

        private static List<int> GetCards(string cards)
        {
            return cards.Select(card => card switch
                {
                    'A' => 14,
                    'K' => 13,
                    'Q' => 12,
                    'J' => 11,
                    'T' => 10,
                    _ => card + '0'
                })
                .ToList();
        }

        private bool OutRanks(Hand hand)
        {
            for (var i = 0; i < _cards.Count; i++)
            {
                if (_cards[i] == hand._cards[i])
                {
                    continue;
                }

                return _cards[i] > hand._cards[i];
            }

            // Shouldn't ever get here
            return false;
        }

        private static HandType GetHandType(string cards)
        {
            var occurrences = new Dictionary<char, int>();
            foreach (var card in cards)
            {
                if (!occurrences.TryAdd(card, 1))
                {
                    occurrences[card]++;
                }
            }

            if (occurrences.Values.Count == 1)
            {
                return HandType.FiveOfAKind;
            }
            
            if (occurrences.Values.Any(x => x == 4))
            {
                return HandType.FourOfAKind;
            }

            if (occurrences.Values.Any(x => x == 3))
            {
                return occurrences.Values.Count == 3 ? HandType.ThreeOfAKind : HandType.FullHouse;
            }

            if (occurrences.Values.Count(x => x == 2) == 2)
            {
                return HandType.TwoPair;
            }

            if (occurrences.Values.Any(x => x == 2))
            {
                return HandType.Pair;
            }

            return HandType.HighCard;
        }
    }

    private enum HandType
    {
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
}