namespace AdventOfCode2015.Day16;

public static class Day16
{
   public static long GetCorrectAuntSue(IEnumerable<string> input)
   {
      return input.Select(ParseAunt).Single(aunt => aunt.CouldMatch(CreateTargetAunt())).Number;
   }
   
   public static long GetCorrectAuntSueWithRecalibration(IEnumerable<string> input)
   {
      return  input.Select(ParseAunt).Single(aunt => aunt.CouldMatchWithRecalibration(CreateTargetAunt())).Number;
   }

   private static Aunt CreateTargetAunt()
   {
      var aunt = new Aunt(0);

      aunt.AddCompound("akitas", 0);
      aunt.AddCompound("cars", 2);
      aunt.AddCompound("cats", 7);
      aunt.AddCompound("children", 3);
      aunt.AddCompound("goldfish", 5);
      aunt.AddCompound("perfumes", 1);
      aunt.AddCompound("pomeranians", 3);
      aunt.AddCompound("samoyeds", 2);
      aunt.AddCompound("trees", 3);
      aunt.AddCompound("vizslas", 0);
      
      return aunt;
   }

   private static Aunt ParseAunt(string input)
   {
      var segments = input
         .Replace(":", "")
         .Replace(",", "")
         .Split(" ");

      var aunt = new Aunt(Convert.ToInt32(segments[1]));
      
      aunt.AddCompound(segments[2], Convert.ToInt32(segments[3]));
      aunt.AddCompound(segments[4], Convert.ToInt32(segments[5]));
      aunt.AddCompound(segments[6], Convert.ToInt32(segments[7]));

      return aunt;
   }

   private class Aunt
   {
      public readonly int Number;
      private int? _children;
      private int? _cats;
      private int? _cars;
      private int? _trees;
      private int? _perfumes;
      private int? _goldfish;
      private int? _samoyeds;
      private int? _pomeranians;
      private int? _akitas;
      private int? _vizslas;

      public Aunt(int number)
      {
         Number = number;
      }

      public bool CouldMatch(Aunt aunt)
      {
         var children = _children is null || _children == aunt._children;
         var cats = _cats is null || _cats == aunt._cats;
         var cars = _cars is null || _cars == aunt._cars;
         var trees = _trees is null || _trees == aunt._trees;
         var perfumes = _perfumes is null || _perfumes == aunt._perfumes;
         var goldfish = _goldfish is null || _goldfish == aunt._goldfish;
         var samoyeds = _samoyeds is null || _samoyeds == aunt._samoyeds;
         var pomeranians = _pomeranians is null || _pomeranians == aunt._pomeranians;
         var akitas = _akitas is null || _akitas == aunt._akitas;
         var vizslas = _vizslas is null || _vizslas == aunt._vizslas;
         
         return children && cats && cars && trees && perfumes && goldfish && samoyeds && pomeranians && akitas && vizslas;
      }
      
      public bool CouldMatchWithRecalibration(Aunt aunt)
      {
         var children = _children is null || _children == aunt._children;
         var cats = _cats is null || _cats > aunt._cats;
         var cars = _cars is null || _cars == aunt._cars;
         var trees = _trees is null || _trees > aunt._trees;
         var perfumes = _perfumes is null || _perfumes == aunt._perfumes;
         var goldfish = _goldfish is null || _goldfish < aunt._goldfish;
         var samoyeds = _samoyeds is null || _samoyeds == aunt._samoyeds;
         var pomeranians = _pomeranians is null || _pomeranians < aunt._pomeranians;
         var akitas = _akitas is null || _akitas == aunt._akitas;
         var vizslas = _vizslas is null || _vizslas == aunt._vizslas;
         
         return children && cats && cars && trees && perfumes && goldfish && samoyeds && pomeranians && akitas && vizslas;
      }
      
      public void AddCompound(string compound, int quantity)
      {
         switch (compound)
         {
            
            case "akitas":
               _akitas = quantity;
               return;
            case "cars":
               _cars = quantity;
               return;
            case "cats":
               _cats = quantity;
               return;
            case "children":
               _children = quantity;
               return;
            case "goldfish":
               _goldfish = quantity;
               return;
            case "perfumes":
               _perfumes = quantity;
               return;
            case "pomeranians":
               _pomeranians = quantity;
               return;
            case "samoyeds":
               _samoyeds = quantity;
               return;
            case "trees":
               _trees = quantity;
               return;
            case "vizslas":
               _vizslas = quantity;
               return;
            default:
               throw new ArgumentOutOfRangeException(nameof(compound), compound, null);
         }
      }
   }
}