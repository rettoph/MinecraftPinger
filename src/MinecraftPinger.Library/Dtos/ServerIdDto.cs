using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftPinger.Library.Dtos
{
    public class ServerIdDto
    {
        public String Edition { get; set; }
        public String MessageOfTheDayLineOne { get; set; }
        public String ProtocolVersion { get; set; }
        public String VersionName { get; set; }
        public Int32 PlayerCount { get; set; }
        public Int32 MaxPlayerCount { get; set; }
        public String ServerUniqueId { get; set; }
        public String MessageOfTheDayLineTwo { get; set; }
        public String GameMode { get; set; }
        public Int32 GameModeNumeric { get; set; }
        public Int32 IPv4Port { get; set; }
        public Int32 IPv6Port { get; set; }
    }
}
