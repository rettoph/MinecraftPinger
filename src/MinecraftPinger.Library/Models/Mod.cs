using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Models
{
    public class Mod
    {
        [JsonPropertyName("modid")]
        public string ModId { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
    }
}
