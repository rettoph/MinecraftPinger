using MinecraftPinger.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public static class NetworkStreamExtensions
    {
        public static void Write(this NetworkStream stream, OutgoingMessage om)
        {
            byte[] message = om.GetData(true);

            stream.Write(message, 0, message.Length);
        }
    }
}
