using MinecraftPinger.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Net.Sockets
{
    public static class UdpClientExtensions
    {
        public static int Send(this UdpClient client, OutgoingMessage om)
        {
            byte[] message = om.GetData(false);

            return client.Send(message, message.Length);
        }
        public static int Send(this UdpClient client, OutgoingMessage om, IPEndPoint endpoint)
        {
            byte[] message = om.GetData(false);

            return client.Send(message, message.Length, endpoint);
        }
    }
}
