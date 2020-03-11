using KZ.API.Database;
using KZ.API.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IRedisManager _redisManager;

        public SessionRepository(IRedisManager redisManager)
        {
            _redisManager = redisManager;
        }

        public async Task<bool> DeleteByUsernameAsync(string username)
        {
            if(!await _redisManager.KeyExists(0, username))
            {
                return false;
            }
            return await _redisManager.KeyDelete(0, username);
        }

        public async Task<IEnumerable<SessionResult>> GetAllSessionsAsync()
        {
            var keys = await _redisManager.Keys(0, "*");
            var values = await _redisManager.StringGet(0, keys);
            return values.Select<string, SessionResult>(s =>
            {
                string[] args = s.Split('|');
                return new SessionResult
                {
                    SessionId = args[0],
                    Username = args[1],
                    LoginTime = DateTime.Parse(args[2]),
                    LastActiveTime = DateTime.Parse(args[3]),
                    RemoteEndPoint = args[4]
                };
            });
        }
    }
}
