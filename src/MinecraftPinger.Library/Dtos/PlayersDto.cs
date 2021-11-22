using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftPinger.Library.Dtos
{
    public class PlayersDto
    {
        [JsonProperty(PropertyName = "max")]
        public Int32 Max { get; set; }

        [JsonProperty(PropertyName = "online")]
        public Int32 Online { get; set; }

        [JsonProperty(PropertyName = "sample")]
        public List<PlayerDto> Sample { get; set; }

        public PlayersDto()
        {
            this.Sample = new List<PlayerDto>();
        }
    }
}
