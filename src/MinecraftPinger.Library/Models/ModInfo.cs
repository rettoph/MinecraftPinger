using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Models
{
    public sealed record ModInfo
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("modList")]
        public Mod[] List { get; set; } = Array.Empty<Mod>();
    }
}
