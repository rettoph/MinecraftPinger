using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftPinger.Library.Dtos
{
    public class RaknetProtocolUnconnectedPongDto
    {
        public DateTime Time { get; set; }
        public Int64 ServerGuid { get; set; }
        public Byte[] Magic { get; set; }
        public ServerIdDto ServerId { get; set;}
    }
}
