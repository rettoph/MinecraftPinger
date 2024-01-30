using MinecraftPinger.Library.Models;
using System.Net;

namespace MinecraftPinger.Library
{
    public static class IPingerExtensions
    {
        public async static Task<Pong<TResponse>[]> PingAsync<TResponse>(this IPinger<TResponse> pinger, IEnumerable<IPEndPoint> endpoints, int timeout = 1000)
            where TResponse : class
        {
            List<Pong<TResponse>> responses = new List<Pong<TResponse>>();
            foreach (IPEndPoint endpoint in endpoints)
            {
                Pong<TResponse> ping = await pinger.PingAsync(endpoint, timeout);
                responses.Add(ping);
            }

            return responses.ToArray();
        }

        public async static Task<Pong<TResponse>> PingAsync<TResponse>(this IPinger<TResponse> pinger, IPEndPoint endpoint, int timeout = 1000)
            where TResponse : class
        {
            return await pinger.PingAsync(endpoint, timeout);
        }

        public async static Task<Pong<TResponse>> PingAsync<TResponse>(this IPinger<TResponse> pinger, string host, int port, int timeout = 1000)
            where TResponse : class
        {
            return await pinger.PingAsync(new IPEndPoint(IPAddress.Parse(host), port), timeout);
        }
    }
}
