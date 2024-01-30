namespace MinecraftPinger.Library.Models
{
    public class RaknetProtocolUnconnectedPong
    {
        public DateTime Time { get; set; }
        public long ServerGuid { get; set; }
        public byte[] Magic { get; set; } = Array.Empty<byte>();
        public ServerId ServerId { get; set; } = ServerId.Empty;
    }
}
