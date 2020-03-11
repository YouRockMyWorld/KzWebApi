using KZ.API.Helpers;
using KZ.API.Models.DtoParameters;
using KZ.API.Models.Entities;
using KZ.API.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Repository
{
    public interface IUserRepository : IBaseRepository<KzUser>
    {
        Task<PagedList<KzUser>> GetAllUsersAsync(UserDtoParameters parameters);
        Task<KzUserProfile> GetUserDetailInfoAsync(int userId);
        Task<PagedList<OperationLogResult>> GetUserOperationLogAsync(int userId, OperationLogDtoParameters parameters);
    }
}
