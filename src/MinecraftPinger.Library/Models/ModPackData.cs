using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Models
{
    public sealed record ModPackData
    {
        [JsonPropertyName("projectID")]
        public int ProjectId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("versionID")]
        public int VersionId { get; set; }

        [JsonPropertyName("isMetadata")]
        public bool IsMetadata { get; set; }
    }
}
