using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MinecraftPinger.Library.Utilities
{
    public static class IPEndPointHelper
    {
        public static IPEndPoint GetIPEndPointFromHost(String host, Int32 port, Boolean throwIfMoreThanOneIP)
        {
            var addresses = System.Net.Dns.GetHostAddresses(host);
            if (addresses.Length == 0)
            {
                throw new ArgumentException(
                    "Unable to retrieve address from specified host name.",
                    "hostName"
                );
            }
            else if (throwIfMoreThanOneIP && addresses.Length > 1)
            {
                throw new ArgumentException(
                    "There is more that one IP address to the specified host.",
                    "hostName"
                );
            }
            return new IPEndPoint(addresses[0], port); // Port gets validated here.
        }
    }
}
