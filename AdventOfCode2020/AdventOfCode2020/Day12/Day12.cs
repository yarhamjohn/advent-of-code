using System;
using System.Collections.Generic;

namespace AdventOfCode2020.Day12
{
    public static class Day12
    {
        public static int CalculateManhattanDistance(List<string> input)
        {
            var direction = Direction.East;
            var eastWest = 0;
            var northSouth = 0;

            foreach (var instruction in input)
            {
                var (move, distance) = ParseInput(instruction);
                switch (move)
                {
                    case 'E':
                        eastWest += distance;
                        break;
                    case 'W':
                        eastWest -= distance;
                        break;
                    case 'N':
                        northSouth += distance;
                        break;
                    case 'S':
                        northSouth -= distance;
                        break;
                    case 'L':
                        direction = RotateLeft(direction, distance);
                        break;
                    case 'R':
                        direction = RotateRight(direction, distance);
                        break;
                    case 'F' when direction == Direction.East:
                        eastWest += distance;
                        break;
                    case 'F' when direction == Direction.West:
                        eastWest -= distance;
                        break;
                    case 'F' when direction == Direction.North:
                        northSouth += distance;
                        break;
                    case 'F' when direction == Direction.South:
                        northSouth -= distance;
                        break;
                    default: throw new InvalidOperationException($"Not a valid movement {move}");
                }
            }

            return Math.Abs(eastWest) + Math.Abs(northSouth);
        }

        public static long CalculateManhattanDistanceUsingWaypoint(List<string> input)
        {
            var eastWest = 0L;
            var northSouth = 0L;
            var waypointEastWest = 10L;
            var waypointNorthSouth = 1L;

            foreach (var instruction in input)
            {
                var (move, distance) = ParseInput(instruction);
                switch (move)
                {
                    case 'E':
                        waypointEastWest += distance;
                        break;
                    case 'W':
                        waypointEastWest -= distance;
                        break;
                    case 'N':
                        waypointNorthSouth += distance;
                        break;
                    case 'S':
                        waypointNorthSouth -= distance;
                        break;
                    case 'L':
                        (waypointEastWest, waypointNorthSouth) =
                            MoveWaypointLeft(waypointEastWest, waypointNorthSouth, distance);
                        break;
                    case 'R':
                        (waypointEastWest, waypointNorthSouth) =
                            MoveWaypointRight(waypointEastWest, waypointNorthSouth, distance);
                        break;
                    case 'F':
                        (eastWest, northSouth) = MoveShipForward(eastWest,
                            northSouth, waypointEastWest, waypointNorthSouth, distance);
                        break;
                    default: throw new InvalidOperationException($"Not a valid movement {move}");
                }
            }

            return Math.Abs(eastWest) + Math.Abs(northSouth);
        }

        private static (long eastWest, long northSouth) MoveShipForward(
            long eastWest, long northSouth, long waypointEastWest, long waypointNorthSouth, long distance)
        {
            var newShipEastWest = eastWest + waypointEastWest * distance;
            var newShipNorthSouth = northSouth + waypointNorthSouth * distance;
            return (newShipEastWest, newShipNorthSouth);
        }

        private static (long waypointEastWest, long waypointNorthSouth) MoveWaypointLeft(long waypointEastWest,
            long waypointNorthSouth, long distance)
        {
            return distance switch
            {
                90 => (-waypointNorthSouth, waypointEastWest),
                180 => (-waypointEastWest, -waypointNorthSouth),
                270 => (waypointNorthSouth, -waypointEastWest),
                _ => throw new InvalidOperationException("Invalid distance")
            };
        }

        private static (long waypointEastWest, long waypointNorthSouth) MoveWaypointRight(long waypointEastWest,
            long waypointNorthSouth, long distance)
        {
            return distance switch
            {
                90 => (waypointNorthSouth, -waypointEastWest),
                180 => (-waypointEastWest, -waypointNorthSouth),
                270 => (-waypointNorthSouth, waypointEastWest),
                _ => throw new InvalidOperationException("Invalid distance")
            };
        }

        private static Direction RotateRight(Direction direction, int distance)
        {
            return direction switch
            {
                Direction.East when distance == 90 => Direction.South,
                Direction.East when distance == 180 => Direction.West,
                Direction.East when distance == 270 => Direction.North,
                Direction.West when distance == 90 => Direction.North,
                Direction.West when distance == 180 => Direction.East,
                Direction.West when distance == 270 => Direction.South,
                Direction.North when distance == 90 => Direction.East,
                Direction.North when distance == 180 => Direction.South,
                Direction.North when distance == 270 => Direction.West,
                Direction.South when distance == 90 => Direction.West,
                Direction.South when distance == 180 => Direction.North,
                Direction.South when distance == 270 => Direction.East,
                _ => throw new InvalidOperationException("Not a valid rotation")
            };
        }

        private static Direction RotateLeft(Direction direction, int distance)
        {
            return direction switch
            {
                Direction.East when distance == 90 => Direction.North,
                Direction.East when distance == 180 => Direction.West,
                Direction.East when distance == 270 => Direction.South,
                Direction.West when distance == 90 => Direction.South,
                Direction.West when distance == 180 => Direction.East,
                Direction.West when distance == 270 => Direction.North,
                Direction.North when distance == 90 => Direction.West,
                Direction.North when distance == 180 => Direction.South,
                Direction.North when distance == 270 => Direction.East,
                Direction.South when distance == 90 => Direction.East,
                Direction.South when distance == 180 => Direction.North,
                Direction.South when distance == 270 => Direction.West,
                _ => throw new InvalidOperationException("Not a valid rotation")
            };
        }

        private static (char, int) ParseInput(string instruction)
        {
            return (instruction[0], Convert.ToInt32(instruction.Substring(1)));
        }
    }

    public enum Direction
    {
        East,
        West,
        North,
        South
    }
}