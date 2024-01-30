using MinecraftPinger.Library.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Json.Converters
{
    internal class ChatConverter : JsonConverter<Chat>
    {
        public override Chat? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return new Chat()
                {
                    Text = reader.GetString() ?? string.Empty
                };
            }

            return JsonSerializer.Deserialize<Chat>(ref reader);
        }

        public override void Write(Utf8JsonWriter writer, Chat value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize<Chat>(writer, value);
        }
    }
}
