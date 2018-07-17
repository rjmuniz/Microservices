using Dapper;
using Microservices.Entities;
using Microservices.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Repository.Dapper
{
    public class RepositoryUsuario : IRepository<Usuario>
    {
        private const string TableName = "Usuario";

        private readonly DatabaseConfig _databaseConfig;

        public RepositoryUsuario(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
            _databaseConfig = databaseConfig;
            _databaseConfig.CreateTable(TableName,
                "CREATE TABLE Usuario (Id UNIQUEIDENTIFIER PRIMARY KEY, NOME VARCHAR(100), Inativo bit)").Wait();

        }
        public async Task<Usuario> AddAsync(Usuario entity)
        {
            using (var db = _databaseConfig.GetConnection())
            {

                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("Id", entity.Id);
                parameters.Add("Nome", entity.Nome);
                parameters.Add("Inativo", entity.Inativo);

                await db.ExecuteAsync($"INSERT INTO [{TableName}](Id, Nome, Inativo) VALUES (@Id, @Nome, @Inativo)", parameters);


                return await FindByIdAsync(entity.Id);
            }
        }

        public async Task<Usuario> FindByIdAsync(object entityId)
        {
            using (var db = _databaseConfig.GetConnection())
            {

                return await db.QuerySingleAsync<Usuario>($"select * from {TableName} where Id = @id", new { id = entityId });
            }
        }

        public Task AfterSaveAsync(Usuario entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public Task BeforeSaveAsync(Usuario entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public void BeginTransation()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CanSaveAsync(Usuario entity, bool insert)
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Usuario> Find(Expression<Func<Usuario, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Usuario> FindAll(string include = "")
        {
            using (var db = _databaseConfig.GetConnection())
            {

                return db.Query<Usuario>($"select * from {TableName}").AsQueryable();
            }
        }



        public Task RemoveAsync(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void RollbackTransation()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> UpdateAsync(object id, Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void ExecuteSql(string sql)
        {
            using (var db = _databaseConfig.GetConnection())
            {
                db.Execute(sql);
            }
        }
    }
}
