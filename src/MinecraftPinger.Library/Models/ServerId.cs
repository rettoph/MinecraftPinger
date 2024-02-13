namespace MinecraftPinger.Library.Models
{
    public sealed record ServerId
    {
        public static readonly ServerId Empty = new ServerId();

        public string Edition { get; set; } = string.Empty;
        public string MessageOfTheDayLineOne { get; set; } = string.Empty;
        public string ProtocolVersion { get; set; } = string.Empty;
        public string VersionName { get; set; } = string.Empty;
        public int PlayerCount { get; set; }
        public int MaxPlayerCount { get; set; }
        public string ServerUniqueId { get; set; } = string.Empty;
        public string MessageOfTheDayLineTwo { get; set; } = string.Empty;
        public string GameMode { get; set; } = string.Empty;
        public int GameModeNumeric { get; set; }
        public int IPv4Port { get; set; }
        public int IPv6Port { get; set; }
    }
}
