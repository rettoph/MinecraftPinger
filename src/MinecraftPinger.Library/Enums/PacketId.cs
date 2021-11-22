using System;
using System.Collections.Generic;
using System.Text;

namespace MinecraftPinger.Library.Enums
{
    public enum PacketId : Byte
    {
        /// <summary>
        /// https://wiki.vg/Raknet_Protocol#Unconnected_Ping
        /// </summary>
        RAKNET_PROTOCOL_UNCONNECTED_PING = 0x01,

        /// <summary>
        /// https://wiki.vg/Raknet_Protocol#Unconnected_Ping
        /// </summary>
        RAKNET_PROTOCOL_UNCONNECTED_PING_2 = 0x02,

        /// <summary>
        /// https://wiki.vg/Raknet_Protocol#Unconnected_Pong
        /// </summary>
        RAKNET_PROTOCOL_UNCONNECTED_PONG = 0x1c,

        /// <summary>
        /// https://wiki.vg/Server_List_Ping#Handshake
        /// </summary>
        JAVA_HANDSHAKE = 0x00,

        /// <summary>
        /// https://wiki.vg/Server_List_Ping#Request
        /// </summary>
        JAVA_REQUEST = 0x00,

        /// <summary>
        /// https://wiki.vg/Server_List_Ping#Response
        /// </summary>
        JAVA_RESPONSE = 0x00,
    }
}
