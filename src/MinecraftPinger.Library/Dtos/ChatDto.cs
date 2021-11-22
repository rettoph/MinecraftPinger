using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftPinger.Library.Dtos
{
    public class ChatDto
    {
        [JsonProperty(PropertyName = "text")]
        public String Text { get; set; }

        [JsonProperty(PropertyName = "bold")]
        public Boolean Bold { get; set; }

        [JsonProperty(PropertyName = "italic")]
        public Boolean Italic { get; set; }

        [JsonProperty(PropertyName = "underline")]
        public Boolean Underline { get; set; }

        [JsonProperty(PropertyName = "strikethrough")]
        public Boolean Strikethrough { get; set; }

        [JsonProperty(PropertyName = "obfuscated")]
        public Boolean Obfuscated { get; set; }

        [JsonProperty(PropertyName = "color")]
        public String Color { get; set; }

        [JsonProperty(PropertyName = "extra")]
        public List<ChatDto> Extra { get; set; }
    }
}
