using MinecraftPinger.Library.Dtos;
using MinecraftPinger.Library.Enums;
using MinecraftPinger.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MinecraftPinger.Library
{
    public class Bedrock
    {
        private static readonly Random Rand = new Random();
        private IPEndPoint _endpoint;

        private Bedrock(IPEndPoint endpoint)
        {
            _endpoint = endpoint;
        }

        public RaknetProtocolUnconnectedPongDto Ping(Int32 timeout = 1000)
        {
            using (UdpClient udp = new UdpClient())
            {
                udp.Client.ReceiveTimeout = timeout;
                udp.Client.SendTimeout = timeout;

                try
                {
                    this.SendUnconnectedPing(udp);

                    return this.ReadUnconnectedPong(udp);
                }
                catch (Exception e)
                {
                    return default;
                }
            }
        }

        #region Packet Methods
        /// <summary>
        /// https://wiki.vg/Raknet_Protocol#Unconnected_Ping
        /// </summary>
        /// <param name="udp"></param>
        private void SendUnconnectedPing(UdpClient udp)
        {
            OutgoingMessage om = new OutgoingMessage(PacketId.RAKNET_PROTOCOL_UNCONNECTED_PING);
            om.WriteLong(DateTime.Now.Ticks);
            om.WriteMagic();

            Byte[] clientGuid = new Byte[8];
            Bedrock.Rand.NextBytes(clientGuid);

            om.WriteBytes(clientGuid);

            udp.Send(om, _endpoint);
        }

        /// <summary>
        /// https://wiki.vg/Raknet_Protocol#Unconnected_Pong
        /// </summary>
        /// <param name="udp"></param>
        private RaknetProtocolUnconnectedPongDto ReadUnconnectedPong(UdpClient udp)
        {
            var im = new IncomingMessage(udp.Receive(ref _endpoint));

            if (im.Id != PacketId.RAKNET_PROTOCOL_UNCONNECTED_PONG)
                throw new InvalidOperationException();

            DateTime time = new DateTime(im.ReadLong());
            Int64 serverGuid = im.ReadLong();
            Byte[] magic = im.ReadMagic();

            Int16 serverStringIdLength = im.ReadShort();
            String[] serverStringId = im.ReadString((Int32)serverStringIdLength).Split(';');

            return new RaknetProtocolUnconnectedPongDto()
            {
                Time = time,
                ServerGuid = serverGuid,
                Magic = magic,
                ServerId = new ServerIdDto()
                {
                    Edition = serverStringId[0],
                    MessageOfTheDayLineOne = serverStringId[1],
                    ProtocolVersion = serverStringId[2],
                    VersionName = serverStringId[3],
                    PlayerCount = Int32.Parse(serverStringId[4]),
                    MaxPlayerCount = Int32.Parse(serverStringId[5]),
                    ServerUniqueId = serverStringId[6],
                    MessageOfTheDayLineTwo = serverStringId[7],
                    GameMode = serverStringId[8],
                    GameModeNumeric = Int32.Parse(serverStringId[9]),
                    IPv4Port = Int32.Parse(serverStringId[10]),
                    IPv6Port = Int32.Parse(serverStringId[11]),
                }
            };
        }
        #endregion

        #region Factory Methods
        public static Bedrock Create(String host, Int32 port = 19132)
        {
            return Bedrock.Create(IPEndPointHelper.GetIPEndPointFromHost(host, port, true));
        }

        public static Bedrock Create(IPEndPoint ep)
        {
            return new Bedrock(ep);
        }
        #endregion
    }
}
