using KZ.API.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Repository
{
    public interface ISessionRepository
    {
        Task<IEnumerable<SessionResult>> GetAllSessionsAsync();
        Task<bool> DeleteByUsernameAsync(string username);
    }
}
