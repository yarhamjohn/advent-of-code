using System.Diagnostics.Metrics;

namespace AdventOfCode2023.Day7;

public static class Day7
{
    public static long CalculateTotalWinnings(IEnumerable<string> input)
    {
        return RankHands(
                input.Select(l => l.Split(" "))
                    .Select(x => (IHand) new Hand(x.First(), int.Parse(x.Last())))
                    .ToList())
            .Sum(x => x.Key * x.Value.GetBid());
    }
    
    public static long CalculateTotalWinningsJoker(IEnumerable<string> input)
    {
        return RankHands(
                input.Select(l => l.Split(" "))
                    .Select(x => (IHand) new JokerHand(x.First(), int.Parse(x.Last())))
                    .ToList())
            .Sum(x => x.Key * x.Value.GetBid());
    }

    private static Dictionary<int, IHand> RankHands(IReadOnlyList<IHand> hands)
    {
        var sortedHands = new List<IHand>();
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

    private interface IHand
    {
        int GetBid();
        HandType GetHandType();
        List<int> GetCards();
        string GetCardsStr();
        bool IsStrongerThan(IHand hand);
    }

    private class Hand(string cards, int bid) : IHand
    {
        private readonly List<int> _cards = GetCards(cards);
        private readonly HandType _type = GetType(cards);

        public string GetCardsStr() => cards;
        public int GetBid() => bid;
        public HandType GetHandType() => _type;
        public List<int> GetCards() => _cards;
        
        public bool IsStrongerThan(IHand hand)
        {
            if (_type == hand.GetHandType())
            {
                return OutRanks(hand);
            }

            return _type > hand.GetHandType();
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
                    _ => int.Parse(card.ToString())
                })
                .ToList();
        }

        private bool OutRanks(IHand hand)
        {
            for (var i = 0; i < _cards.Count; i++)
            {
                if (_cards[i] == hand.GetCards()[i])
                {
                    continue;
                }

                return _cards[i] > hand.GetCards()[i];
            }

            // Shouldn't ever get here
            return false;
        }

        private static HandType GetType(string cards)
        {
            var list = cards.GroupBy(c => c).Select(group => group.Count()).ToList();
            return list switch
            {
                {Count: 1} => HandType.FiveOfAKind,
                {Count: 2} when list.Any(x => x == 4) => HandType.FourOfAKind,
                {Count: 2} => HandType.FullHouse,
                {Count: 3} when list.Any(x => x == 3) => HandType.ThreeOfAKind,
                {Count: 3} => HandType.TwoPair,
                {Count: 4} => HandType.Pair,
                _ => HandType.HighCard
            };
        }
    }

    private class JokerHand(string cards, int bid) : IHand
    {
        private readonly List<int> _cards = GetCards(cards);
        private readonly HandType _type = GetType(cards);

        public string GetCardsStr() => cards;
        public int GetBid() => bid;
        public HandType GetHandType() => _type;
        public List<int> GetCards() => _cards;

        public bool IsStrongerThan(IHand hand)
        {
            if (_type == hand.GetHandType())
            {
                return OutRanks(hand);
            }

            return _type > hand.GetHandType();
        }

        private static List<int> GetCards(string cards)
        {
            return cards.Select(card => card switch
                {
                    'A' => 14,
                    'K' => 13,
                    'Q' => 12,
                    'J' => 1,
                    'T' => 10,
                    _ => int.Parse(card.ToString())
                })
                .ToList();
        }

        private bool OutRanks(IHand hand)
        {
            for (var i = 0; i < _cards.Count; i++)
            {
                if (_cards[i] == hand.GetCards()[i])
                {
                    continue;
                }

                return _cards[i] > hand.GetCards()[i];
            }

            // Shouldn't ever get here
            return false;
        }

        private static HandType GetType(string cards)
        {
            switch (cards.Count(x => x == 'J'))
            {
                case 0:
                {
                    var list = cards.GroupBy(c => c).Select(group => group.Count()).ToList();
                    return list switch
                    {
                        {Count: 1} => HandType.FiveOfAKind,
                        {Count: 2} when list.Any(x => x == 4) => HandType.FourOfAKind,
                        {Count: 2} => HandType.FullHouse,
                        {Count: 3} when list.Any(x => x == 3) => HandType.ThreeOfAKind,
                        {Count: 3} => HandType.TwoPair,
                        {Count: 4} => HandType.Pair,
                        _ => HandType.HighCard
                    };
                }
                case 1:
                {
                    var list = cards.GroupBy(c => c).Select(group => group.Count()).ToList();
                    return list switch
                    {
                        {Count: 2} => HandType.FiveOfAKind, // 1 Joker + 4 matching
                        {Count: 3} when list.Any(x => x == 3) => HandType.FourOfAKind, // 1 Joker + 3 + 1
                        {Count: 3} => HandType.FullHouse, // 1 Joker + 2 + 2
                        {Count: 4} => HandType.ThreeOfAKind, // 1 Joker + 1 + 1 + 2
                        _ => HandType.Pair // 1 Joker + 1 + 1 + 1 + 1
                    };
                }
                case 2:
                {
                    var list = cards.GroupBy(c => c).Select(group => group.Count()).ToList();
                    return list switch
                    {
                        {Count: 2} => HandType.FiveOfAKind, // 2 Joker + 3 matching
                        {Count: 3} => HandType.FourOfAKind, // 2 Joker + 1 + 2
                        _ => HandType.ThreeOfAKind, // 2 Joker + 1 + 1 + 1
                    };
                }
                case 3:
                {
                    var list = cards.GroupBy(c => c).Select(group => group.Count()).ToList();
                    return list switch
                    {
                        {Count: 2} => HandType.FiveOfAKind, // 3 Joker + 2 matching
                        _ => HandType.FourOfAKind, // 3 Joker + 1 + 1
                    };
                }
                default:
                    return HandType.FiveOfAKind;
            }
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