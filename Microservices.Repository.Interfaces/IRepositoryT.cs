using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Microservices.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> FindByIdAsync(object entityId, string include = "");
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindAll(string include = "");
        Task<int> CountAsync();

        Task AddAsync(TEntity entity);
        Task UpdateAsync(object id, TEntity entity);
        Task RemoveAsync(TEntity entity);


        Task BeforeSaveChangesAsync();

        Task AfterSaveChangesAsync();


        Task<bool> CanSaveAsync(TEntity entity, bool insert);

        Task Commit();
    }
}
