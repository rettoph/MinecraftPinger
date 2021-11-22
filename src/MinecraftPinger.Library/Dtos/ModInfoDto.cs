using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftPinger.Library.Dtos
{
    public class ModInfoDto
    {
        [JsonProperty(PropertyName = "type")]
        public String Type { get; set; }

        [JsonProperty(PropertyName = "modList")]
        public ModDto[] List { get; set; }
    }
}
