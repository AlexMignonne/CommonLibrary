using System.Net;
using System.Threading;

namespace Common.Helpers
{
    public static class RemoteIpContext
    {
        private static readonly AsyncLocal<IPAddress> IPAddress
            = new AsyncLocal<IPAddress>();

        public static void Set(IPAddress ipAddress) =>
            IPAddress.Value = ipAddress;

        public static IPAddress Get() => IPAddress.Value;
    }
}