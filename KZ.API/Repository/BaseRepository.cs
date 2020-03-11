using KZ.API.Database;
using KZ.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DbContext<TEntity> _dbContext;
        public BaseRepository(DbContext<TEntity> dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }
        public async Task<int> AddAsync(TEntity model)
        {
            return await _dbContext.Db.Insertable<TEntity>(model).ExecuteReturnIdentityAsync();
        }

        public async Task<bool> DeleteAsync(TEntity model)
        {
            return await _dbContext.Db.Deleteable<TEntity>(model).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> DeleteByIdAsync(object id)
        {
            return await _dbContext.Db.Deleteable<TEntity>(id).ExecuteCommandHasChangeAsync();
        }

        public async Task<bool> DeleteByIdsAsync(object[] ids)
        {
            return await _dbContext.Db.Deleteable<TEntity>(ids).ExecuteCommandHasChangeAsync();
        }

        public async Task<TEntity> QueryByIdAsync(object id)
        {
            return await _dbContext.Db.Queryable<TEntity>().In(id).SingleAsync();
        }

        public async Task<List<TEntity>> QueryByIdsAsync(object[] ids)
        {
            return await _dbContext.Db.Queryable<TEntity>().In(ids).ToListAsync();
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            return await _dbContext.Db.Updateable<TEntity>(model).ExecuteCommandHasChangeAsync();
        }
    }
}
