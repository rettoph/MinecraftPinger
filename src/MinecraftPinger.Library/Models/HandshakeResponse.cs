using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Models
{
    public sealed record HandshakeResponse
    {
        public static readonly HandshakeResponse Empty = new HandshakeResponse();

        [JsonPropertyName("players")]
        public Players Players { get; set; } = Players.Empty;

        [JsonPropertyName("version")]
        public Version Version { get; set; } = Version.Empty;

        [JsonPropertyName("description")]
        public Chat Description { get; set; } = Chat.Empty;

        [JsonPropertyName("modinfo")]
        public ModInfo? ModInfo { get; set; } = null;

        [JsonPropertyName("modpackData")]
        public ModPackData? ModPackData { get; set; } = null;

        [JsonPropertyName("favicon")]
        public string Icon { get; set; } = string.Empty;
    }
}
