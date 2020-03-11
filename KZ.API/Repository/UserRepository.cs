using SqlSugar;
using KZ.API.Database;
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
    public class UserRepository : BaseRepository<KzUser>, IUserRepository
    {
        public UserRepository(DbContext<KzUser> dbContext):base(dbContext)
        {

        }

        public async Task<PagedList<KzUser>> GetAllUsersAsync(UserDtoParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            RefAsync<int> totalNumber = 0;
            var users = await _dbContext.Db.Queryable<KzUser>().ToPageListAsync(parameters.PageNumber, parameters.PageSize, totalNumber);
            return new PagedList<KzUser>(users, (int)totalNumber, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<KzUserProfile> GetUserDetailInfoAsync(int userId)
        {
            //throw new NotImplementedException();
            return await _dbContext.Db.Queryable<KzUserProfile>().Where(x => x.UserId == userId).SingleAsync();
        }

        public async Task<PagedList<OperationLogResult>> GetUserOperationLogAsync(int userId, OperationLogDtoParameters parameters)
        {
            if (parameters == null) 
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            RefAsync<int> totalNumber = 0;
            var logs = await _dbContext.Db.Queryable<KzLog, KzLogDetail, KzUser>((l, d, u) => new object[]
            {
                JoinType.Left, l.DetailId == d.DetailId, JoinType.Left, l.UserId == u.UserId
            }).Where((l, d, u) => l.UserId == userId && l.OperationTime > parameters.StartDateTime && l.OperationTime < parameters.EndDateTime)
            .Select((l, d, u) => new OperationLogResult
            {
                UserId = l.UserId,
                Username = u.Username,
                Module = l.Module,
                LogType = l.LogType,
                OperationTime = l.OperationTime,
                IPAddress = l.IPAddress,
                Details = d.Details
            })
            .ToPageListAsync(parameters.PageNumber, parameters.PageSize, totalNumber);
            return new PagedList<OperationLogResult>(logs, (int)totalNumber, parameters.PageNumber, parameters.PageSize);
        }
    }
}
