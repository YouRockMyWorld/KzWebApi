using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KZ.API.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> QueryByIdAsync(object id);
        Task<List<TEntity>> QueryByIdsAsync(object[] ids);
        Task<int> AddAsync(TEntity model);
        Task<bool> DeleteByIdAsync(object id);
        Task<bool> DeleteAsync(TEntity model);
        Task<bool> DeleteByIdsAsync(object[] ids);
        Task<bool> UpdateAsync(TEntity model);
    }
}
