using MinecraftPinger.Library.Enums;
using System.Net;

namespace MinecraftPinger.Library.Models
{
    public sealed record Pong<T>
    {
        public required IPEndPoint Endpoint { get; init; }

        public required PongStatusEnum Status { get; init; }

        public required T? Content { get; init; }

        public Exception? Exception { get; init; }
    }
}
