using MinecraftPinger.Library.Enums;
using MinecraftPinger.Library.Models;
using System.Net;
using System.Net.Sockets;

namespace MinecraftPinger.Library
{
    public class BedrockPinger : IPinger<RaknetProtocolUnconnectedPong>
    {
        private static readonly Random Rand = new Random();

        public BedrockPinger()
        {
        }

        public Task<Pong<RaknetProtocolUnconnectedPong>> PingAsync(IPEndPoint endpoint, int timeout = 1000)
        {
            throw new NotImplementedException();

            using UdpClient socket = new UdpClient();

            socket.Client.SendTimeout = timeout;
            socket.Client.ReceiveTimeout = timeout;

            this.SendUnconnectedPing(socket, endpoint);

            return Task.FromResult(new Pong<RaknetProtocolUnconnectedPong>()
            {
                Endpoint = endpoint,
                Status = PongStatusEnum.OK,
                Content = this.ReadUnconnectedPong(socket, endpoint)
            });
        }

        #region Packet Methods
        /// <summary>
        /// https://wiki.vg/Raknet_Protocol#Unconnected_Ping
        /// </summary>
        /// <param name="udp"></param>
        private void SendUnconnectedPing(UdpClient udp, IPEndPoint endpoint)
        {
            OutgoingMessage om = new OutgoingMessage(PacketId.RAKNET_PROTOCOL_UNCONNECTED_PING);
            om.WriteLong(DateTime.Now.Ticks);
            om.WriteMagic();

            byte[] clientGuid = new byte[8];
            BedrockPinger.Rand.NextBytes(clientGuid);

            om.WriteBytes(clientGuid);

            udp.Send(om, endpoint);
        }

        /// <summary>
        /// https://wiki.vg/Raknet_Protocol#Unconnected_Pong
        /// </summary>
        /// <param name="udp"></param>
        private RaknetProtocolUnconnectedPong ReadUnconnectedPong(UdpClient udp, IPEndPoint endpoint)
        {
            var im = new IncomingMessage(udp.Receive(ref endpoint));

            if (im.Id != PacketId.RAKNET_PROTOCOL_UNCONNECTED_PONG)
                throw new InvalidOperationException();

            DateTime time = new DateTime(im.ReadLong());
            long serverGuid = im.ReadLong();
            byte[] magic = im.ReadMagic();

            short serverStringIdLength = im.ReadShort();
            string[] serverStringId = im.ReadString((int)serverStringIdLength).Split(';');

            return new RaknetProtocolUnconnectedPong()
            {
                Time = time,
                ServerGuid = serverGuid,
                Magic = magic,
                ServerId = new ServerId()
                {
                    Edition = serverStringId[0],
                    MessageOfTheDayLineOne = serverStringId[1],
                    ProtocolVersion = serverStringId[2],
                    VersionName = serverStringId[3],
                    PlayerCount = int.Parse(serverStringId[4]),
                    MaxPlayerCount = int.Parse(serverStringId[5]),
                    ServerUniqueId = serverStringId[6],
                    MessageOfTheDayLineTwo = serverStringId[7],
                    GameMode = serverStringId[8],
                    GameModeNumeric = int.Parse(serverStringId[9]),
                    IPv4Port = int.Parse(serverStringId[10]),
                    IPv6Port = int.Parse(serverStringId[11]),
                }
            };
        }
        #endregion
    }
}
