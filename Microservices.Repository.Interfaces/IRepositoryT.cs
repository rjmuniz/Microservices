using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {        
        IEnumerable<TEntity> GetAll(string include = "");

        IQueryable<TEntity> Query { get; }


        TEntity GetById(int entityId);

        Task<TEntity> InsertEntityAsync(TEntity entity);
        Task<TEntity> UpdateEntityAsync(int id, TEntity entity);

        Task BeforeSaveAsync(TEntity entity, bool insert);

        Task AfterSaveAsync(TEntity entity, bool insert);

        Task<bool> CanSaveAsync(TEntity entity, bool insert);
        void BeginTransation();
        void RollbackTransation();
        void CommitTransaction();
    }
}
