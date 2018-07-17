//using Microservices.Entities.Common;
//using Microservices.Repository.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Dapper;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace Microservices.Repository.Dapper
//{
//    public class Repository<TEntity> : IRepository<TEntity> where TEntity: IEntity
//    {
//        private readonly DapperConfiguration<TEntity> _dapperConfiguration;
//        private readonly DatabaseConfig _databaseConfig;
//        public Repository(DapperConfiguration<TEntity> dapperConfiguration, DatabaseConfig databaseConfig)
//        {
//            _dapperConfiguration = dapperConfiguration;
//            _databaseConfig = databaseConfig;
//            _databaseConfig.CreateTable(dapperConfiguration).Wait();
//        }

        

//        public async Task<TEntity> AddAsync(TEntity entity)
//        {
//            using (var db = _databaseConfig.GetConnection())
//            {
//                var parameters = new DynamicParameters();
//                foreach (var item in _dapperConfiguration.InsertValues(entity))
//                    parameters.Add(item.Key, item.Value);
//                await db.ExecuteAsync(_dapperConfiguration.Insert, parameters);
//            }
//        }

//        public Task AfterSaveAsync(TEntity entity, bool insert)
//        {
//            throw new NotImplementedException();
//        }

//        public Task BeforeSaveAsync(TEntity entity, bool insert)
//        {
//            throw new NotImplementedException();
//        }

//        public void BeginTransation()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<bool> CanSaveAsync(TEntity entity, bool insert)
//        {
//            throw new NotImplementedException();
//        }

//        public void CommitTransaction()
//        {
//            throw new NotImplementedException();
//        }

//        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
//        {
//            throw new NotImplementedException();
//        }

//        public IQueryable<TEntity> FindAll(string include = "")
//        {
//           using(var db = _databaseConfig.GetConnection())
//            {
//                return db.Query<TEntity>(_dapperConfiguration.FindAll).AsQueryable();
//            }
//        }

//        public Task<TEntity> FindByIdAsync(object entityId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task RemoveAsync(TEntity entity)
//        {
//            throw new NotImplementedException();
//        }

//        public void RollbackTransation()
//        {
//            throw new NotImplementedException();
//        }

//        public Task<TEntity> UpdateAsync(object id, TEntity entity)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
