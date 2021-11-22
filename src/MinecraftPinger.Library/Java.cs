using MinecraftPinger.Library.Dtos;
using MinecraftPinger.Library.Enums;
using MinecraftPinger.Library.Json.Converters;
using MinecraftPinger.Library.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MinecraftPinger.Library
{
    public class Java
    {
        private String _host;
        private Int32 _port;

        private Java(String host, Int32 port)
        {
            _host = host;
            _port = port;
        }

        public HandshakeResponseDto Ping(Int32 timeout = 1000)
        {
            using (TcpClient tcp = new TcpClient())
            {
                tcp.Client.ReceiveTimeout = timeout;
                tcp.Client.SendTimeout = timeout;

                try
                {
                    if (!(!tcp.ConnectAsync(_host, _port).Wait(timeout)))
                    { // Only do anything if a server is found at that ip
                        using (NetworkStream stream = tcp.GetStream())
                        {
                            this.SendHandshake(stream);
                            this.SendRequest(stream);

                            return this.ReadResponse(stream);
                        }
                    }

                    return default;
                }
                catch (Exception e)
                {
                    return default;
                }
            }
        }

        #region Network Methods
        /// <summary>
        /// Send a "Handshake" packet
        /// http://wiki.vg/Server_List_Ping#Ping_Process
        /// </summary>
        private void SendHandshake(NetworkStream stream)
        {
            var om = new OutgoingMessage(PacketId.JAVA_HANDSHAKE);
            om.WriteVarInt(1);
            om.WriteString(_host);
            om.WriteShort((Int16)_port);
            om.WriteVarInt(1);

            stream.Write(om);
        }

        /// <summary>
        /// Send a "Status Request" packet
        /// http://wiki.vg/Server_List_Ping#Ping_Process
        /// </summary>
        private void SendRequest(NetworkStream stream)
        {
            var om = new OutgoingMessage(PacketId.JAVA_REQUEST);
            stream.Write(om);
        }

        private HandshakeResponseDto ReadResponse(NetworkStream stream)
        {
            var im = new IncomingMessage(stream);

            if (im.Id != PacketId.JAVA_RESPONSE)
                throw new InvalidOperationException();


            var jsonLength = im.ReadVarInt();
            var json = im.ReadString(jsonLength);

            return JsonConvert.DeserializeObject<HandshakeResponseDto>(json, new HandshakeResponseConverter());
        }
        #endregion

        #region Factory Methods
        public static Java Create(String host, Int32 port = 25565)
        {
            return new Java(host, port);
        }
        #endregion
    }
}
