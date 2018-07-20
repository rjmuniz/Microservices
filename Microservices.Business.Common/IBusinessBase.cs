using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Business.Common
{
    public interface IBusinessBase<TEntity>
    {
        Task<IQueryable<TEntity>> FindAllAsync();
        Task<int> CountAsync();
        Task<IQueryable<TEntity>> FindAllActivesAsync();
        Task<TEntity> FindByIdAsync(object entityId);

        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateEntityAsync(object id, TEntity entity);
        Task RemoveAsync(object entityId);

        Task BeforeAddAsync(TEntity entity, bool insert);
        Task AfterAddedAsync(TEntity entity, bool insert);
        Task<bool> CanAddAsync(TEntity entity, bool insert);
        Task BeforeUpdateAsync(TEntity entity, bool insert);
        Task AfterUpdatedAsync(TEntity entity, bool insert);
        Task<bool> CanUpdateAsync(TEntity entity, bool insert);
    }
}
