namespace AdventOfCode2015.Day21;

public static class Day21
{
    private static Dictionary<string, List<Item>> Shop = new(); 
        
    public static long GetMinimumGoldSpend()
    {
        InitializeShop();

        var possiblePurchases = GetPossiblePurchases();
        
        var boss = new Player(104, 8, 1);

        var goldRequired = int.MaxValue;
        foreach (var purchases in possiblePurchases)
        {
            Console.WriteLine(string.Join(",", purchases.Select(x => x.Name)));

            var damage = purchases.Sum(x => x.Damage);
            var armor = purchases.Sum(x => x.Armor);
            var player = new Player(100, damage, armor);

            var playerWon = PlayGame(boss, player);

            Console.WriteLine($"Boss: {boss}");
            Console.WriteLine($"Player: {player}");

            boss.ResetHealth();
            player.ResetHealth();
            
            if (playerWon)
            {
                var totalCost = purchases.Sum(x => x.Cost);
                if (totalCost < goldRequired)
                {
                    Console.WriteLine(totalCost);
                    goldRequired = totalCost;
                }
            }
        }
        
        return goldRequired;
    }

    private static bool PlayGame(Player boss, Player player)
    {
        var activePlayer = player;
        while (!boss.HasLost() && !player.HasLost())
        {
            if (activePlayer == player)
            {
                boss.TakeDamage(player.Damage);
                activePlayer = boss;
            }
            else
            {
                player.TakeDamage(boss.Damage);
                activePlayer = player;
            }
        }

        return !player.HasLost();
    }

    private static List<List<Item>> GetPossiblePurchases()
    {
        var purchases = new List<List<Item>>();
        
        // one weapon, no armor, no ring
        var weaponOnly = Shop["Weapons"].Select(x => new List<Item> {x}).ToList();
        purchases.AddRange(weaponOnly);
        
        // one weapon, one armor, no ring
        var weaponPlusArmor = new List<List<Item>>();
        foreach (var weaponSet in weaponOnly)
        {
            foreach (var armor in Shop["Armor"])
            {
                var newSet = weaponSet.Select(x => x).ToList();
                newSet.Add(armor);
                weaponPlusArmor.Add(newSet);
            }
        }
        purchases.AddRange(weaponPlusArmor);
        
        // one weapon, no armor, one ring
        // one weapon, no armor, two rings
        // one weapon, one armor, one ring
        // one weapon, one armor, two rings

        return purchases;
    }

    private static void InitializeShop()
    {
        Shop["Weapons"] = new List<Item>()
        {
            new("Dagger", 8, 4, 0),
            new("Shortsword", 10, 5, 0),
            new("Warhammer", 25, 6, 0),
            new("Longsword", 40, 7, 0),
            new("Greataxe", 74, 8, 0)
        };
        
        Shop["Armor"] = new List<Item>()
        {
            new("Leather", 13, 0, 1),
            new("Chainmail", 31, 0, 2),
            new("Splintmail", 53, 0, 3),
            new("Bandedmail", 75, 0, 4),
            new("Platemail", 102, 0, 5)
        };
        
        Shop["Rings"] = new List<Item>()
        {
            new("Damage +1", 25, 1, 0),
            new("Damage +2", 50, 2, 0),
            new("Damage +3", 100, 3, 0),
            new("Defense +1", 20, 0, 1),
            new("Defense +2", 40, 0, 2),
            new("Defense +3", 80, 0, 3)
        };
    }

    private record Item(string Name, int Cost, int Damage, int Armor);

    private class Player
    {
        private int _hitPoints;
        private int _currentHitPoints;
        public readonly int Damage;
        private readonly int _armor;

        public Player(int hitPoints, int damage, int armor)
        {
            _hitPoints = hitPoints;
            _currentHitPoints = hitPoints;
            Damage = damage;
            _armor = armor;
        }

        public void TakeDamage(int damage)
        {
            if (damage <= _armor)
            {
                _currentHitPoints -= 1;
            }
            else
            {
                _currentHitPoints -= damage - _armor;
            }
        }

        public void ResetHealth() => _currentHitPoints = _hitPoints;

        public bool HasLost() => _currentHitPoints <= 0;

        public override string ToString()
        {
            return $"Hit Points: {_currentHitPoints}";
        }
    }
}