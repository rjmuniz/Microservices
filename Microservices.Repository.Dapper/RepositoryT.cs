using Microservices.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Repository.Dapper
{
    public class Repository<TEntity> : IRepository<TEntity>
    {
        public IQueryable<TEntity> Query => throw new NotImplementedException();

        public Task AfterSaveAsync(TEntity entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public Task BeforeSaveAsync(TEntity entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public void BeginTransation()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanSaveAsync(TEntity entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll(string include = "")
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransation()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateEntityAsync(int id, TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
