using System.Text.Json;
using System.Text.Json.Serialization;

namespace MinecraftPinger.Library.Json.Converters
{
    internal class VersionConverter : JsonConverter<Models.Version>
    {
        public override Models.Version? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string name = string.Empty;
            string protocol = string.Empty;

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new Exception();
            }

            reader.Read();

            while (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString() ?? throw new Exception();
                reader.Read();

                switch (propertyName)
                {
                    case nameof(name):
                        name = reader.GetString() ?? throw new Exception();
                        reader.Read();
                        break;
                    case nameof(protocol):
                        protocol = reader.TokenType switch
                        {
                            JsonTokenType.String => reader.GetString(),
                            JsonTokenType.Number => reader.GetSingle().ToString(),
                            _ => string.Empty
                        } ?? throw new Exception();
                        reader.Read();
                        break;
                }
            }

            if (reader.TokenType != JsonTokenType.EndObject)
            {
                throw new Exception();
            }



            return new Models.Version()
            {
                Name = name,
                Protocol = protocol
            };
        }

        public override void Write(Utf8JsonWriter writer, Models.Version value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
