using MinecraftPinger.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public static class UdpClientExtensions
    {
        public static Int32 Send(this UdpClient client, OutgoingMessage om)
        {
            Byte[] message = om.GetData(false);

            return client.Send(message, message.Length);
        }
        public static Int32 Send(this UdpClient client, OutgoingMessage om, IPEndPoint endpoint)
        {
            Byte[] message = om.GetData(false);

            return client.Send(message, message.Length, endpoint);
        }
    }
}
