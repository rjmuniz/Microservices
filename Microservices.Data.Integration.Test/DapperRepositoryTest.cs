using FluentAssertions;
using Microservices.Entities;
using Microservices.Repository.Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Xunit.Abstractions;
using Microservices.Repository.Interfaces;
using System.Data.SqlClient;

namespace Microservices.Data.Integration.Test
{
    public class DapperRepositoryTest : IDisposable
    {
        RepositoryUsuario _repository;
        private const string database = "dapper_test";
        private readonly ITestOutputHelper _log;

        public DapperRepositoryTest(ITestOutputHelper log)
        {
            _log = log;
            // var configMock = new Mock<IConfiguration>();
            //configMock.Setup(c=> c.GetConnectionString(c.))
            // var repOptions = new Microservices.Repository.Interfaces.RepositoryOptions(configMock.Object);

            CreateDb();


            var mockRepOptions = new Mock<IRepositoryOptions>();
            mockRepOptions.Setup(r => r.ConnectionString).Returns($"Server=(localdb)\\mssqllocaldb;Database={database};Trusted_Connection=True;");

            _repository = new RepositoryUsuario(new DatabaseConfig(mockRepOptions.Object));

            
        }

        private void CreateDb()
        {
            using (SqlConnection sql = new SqlConnection($"Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;"))
            {
                sql.Open();
                var cmd = sql.CreateCommand();
                cmd.CommandText = $"If(db_id(N'{database}') IS NULL) \r\n BEGIN \r\n CREATE DATABASE {database}; \r\n END";
                cmd.ExecuteNonQuery();
                _log.WriteLine("Created Database");
            }
        }

        [Fact]
        public void InsereTest()
        {
            _repository.FindAll().Count().Should().Be(0);
            AddItemAsync().Wait();
            _repository.FindAll().Count().Should().Be(1);
        }

        //[Fact]
        //public async Task UpdateTest()
        //{
        //    _repository.FindAll().Count().Should().Be(0);
        //    var usuario = await AddItemAsync();
        //    _repository.FindAll().Count().Should().Be(1);
        //    var expected = "TesteNovoUser";
        //    usuario.Nome = expected;
        //    usuario.Inativo = true;
        //    await _repository.UpdateAsync(usuario.Id, usuario);
        //    var foundUser = await _repository.FindByIdAsync(usuario.Id);
        //    foundUser.Nome.Should().Be(expected);
        //    foundUser.Inativo.Should().BeTrue();
        //    _repository.FindAll().Count().Should().Be(1);

        //}
        //[Fact]
        //public async Task RemoveTest()
        //{
        //    _repository.FindAll().Count().Should().Be(0);
        //    var usuario = await AddItemAsync();
        //    _repository.FindAll().Count().Should().Be(1);
        //    await _repository.RemoveAsync(usuario);
        //    _repository.FindAll().Count().Should().Be(0);
        //}

        [Fact]
        public void InsercaoDuasVezesTest()
        {

            _repository.FindAll().Count().Should().Be(0);
            AddItemAsync().Wait();
            AddItemAsync().Wait();
            _repository.FindAll().Count().Should().Be(2);
        }

        public async Task<Usuario> AddItemAsync()
        {
            _log.WriteLine("Add Item");
            Usuario usuario = new Usuario()
            {
                Id = Guid.NewGuid(),
                Nome = "test"
            };
            await _repository.AddAsync(usuario);
            await _repository.Commit();
            return usuario;
        }

        public void Dispose()
        {
            _log.WriteLine("Delete Database");

            _repository.ExecuteSql($"use master; drop database {database}");
            _repository = null;
        }
    }
}
