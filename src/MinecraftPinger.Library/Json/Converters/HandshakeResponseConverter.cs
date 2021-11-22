using MinecraftPinger.Library.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftPinger.Library.Json.Converters
{
    /// <summary>
    /// Used to convert the Handshake response
    /// json string into a valid HandshakeResponse
    /// object.
    /// </summary>
    public class HandshakeResponseConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(HandshakeResponseDto);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);
            
            if (!(jObject["description"] is JObject))
            { // If the description is a string we must convert it before doing anything
                var newValue = new JObject();
                newValue["Text"] = jObject["description"];
                jObject["description"] = newValue;
            }

            HandshakeResponseDto response = new HandshakeResponseDto();
            serializer.Populate(jObject.CreateReader(), response);

            return response;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
