using Dapper;
using Microservices.Entities;
using Microservices.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Repository.Dapper
{
    public class RepositoryUsuario : IRepository<Usuario>
    {
        private const string TableName = "Usuario";
        private IDbConnection internalConnection;
        private IDbConnection Connection
        {
            get
            {
                if (internalConnection == null)
                    internalConnection = _databaseConfig.GetConnection();
                return internalConnection;
            }
        }
        private IDbTransaction Transaction;

        private readonly DatabaseConfig _databaseConfig;

        public RepositoryUsuario(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
            _databaseConfig = databaseConfig;
            _databaseConfig.CreateTable(TableName,
                "CREATE TABLE Usuario (Id UNIQUEIDENTIFIER PRIMARY KEY, NOME VARCHAR(100), Inativo bit)").Wait();

        }


        public async Task AddAsync(Usuario entity)
        {
            CreateTransaction();


            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();
            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id);
            parameters.Add("Nome", entity.Nome);
            parameters.Add("Inativo", entity.Inativo);

            await Connection.ExecuteAsync($"INSERT INTO [{TableName}](Id, Nome, Inativo) VALUES (@Id, @Nome, @Inativo)", parameters);


        }

        private void CreateTransaction()
        {
            if (Transaction == null)
                Transaction = Connection.BeginTransaction();
        }


        public async Task<Usuario> FindByIdAsync(object entityId, string include = "")
        {
            using (var db = _databaseConfig.GetConnection())
            {
                return await db.QuerySingleAsync<Usuario>($"select * from {TableName} where Id = @id", new { id = entityId });
            }
        }


        public Task<bool> CanSaveAsync(Usuario entity, bool insert)
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


        public Task UpdateAsync(object id, Usuario entity)
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

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task BeforeSaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task AfterSaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task Commit()
        {
            Transaction.Commit();
            Transaction = null;
            await Task.CompletedTask;
        }


    }
}
