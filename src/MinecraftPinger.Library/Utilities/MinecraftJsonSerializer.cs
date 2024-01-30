using MinecraftPinger.Library.Json.Converters;
using System.Text.Json;

namespace MinecraftPinger.Library.Utilities
{
    public static class MinecraftJsonSerializer
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions()
        {
            Converters = {
                new ChatConverter(),
                new VersionConverter()
            }
        };

        public static string Serialize<T>(T data)
        {
            return JsonSerializer.Serialize(data, _jsonOptions);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, _jsonOptions) ?? throw new Exception();
        }
    }
}
