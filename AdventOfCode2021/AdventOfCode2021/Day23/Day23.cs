namespace AdventOfCode2021.Day23;

public static class Day23
{
    // Ignore walls at end
    private static readonly Dictionary<string, int> DoorwayIndexes = new() { {"A", 2}, {"B", 4}, {"C", 6}, {"D", 8} };
    
    public static long CalculateLowestEnergy(string[] input)
    {
        var hallway = InitializeHallway(input[1]);
        var initialRooms = InitializeRooms(input);

        // Get a list of all possible solutions
        var startIteration = new Iteration {Hallway = hallway, Rooms = initialRooms};

        var allPossibleIterations = CalculateAllPossibleIterations(startIteration).ToArray();
        Console.WriteLine($"Num iterations: {_count}");
        // Calculate the min energy of the possible solutions
        return _total;
    }

    private static int TotalEnergyUsed(Iteration iteration)
    {
        return iteration.Hallway.Sum(x => x?.EnergyUsed ?? 0) + iteration.Rooms.Sum(x =>
            x.occupants.Sum(y => y?.EnergyUsed ?? 0));
    }

    private static Amphipod?[] InitializeHallway(string input)
    {
        return input[1..^1].Select(x => x == '.' ? null : CreateAmphipod(x)).ToArray();
    }

    private class Iteration
    {
        public Amphipod?[] Hallway;
        public (string letter, Amphipod?[] occupants)[] Rooms;
    }

    private static int _total = Int32.MaxValue;
    private static int _count = 0;

    private static IEnumerable<Iteration> CalculateAllPossibleIterations(Iteration iteration)
    {
        _count++;
        
        var totalEnergyUsed = TotalEnergyUsed(iteration);
        var minimumRemainingEnergy = GetMinimumEnergyRemaining(iteration);

        // Console.WriteLine($"total: {_total}, EnergyUsed: {totalEnergyUsed}, minimumRemaining: {minimumRemainingEnergy}");
        
        if (totalEnergyUsed > _total)
        {
            return Enumerable.Empty<Iteration>();
        }

        if (minimumRemainingEnergy + totalEnergyUsed > _total)
        {
            return Enumerable.Empty<Iteration>();
        }

        if (IsComplete(iteration))
        {
            _total = totalEnergyUsed < _total ? totalEnergyUsed : _total;
            return new[] { iteration };
        }
        
        return GetNextIterations(iteration).SelectMany(CalculateAllPossibleIterations);
    }

    private static int GetMinimumEnergyRemaining(Iteration iteration)
    {
        var energy = 0;
        // check each hallway amphipod to inside door of room
        // check each room amphipod to inside door of room
        for (var i = 0; i < iteration.Hallway.Length; i++)
        {
            var pod = iteration.Hallway[i];
            if (pod is not null)
            {
                energy += DistanceFromHallwayToFirstSpaceInRoom(i, pod.Id);
            }
        }

        for (var i = 0; i < iteration.Rooms.Length; i++)
        {
            var room = iteration.Rooms[i];
            for (var j = 0; j < room.occupants.Length; j++)
            {
                var pod = room.occupants[j];
                if (pod is not null && pod.Id != room.letter)
                {
                    energy += j + 1; // distance out of room
                    energy += DistanceFromHallwayToFirstSpaceInRoom(DoorwayIndexes[room.letter], pod.Id);
                }
            }
        }
        
        return energy;
    }

    private static IEnumerable<Iteration> GetNextIterations(Iteration iteration)
    {
        return GetIterationsFromHallwayAmphipods(iteration)
            .Concat(GetIterationsFromRoomAmphipods(iteration));
    }

    private static IEnumerable<Iteration> GetIterationsFromRoomAmphipods(Iteration iteration)
    {
        return iteration.Rooms
            .SelectMany(room => !RoomContainsOnlyCorrectOrNull(room) && !RoomIsEmpty(room)
                ? GetIterationsForRoom(iteration.Rooms, room.letter, iteration.Hallway)
                : Enumerable.Empty<Iteration>());
    }

    private static bool RoomIsEmpty((string letter, Amphipod?[] occupants) room)
    {
        return room.occupants.All(x => x is null);
    }

    private static IEnumerable<Iteration> GetIterationsForRoom((string letter, Amphipod?[] occupants)[] rooms, string letter, Amphipod?[] hallway)
    {
        // No amphipods in hallway, so get every iteration from room specified by letter
        if (hallway.All(amphipod => amphipod is null))
        {
            return Enumerable.Range(0, 11)
                .Except(DoorwayIndexes.Values)
                .Select(hallwayIndex => MoveFromRoomToHallway(rooms, letter, hallway, hallwayIndex));
        }
        
        var iterations = new List<Iteration>();
        
        // Search left along hallway
        for (var i = DoorwayIndexes[letter]; i >= 0; i--)
        {
            if (hallway[i] is not null)
            {
                // Blocked from rest of hallway
                break;
            }

            if (!DoorwayIndexes.ContainsValue(i))
            {
                iterations.Add(MoveFromRoomToHallway(rooms, letter, hallway, i));
            }
        }
        
        // Search right along hallway
        for (var i = DoorwayIndexes[letter]; i < hallway.Length; i++)
        {
            if (hallway[i] is not null)
            {
                // Blocked from rest of hallway
                break;
            }

            if (!DoorwayIndexes.ContainsValue(i))
            {
                iterations.Add(MoveFromRoomToHallway(rooms, letter, hallway, i));
            }
        }

        return iterations;
    }

    private static Iteration MoveFromRoomToHallway(
        (string letter, Amphipod?[] occupants)[] rooms,
        string letter,
        Amphipod?[] hallway,
        int hallwayIndex)
    {
        var targetRoomOccupants = rooms.Single(x => x.letter == letter).occupants;
        var indexOfFirstAmphipodInRoom = targetRoomOccupants.ToList().FindIndex(0, a => a is not null);
        
        if (indexOfFirstAmphipodInRoom == -1) throw new InvalidOperationException("Room shouldn't be empty here.");

        var targetAmphipod = targetRoomOccupants[indexOfFirstAmphipodInRoom];
        var replacementAmphipod = targetAmphipod!.Clone(targetAmphipod.EnergyUsed, targetAmphipod.Status);
        
        var distance = GetDistanceToMove(hallwayIndex, letter, indexOfFirstAmphipodInRoom);
        replacementAmphipod.Move(distance);
        
        return new Iteration
        {
            Hallway = AddAmphipodToHallway(hallway, hallwayIndex, replacementAmphipod),
            Rooms = RemoveAmphipodFromRoom(rooms, letter)
        };
    }

    private static Amphipod?[] AddAmphipodToHallway(Amphipod?[] hallway, int index, Amphipod amphipod)
    {
        var newHallway = new Amphipod?[hallway.Length];
        for (var x = 0; x < hallway.Length; x++)
        {
            if (x == index)
            {
                newHallway[x] = amphipod;
            }

            else if (hallway[x] is null)
            {
                newHallway[x] = null;
            }

            else
            {
                var a = hallway[x]!;
                newHallway[x] = a.Clone(a.EnergyUsed, a.Status);
            }
        }

        return newHallway;
    }

    private static (string letter, Amphipod?[] occupants)[] RemoveAmphipodFromRoom((string letter, Amphipod?[] occupants)[] rooms, string letter)
    {
        var newRooms = new (string letter, Amphipod?[] occupants)[rooms.Length];
        for (var i = 0; i < rooms.Length; i++)
        {
            newRooms[i] = (rooms[i].letter, rooms[i].occupants.Select(x => x?.Clone(x.EnergyUsed, x.Status)).ToArray());
        }
        
        switch (letter)
        {
            case "A":
                newRooms[0].occupants[rooms[0].occupants.ToList().FindIndex(0, x => x is not null)] = null;
                break;
            case "B":
                newRooms[1].occupants[rooms[1].occupants.ToList().FindIndex(0, x => x is not null)] = null;
                break;
            case "C":
                newRooms[2].occupants[rooms[2].occupants.ToList().FindIndex(0, x => x is not null)] = null;
                break;
            case "D":
                newRooms[3].occupants[rooms[3].occupants.ToList().FindIndex(0, x => x is not null)] = null;
                break;
        }

        return newRooms;    }

    private static IEnumerable<Iteration> GetIterationsFromHallwayAmphipods(Iteration iteration)
    {
        return iteration.Hallway
            .Select((amphipod, hallwayIndex) => (amphipod, hallwayIndex))
            .Where(x => x.amphipod is not null && CanMoveToRoom(iteration, x.amphipod, x.hallwayIndex))
            .Select(z =>
            {
                var replacementAmphipod = z.amphipod!.Clone(z.amphipod.EnergyUsed, z.amphipod.Status);
                return MoveFromHallwayToRoom(iteration, z.hallwayIndex, replacementAmphipod);

            });
    }

    private static bool CanMoveToRoom(Iteration iteration, Amphipod amphipod, int hallwayIndex)
    {
        return RouteToRoomIsClear(iteration, hallwayIndex, amphipod.Id) &&
               RoomContainsOnlyCorrectOrNull(iteration.Rooms.Single(y => y.letter == amphipod.Id));
    }

    private static Iteration MoveFromHallwayToRoom(Iteration iteration, int hallwayIndex, Amphipod amphipod)
    {
        var distance = GetDistanceToMove(hallwayIndex, amphipod.Id, GetIndexOfFirstSpaceInRoom(iteration, amphipod.Id));
        amphipod.Move(distance);
        
        var newHallway = RemoveAmphipodFromHallway(iteration, hallwayIndex);
        var newRooms = AddAmphipodToRoom(iteration, amphipod);

        return new Iteration { Hallway = newHallway, Rooms = newRooms };
    }

    private static int GetDistanceToMove(int hallwayIndex, string letter, int roomIndex)
    {
        // assumes room index of 0 is at top
        return Math.Abs(DoorwayIndexes[letter] - hallwayIndex) + roomIndex + 1;
    }

    private static (string letter, Amphipod?[] occupants)[] AddAmphipodToRoom(Iteration iteration, Amphipod amphipod)
    {
        var newRooms = new (string letter, Amphipod?[] occupants)[iteration.Rooms.Length];
        for (var i = 0; i < iteration.Rooms.Length; i++)
        {
            newRooms[i] = CloneRoom(iteration, i);
        }
        
        newRooms[amphipod.Id switch
        {
            "A" => 0,
            "B" => 1,
            "C" => 2,
            _ => 3
        }].occupants[GetIndexOfFirstSpaceInRoom(iteration, amphipod.Id)] = amphipod;

        return newRooms;
    }

    private static (string letter, Amphipod?[]) CloneRoom(Iteration iteration, int i)
    {
        return (iteration.Rooms[i].letter, iteration.Rooms[i].occupants.Select(x => x?.Clone(x.EnergyUsed, x.Status)).ToArray());
    }

    private static int GetIndexOfFirstSpaceInRoom(Iteration iteration, string letter)
    {
        return iteration.Rooms[letter switch
        {
            "A" => 0,
            "B" => 1,
            "C" => 2,
            _ => 3
        }].occupants.ToList().FindIndex(0, x => x is null);
    }

    private static Amphipod?[] RemoveAmphipodFromHallway(Iteration iteration, int hallwayIndex)
    {
        var newHallway = new Amphipod?[iteration.Hallway.Length];
        for (var x = 0; x < iteration.Hallway.Length; x++)
        {
            // Relpace amphipod with null
            if (x == hallwayIndex)
            {
                newHallway[x] = null;
            }
            // persist any nulls
            else if (iteration.Hallway[x] is null)
            {
                newHallway[x] = null;
            }
            // clone any other amphipods
            else
            {
                var a = iteration.Hallway[x]!;
                newHallway[x] = a.Clone(a.EnergyUsed, a.Status);
            }
        }

        return newHallway;
    }

    private static bool RoomContainsOnlyCorrectOrNull((string letter, Amphipod?[] occupants) room)
    {
        return room.occupants.All(amphipod => amphipod is null || room.letter == amphipod.Id);
    }

    private static bool RouteToRoomIsClear(Iteration iteration, int hallwayIndex, string letter)
    {
        var roomIndex = DoorwayIndexes[letter];
        
        var traverseLeft = hallwayIndex > roomIndex && iteration.Hallway[roomIndex..hallwayIndex].All(x => x is null);
        var traverseRight = hallwayIndex < roomIndex && iteration.Hallway[(hallwayIndex + 1)..roomIndex].All(x => x is null);
        return traverseRight || traverseLeft;
    }
    
    private static int DistanceFromHallwayToFirstSpaceInRoom(int hallwayIndex, string letter)
    {
        return Math.Abs(hallwayIndex - DoorwayIndexes[letter]) + 1;
    }
    
    private static (string letter, Amphipod?[] occupants)[] InitializeRooms(string[] input)
    {
        var roomRows = input[2..^1];
        var amberRoom = ("A", roomRows.Select(x => CreateAmphipod(x[3])).ToArray());
        var bronzeRoom = ("B", roomRows.Select(x => CreateAmphipod(x[5])).ToArray());
        var copperRoom = ("C", roomRows.Select(x => CreateAmphipod(x[7])).ToArray());
        var desertRoom = ("D", roomRows.Select(x => CreateAmphipod(x[9])).ToArray());

        return new (string letter, Amphipod?[] occupants)[] { amberRoom, bronzeRoom, copperRoom, desertRoom };
    }

    private static Amphipod? CreateAmphipod(char letter)
    {
        return letter switch
        {
            'A' => new Amber(),
            'B' => new Bronze(),
            'C' => new Copper(),
            'D' => new Desert(),
            '.' => null,
            _ => throw new InvalidOperationException($"Invalid letter {letter}")
        };
    }

    private static bool IsComplete(Iteration iteration)
    {
        foreach (var (letter, occupants) in iteration.Rooms)
        {
            switch (letter)
            {
                case "A" when occupants.Any(x => x is not Amber):
                case "B" when occupants.Any(x => x is not Bronze):
                case "C" when occupants.Any(x => x is not Copper):
                case "D" when occupants.Any(x => x is not Desert):
                    return false;
            }
        }
        return true;
    }
    
    public enum Status
    {
        Unmoved,
        InHall,
        InRoom
    }

    public abstract class Amphipod
    {
        private readonly int _energyPerPosition;

        public abstract string Id { get; }
        public int EnergyUsed { get; private set; }
        public Status Status { get; set; }

        protected Amphipod(int energy)
        {
            Status = Status.Unmoved;
            _energyPerPosition = energy;
        }

        protected Amphipod(int energy, int energyUsed, Status status)
        {
            Status = status;
            _energyPerPosition = energy;
            EnergyUsed = energyUsed;
        }
        
        public void Move(int distance)
        {
            if (Status == Status.InRoom) return;
            
            EnergyUsed += distance * _energyPerPosition;
            Status = Status == Status.Unmoved ? Status.InHall : Status.InRoom;
        }

        public abstract Amphipod Clone(int energyStatus, Status status);
    }
    
    public class Amber : Amphipod
    {
        public override string Id => "A";

        public Amber() : base(1)
        {
        }

        private Amber(int energyUsed, Status status) : base(1, energyUsed, status)
        {
        }

        public override Amber Clone(int energyUsed, Status status) => new(energyUsed, status);
    }
    
    public class Bronze : Amphipod
    {
        public override string Id => "B";

        public Bronze() : base(10)
        {
        }

        private Bronze(int energyUsed, Status status) : base(10, energyUsed, status)
        {
        }
        
        public override Bronze Clone(int energyUsed, Status status) => new(energyUsed, status);
    }
    
    public class Copper : Amphipod
    {
        public override string Id => "C";

        public Copper() : base(100)
        {
        }
        
        private Copper(int energyUsed, Status status) : base(100, energyUsed, status)
        {
        }
        
        public override Copper Clone(int energyUsed, Status status) => new(energyUsed, status);
    }
    
    public class Desert : Amphipod
    {
        public override string Id => "D";

        public Desert() : base(1000)
        {
        }
        
        private Desert(int energyUsed, Status status) : base(1000, energyUsed, status)
        {
        }
        
        public override Desert Clone(int energyUsed, Status status) => new(energyUsed, status);
    }
}