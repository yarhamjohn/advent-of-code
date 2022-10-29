namespace AdventOfCode2015.Day22;

public static class Day22
{
    public static long CalculateMinimumMana(int playerHitPoints, int playerMana, int bossHitPoints, int bossDamage)
    {
        var spells = new Spell[] { new MagicMissile(), new Drain(), new Shield(), new Poison(), new Recharge() };

        var spellCombinations = new List<Spell[]>();
        for (var i = 1; i < 10; i++)
        {
            var combinations = GetCombinations(spells, i).ToArray();

            spellCombinations.AddRange(combinations);
        }
        
        var manaSpent = int.MaxValue;
        
        foreach (var combination in spellCombinations)
        {
            var mana = PlayGame(combination, playerHitPoints, playerMana, bossHitPoints, bossDamage);

            if (mana < manaSpent)
            {
                manaSpent = (int) mana;
                Total = (int) mana;
            }
        }
        
        return manaSpent;
    }

    public static int Total = int.MaxValue;
    
    public static int? PlayGame(Spell[] spellsChosen, int playerHitPoints, int playerMana, int bossHitPoints, int bossDamage)
    {
        var activeSpells = new Dictionary<Spell, int>();
        
        var player = new Player(playerHitPoints, playerMana);
        var boss = new Boss(bossHitPoints, bossDamage);

        IPerson activePlayer = player;
        
        var spellIndex = 0;
        while (GameIsNotOver(player, boss))
        {
            if (Total < player.ManaSpent)
            {
                return null;
            }
            
            ApplyActiveSpells(activeSpells, player, boss);

            if (!GameIsNotOver(player, boss))
            {
                break;
            }
            
            if (spellIndex == spellsChosen.Length)
            {
                //Console.WriteLine($"Run out of spells ({string.Join(",",spellsChosen.Select(x => x.GetName()))}), player: {player.HitPoints}, boss: {boss.HitPoints}, manaSpent: {player.ManaSpent}");
                return null;
            }

            if (activePlayer is Boss b)
            {
                player.HitPoints -= b.Damage - player.Armor;
            }
            else
            {
                var nextSpell = spellsChosen[spellIndex];

                if (activeSpells.ContainsKey(nextSpell) && activeSpells[nextSpell] > 0)
                {
                    //Console.WriteLine($"Invalid spell combination ({spellIndex}): {string.Join(",",spellsChosen.Select(x => x.GetName()))}, player: {player.HitPoints}, boss: {boss.HitPoints}, manaSpent: {player.ManaSpent}");
                    return null;
                }
                
                player.BuySpell(nextSpell.GetCost());

                if (player.Mana <= 0)
                {
                    //Console.WriteLine($"Couldn't afford next spell ({spellIndex}) from: {string.Join(",",spellsChosen.Select(x => x.GetName()))}, player: {player.HitPoints}, boss: {boss.HitPoints}, manaSpent: {player.ManaSpent}");
                    return null;
                }
                
                CastSpell(nextSpell, player, boss, activeSpells);
            }

            if (activePlayer is Player)
            {
                spellIndex++;
                activePlayer = boss;
            }
            else
            {
                activePlayer = player;
            }
        }

        if (player.HitPoints > 0)
        {
            Console.WriteLine(
                $"Someone Won ({string.Join(",", spellsChosen.Select(x => x.GetName()))}), player: {player.HitPoints}, boss: {boss.HitPoints}, manaSpent: {player.ManaSpent}");
        }

        return player.HitPoints > 0 ? player.ManaSpent : null;
    }

    private static void ApplyActiveSpells(Dictionary<Spell, int> activeSpells, Player player, Boss boss)
    {
        foreach (var (key, value) in activeSpells)
        {
            switch (key)
            {
                case Shield shield:
                    if (activeSpells[shield] == 0)
                    {
                        player.Armor = 0;
                        activeSpells.Remove(shield);
                    }
                    else
                    {
                        player.Armor = shield.SetArmor;
                        activeSpells[shield] -= 1;
                    }

                    break;
                case Poison poison:
                    if (activeSpells[poison] == 0)
                    {
                        activeSpells.Remove(poison);
                    }
                    else
                    {
                        boss.HitPoints -= poison.DamagePerTurn;
                        activeSpells[poison] -= 1;
                    }

                    break;
                case Recharge recharge:
                    if (activeSpells[recharge] == 0)
                    {
                        activeSpells.Remove(recharge);
                    }
                    else
                    {
                        player.Mana += recharge.ManaPerTurn;
                        activeSpells[recharge] -= 1;
                    }

                    break;
            }
        }
    }

    private static bool GameIsNotOver(Player player, Boss boss)
    {
        return player.HitPoints > 0 && boss.HitPoints > 0;
    }

    private static void CastSpell(Spell spell, Player player, Boss boss, Dictionary<Spell, int> activeSpells)
    {
        switch (spell)
        {
            case Drain drain:
                player.HitPoints += drain.Heal;
                boss.HitPoints -= drain.Damage;
                break;
            case MagicMissile magicMissile:
                boss.HitPoints -= magicMissile.Damage;
                break;
            case Shield shield:
                activeSpells[shield] = shield.Turns;
                break;
            case Poison poison:
                activeSpells[poison] = poison.Turns;
                break;
            case Recharge recharge:
                activeSpells[recharge] = recharge.Turns;
                break;
        }
    }

    private static IEnumerable<Spell[]> GetCombinations(Spell[] spells, int length)
    {
        if (length == 1)
        {
             return spells.Select(s => new[] { s }).ToArray();
        }

        // foreach (var spell in spells)
        // {
             return spells.SelectMany(spell => GetCombinations(spells, length - 1).Select(x => new[] { spell }.Concat(x).ToArray()));
        // }
    }

    public abstract record Spell
    {
        public abstract string GetName();
        public abstract int GetCost();
    };

    public record Drain() : Spell
    {
        public override string GetName() => "Drain";
        public override int GetCost() => 73;

        public readonly int Damage = 2;
        public readonly int Heal = 2;
    }
    
    public record MagicMissile() : Spell
    {
        public override string GetName() => "MagicMissile";
        public override int GetCost() => 53;
        
        public readonly int Damage = 4;
    }
    
    public record Shield() : Spell
    {
        public override string GetName() => "Shield";
        public override int GetCost() => 113;
        
        public readonly int SetArmor = 7;
        public readonly int Turns = 6;
    }
    
    public record Poison() : Spell
    {
        public override string GetName() => "Poison";
        public override int GetCost() => 173;

        public readonly int DamagePerTurn = 3;
        public readonly int Turns = 6;
    }
    
    public record Recharge() : Spell
    {
        public override string GetName() => "Recharge";
        public override int GetCost() => 229;

        public readonly int ManaPerTurn = 101;
        public readonly int Turns = 5;
    }

    public interface IPerson
    {
    }

    public class Player : IPerson
    {
        public int HitPoints;
        public int Mana;
        public int Armor;

        public int ManaSpent;

        public Player(int hitPoints, int mana)
        {
            HitPoints = hitPoints;
            Mana = mana;
        }

        public void BuySpell(int cost)
        {
            Mana -= cost;
            ManaSpent += cost;
        }
    }

    public class Boss : IPerson
    {
        public int HitPoints;
        public int Damage;

        public Boss(int hitPoints, int damage)
        {
            HitPoints = hitPoints;
            Damage = damage;
        }
    }
}