using System.Text;
using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Models
{
    public class Chat
    {
        public static readonly Chat Empty = new Chat();

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("bold")]
        public bool Bold { get; set; }

        [JsonPropertyName("italic")]
        public bool Italic { get; set; }

        [JsonPropertyName("underline")]
        public bool Underline { get; set; }

        [JsonPropertyName("strikethrough")]
        public bool Strikethrough { get; set; }

        [JsonPropertyName("obfuscated")]
        public bool Obfuscated { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;

        [JsonPropertyName("extra")]
        public List<Chat> Extra { get; set; } = [];

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            this.Append(builder);

            return builder.ToString();
        }

        private void Append(StringBuilder builder)
        {
            builder.Append(this.Text);

            foreach (Chat extra in this.Extra)
            {
                extra.Append(builder);
            }
        }
    }
}
