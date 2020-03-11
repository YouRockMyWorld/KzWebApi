using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KZ.API.Database
{
    public class RedisHelper : IRedisManager
    {
        private volatile ConnectionMultiplexer s_connection;
        private readonly string s_connectionString = "***";
        private readonly object s_objlock = new object();
        private ConnectionMultiplexer GetConnection()
        {
            if (s_connection != null && s_connection.IsConnected) return s_connection;

            lock (s_objlock)
            {
                if (s_connection != null && s_connection.IsConnected) return s_connection;

                if (s_connection != null)
                {
                    s_connection.Dispose();
                }
                try
                {
                    s_connection = ConnectionMultiplexer.Connect(s_connectionString);
                }
                catch
                {
                    s_connection = null;
                }
            }

            return s_connection;
        }
        public ConnectionMultiplexer Connection => GetConnection();
        public async Task<bool> StringSet(int dbIndex, string key, string value, TimeSpan? expiry = null)
        {
            var db = Connection.GetDatabase(dbIndex);
            return await db.StringSetAsync(key, value, expiry);
        }

        public async Task<string> StringGet(int dbIndex, string key)
        {
            var db = Connection.GetDatabase(dbIndex);
            return await db.StringGetAsync(key);
        }
        public async Task<IEnumerable<string>> StringGet(int dbIndex, IEnumerable<string> keys)
        {
            var db = Connection.GetDatabase(dbIndex);
            var qkeys = keys.Select(x => (RedisKey)x).ToArray();
            var result = await db.StringGetAsync(qkeys);
            return result.Select(x => (string)x);
        }
        public async Task<bool> KeyExists(int dbIndex, string key)
        {
            var db = Connection.GetDatabase(dbIndex);
            return await db.KeyExistsAsync(key);
        }

        public async Task<bool> KeyDelete(int dbIndex, string key)
        {
            var db = Connection.GetDatabase(dbIndex);
            return await db.KeyDeleteAsync(key);
        }

        public async Task Subscribe(string channel, Action<string, string> action)
        {
            var sub = Connection.GetSubscriber();
            await sub.SubscribeAsync(channel, (c, v) =>
            {
                action(c, v);
            });
        }

        public async Task UnSubscribe(string channel)
        {
            var sub = Connection.GetSubscriber();
            await sub.UnsubscribeAsync(channel);
        }

        public async Task<IEnumerable<string>> Keys(int dbIndex, string pattern)
        {
            var server = Connection.GetServer("***");
            return await Task.Run<IEnumerable<string>>(() =>
            {
                return server.Keys(dbIndex, pattern, 1000).Select(x => x.ToString());
            });
        }

        
    }
}
