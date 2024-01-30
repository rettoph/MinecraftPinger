using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Models
{
    public class Version
    {
        public static readonly Version Empty = new Version();

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("protocol")]
        public string Protocol { get; set; } = string.Empty;
    }
}
