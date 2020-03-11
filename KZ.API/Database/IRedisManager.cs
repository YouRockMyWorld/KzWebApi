using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Database
{
    public interface IRedisManager
    {
        Task<bool> StringSet(int dbIndex, string key, string value, TimeSpan? expiry = null);
        Task<string> StringGet(int dbIndex, string key);
        Task<IEnumerable<string>> StringGet(int dbIndex, IEnumerable<string> keys);
        Task<bool> KeyExists(int dbIndex, string key);
        Task<bool> KeyDelete(int dbIndex, string key);
        Task Subscribe(string channel, Action<string, string> action);
        Task UnSubscribe(string channel);
        Task<IEnumerable<string>> Keys(int dbIndex, string pattern);
    }
}
