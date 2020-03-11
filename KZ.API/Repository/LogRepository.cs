using SqlSugar;
using KZ.API.Database;
using KZ.API.Helpers;
using KZ.API.Models.DtoParameters;
using KZ.API.Models.Entities;
using KZ.API.Models.Others;
using KZ.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Repository
{
    public class LogRepository : BaseRepository<KzLog>, ILogRepository
    {
        public LogRepository(DbContext<KzLog> dbContext) : base(dbContext)
        {

        }
        public async Task<PagedList<OperationLogResult>> GetOperationLogsAsync(OperationLogDtoParameters parameters)
        {
            try
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException(nameof(parameters));
                }
                RefAsync<int> totalNumber = 0;
                var logs = await _dbContext.Db.Queryable<KzLog, KzLogDetail, KzUser>((l, d, u) => new object[]
                {
                JoinType.Left, l.DetailId == d.DetailId, JoinType.Left, l.UserId == u.UserId
                }).Where((l, d, u) => l.OperationTime > parameters.StartDateTime && l.OperationTime < parameters.EndDateTime)
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
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
            
        }
    }
}
