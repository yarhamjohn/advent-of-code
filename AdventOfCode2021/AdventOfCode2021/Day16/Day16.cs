using System.Text;

namespace AdventOfCode2021.Day16;

public static class Day16
{
    public static readonly Dictionary<string, string> HexBinaryMapping = new()
    {
        { "0", "0000" },
        { "1", "0001" },
        { "2", "0010" },
        { "3", "0011" },
        { "4", "0100" },
        { "5", "0101" },
        { "6", "0110" },
        { "7", "0111" },
        { "8", "1000" },
        { "9", "1001" },
        { "A", "1010" },
        { "B", "1011" },
        { "C", "1100" },
        { "D", "1101" },
        { "E", "1110" },
        { "F", "1111" },
    };
    
    public static long CalculateVersionSum(string input)
    {
        var binary = string.Join("", input.ToArray().Select(x => HexBinaryMapping[x.ToString()]));

        var (packet, _) = PacketFactory.CreatePacket(binary);
        
        return packet.CalculateVersionTotal();
    }
    
    public static long CalculateValue(string input)
    {
        var binary = string.Join("", input.ToArray().Select(x => HexBinaryMapping[x.ToString().ToUpper()]));

        var (packet, _) = PacketFactory.CreatePacket(binary);
        
        return packet.CalculateValue();
    }
    
    private static class PacketFactory
    {
        public static (Packet, string) CreatePacket(string binaryInput)
        {
            var (version, typeId) = GetPacketHeader(binaryInput);
            if (typeId == 4)
            {
                var (value, leftOver) = ParseLiteralPacket(binaryInput[6..]);
                return (new LiteralPacket(version, typeId, value), leftOver);
            }

            var lengthTypeId = binaryInput.Substring(6, 1);
            var subPackets = new List<Packet>();
            if (lengthTypeId == "0")
            {
                var packetLength = Convert.ToInt32(binaryInput.Substring(7, 15), 2);
                var leftOver = binaryInput.Substring(22, packetLength);
                while (leftOver.Length > 0)
                {
                    var (packet, item2) = CreatePacket(leftOver);
                    subPackets.Add(packet);
                    leftOver = item2;
                }

                return (new OperatorPacket(version, typeId, subPackets), binaryInput[(22 + packetLength)..]);
            }
            
            var numPackets = Convert.ToInt32(binaryInput.Substring(7, 11), 2);
            var remaining = binaryInput[18..];
            for (var i = 0; i < numPackets; i++)
            {
                var (packet, item2) = CreatePacket(remaining);
                subPackets.Add(packet);
                remaining = item2;
            }
            
            return (new OperatorPacket(version, typeId, subPackets), remaining);
        }

        private static (long, string) ParseLiteralPacket(string input)
        {
            var result = new StringBuilder();
            var leftOver = "";
            
            for (var i = 0; i < input.Length; i += 5)
            {
                var startChar = input[i];
                result.Append(input.AsSpan(i + 1, 4));

                if (startChar == '0')
                {
                    leftOver = input[(i + 5)..];
                    break;
                }
            }

            return (Convert.ToInt64(result.ToString(), 2), leftOver);
        }

        private static (int version, int typeId) GetPacketHeader(string binaryInput)
        {
            var version = Convert.ToInt32(binaryInput[..3], 2);
            var typeId = Convert.ToInt32(binaryInput.Substring(3, 3), 2);
            
            return (version, typeId);
        }
    }

    private abstract class Packet
    {
        protected int Version { get; }
        protected int TypeId { get; }

        protected Packet(int version, int typeId)
        {
            Version = version;
            TypeId = typeId;
        }

        public abstract int CalculateVersionTotal();
        public abstract long CalculateValue();
    }

    private class LiteralPacket : Packet
    {
        private long Value { get; }
        
        public LiteralPacket(int version, int typeId, long value) : base(version, typeId)
        {
            Value = value;
        }
        
        public override int CalculateVersionTotal() => Version;
        public override long CalculateValue() => Value;
    }

    private class OperatorPacket : Packet
    {
        private List<Packet> SubPackets { get; }

        public OperatorPacket(int version, int typeId, List<Packet> subPackets) : base(version, typeId)
        {
            SubPackets = subPackets;
        }
        
        public override int CalculateVersionTotal() => Version + SubPackets.Sum(x => x.CalculateVersionTotal());

        public override long CalculateValue()
        {
            return TypeId switch
            {
                0 => SubPackets.Sum(x => x.CalculateValue()),
                1 => SubPackets.Aggregate(1L, (current, next) => current * next.CalculateValue()),
                2 => SubPackets.Min(x => x.CalculateValue()),
                3 => SubPackets.Max(x => x.CalculateValue()),
                5 => SubPackets.First().CalculateValue() > SubPackets.Last().CalculateValue() ? 1 : 0,
                6 => SubPackets.First().CalculateValue() < SubPackets.Last().CalculateValue() ? 1 : 0,
                7 => SubPackets.First().CalculateValue() == SubPackets.Last().CalculateValue() ? 1 : 0,
                _ => throw new Exception()
            };
        }
    }
}