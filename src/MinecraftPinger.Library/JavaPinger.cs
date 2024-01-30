using MinecraftPinger.Library.Enums;
using MinecraftPinger.Library.Models;
using MinecraftPinger.Library.Utilities;
using System.Net;
using System.Net.Sockets;

namespace MinecraftPinger.Library
{
    public class JavaPinger : IPinger<HandshakeResponse>
    {
        public JavaPinger()
        {
        }

        public async Task<Pong<HandshakeResponse>> PingAsync(IPEndPoint endpoint, int timeout = 1000)
        {
            try
            {
                using TcpClient socket = new TcpClient();
                using CancellationTokenSource cts = new CancellationTokenSource(timeout);

                TaskCompletionSource<bool> cancellationCompletionSource = new TaskCompletionSource<bool>();
                Task connect = socket.ConnectAsync(endpoint);

                using (cts.Token.Register(() => cancellationCompletionSource.TrySetResult(true)))
                {
                    if (connect != await Task.WhenAny(connect, cancellationCompletionSource.Task))
                    {
                        return new Pong<HandshakeResponse>()
                        {
                            Endpoint = endpoint,
                            Status = PongStatusEnum.Timeout,
                            Content = null
                        };
                    }
                }

                using (NetworkStream stream = socket.GetStream())
                {
                    this.SendHandshake(stream, endpoint);
                    this.SendRequest(stream);

                    return new Pong<HandshakeResponse>()
                    {
                        Endpoint = endpoint,
                        Status = PongStatusEnum.OK,
                        Content = this.ReadResponse(stream)
                    };
                }
            }
            catch (Exception ex)
            {
                return new Pong<HandshakeResponse>()
                {
                    Endpoint = endpoint,
                    Status = PongStatusEnum.Exception,
                    Content = null,
                    Exception = ex
                };
            }
        }

        #region Network Methods
        /// <summary>
        /// Send a "Handshake" packet
        /// http://wiki.vg/Server_List_Ping#Ping_Process
        /// </summary>
        private void SendHandshake(NetworkStream stream, IPEndPoint endpoint)
        {
            var om = new OutgoingMessage(PacketId.JAVA_HANDSHAKE);
            om.WriteVarInt(1);
            om.WriteString(endpoint.Address.ToString());
            om.WriteShort((short)endpoint.Port);
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

        private HandshakeResponse ReadResponse(NetworkStream stream)
        {
            var im = new IncomingMessage(stream);

            if (im.Id != PacketId.JAVA_RESPONSE)
                throw new InvalidOperationException();


            var jsonLength = im.ReadVarInt();
            var json = im.ReadString(jsonLength);

            return MinecraftJsonSerializer.Deserialize<HandshakeResponse>(json);
        }
        #endregion
    }
}
