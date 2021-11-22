using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftPinger.Library.Dtos
{
    public class HandshakeResponseDto
    {
        [JsonProperty(PropertyName = "players")]
        public PlayersDto Players { get; set; }

        [JsonProperty(PropertyName = "version")]
        public Version Version { get; set; }

        [JsonProperty(PropertyName = "description")]
        public ChatDto Description { get; set; }

        [JsonProperty(PropertyName = "modInfo")]
        public ModInfoDto ModInfo { get; set; }

        [JsonProperty(PropertyName = "favicon")]
        public String Icon { get; set; }
    }
}
