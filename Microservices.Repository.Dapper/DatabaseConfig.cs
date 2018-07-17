using Microservices.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Repository.Dapper
{
    public class DatabaseConfig
    {
        private SqlConnection _connection;

        private readonly IList<string> Tables = new List<string>();
        private readonly IRepositoryOptions _repositoryOptions;

        public DatabaseConfig(IRepositoryOptions repositoryOptions)
        {
            _repositoryOptions = repositoryOptions;
            _connection = new SqlConnection(_repositoryOptions.ConnectionString);
            _connection.Open();
            _connection.Close();
        }
        public async Task CreateTable<T>(DapperConfiguration<T> dapperConfiguration)
        {
            await CreateTable(dapperConfiguration.TableName, dapperConfiguration.Create);
        }

        public async Task CreateTable(string tableName, string sqlCreateTable)
        {
            if (Tables.IndexOf(tableName) < 0)
            {
                await _connection.OpenAsync();

                if (!await ExistsTableAsync(tableName))
                    await CreateDDLTableAsync(sqlCreateTable);

                Tables.Add(tableName);
                _connection.Close();
            }
        }
        private async Task<bool> ExistsTableAsync(string table)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "select 1 from sys.objects where name = @name";
            cmd.Parameters.AddWithValue("name", table);
            var _ret = await cmd.ExecuteScalarAsync();
            return (_ret??0).Equals(1);
        }

        private async Task CreateDDLTableAsync(string sql)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = sql;
            await cmd.ExecuteNonQueryAsync();

        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(_repositoryOptions.ConnectionString);
        }
    }
}
