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
        public string LoginTime { get; set; }
        public string LastActiveTime { get; set; }
        public string RemoteEndPoint { get; set; }
        public string ConnectionTime { get; }

        public SessionResult(string sessionId, string username, DateTime loginTime, DateTime lastActiveTime, string remoteEndPoint)
        {
            SessionId = sessionId;
            Username = username;
            LoginTime = loginTime.ToString("yyyy-MM-dd HH:mm:ss");
            LastActiveTime = lastActiveTime.ToString("yyyy-MM-dd HH:mm:ss");
            RemoteEndPoint = remoteEndPoint;
            ConnectionTime = (DateTime.Now - loginTime).ToString(@"d\d\ hh\:mm\:ss");
        }
    }
}
