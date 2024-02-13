using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Models
{
    public sealed record Players
    {
        public static readonly Players Empty = new Players();

        [JsonPropertyName("max")]
        public int Max { get; set; } = 0;

        [JsonPropertyName("online")]
        public int Online { get; set; } = 0;

        [JsonPropertyName("sample")]
        public Player[] Sample { get; set; } = [];
    }
}
