using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Models
{
    public sealed record Player
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
