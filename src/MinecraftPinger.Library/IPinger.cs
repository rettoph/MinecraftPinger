using MinecraftPinger.Library.Models;
using System.Net;

namespace MinecraftPinger.Library
{
    public interface IPinger<TPong>
        where TPong : class
    {
        Task<Pong<TPong>> PingAsync(IPEndPoint endpoint, int timeout = 1000);
    }
}
