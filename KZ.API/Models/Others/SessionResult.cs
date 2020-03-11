using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Models.Others
{
    public class SessionResult
    {
        public string SessionId { get; set; }
        public string Username { get; set; }
        public DateTime LoginTime { get; set; }
        public DateTime LastActiveTime { get; set; }
        public string RemoteEndPoint { get; set; }
        public string ConnectionTime => (DateTime.Now - LoginTime).ToString();
    }
}
