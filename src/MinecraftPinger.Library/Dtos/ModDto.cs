using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftPinger.Library.Dtos
{
    public class ModDto
    {
        [JsonProperty(PropertyName = "modid")]
        public String ModId { get; set; }

        [JsonProperty(PropertyName = "version")]
        public String Version { get; set; }
    }
}
