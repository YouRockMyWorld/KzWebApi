using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KZ.API.Helpers
{
    public static class IPAddressHelper
    {
        public static string NumberToIpString(long ipNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((ipNumber >> 24) & 0xFF).Append(".");
            sb.Append((ipNumber >> 16) & 0xFF).Append(".");
            sb.Append((ipNumber >> 8) & 0xFF).Append(".");
            sb.Append(ipNumber & 0xFF);
            return sb.ToString();
        }
        public static long IpStringToNumber(string ipString)
        {
            string[] items = ipString.Split('.');
            return long.Parse(items[0]) << 24 | long.Parse(items[1]) << 16 | long.Parse(items[2]) << 8 | long.Parse(items[3]);
        }
    }
}
