using FluentAssertions;
using Microservices.Entities;
using Microservices.Repository;
using Microservices.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microservices.Data.Integration.Test
{
    public class EFCoreRepositoryTest : IDisposable
    {
        IRepository<Usuario> _repository;
        DataContext _dataContext;
        private readonly ITestOutputHelper _log;
        private const string database = "ef_core_test";
        public EFCoreRepositoryTest(ITestOutputHelper log)
        {
            _log = log;

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={database};Trusted_Connection=True;").Options;

            _dataContext = new DataContext(options);

            _repository = new Repository<Usuario>(_dataContext);
            _dataContext.Database.EnsureCreated();
            _log.WriteLine("Create Database");
        }

    

        [Fact]
        public void InsereTest()
        {
            _repository.FindAll().Count().Should().Be(0);
            AddItemAsync().Wait();
            _repository.FindAll().Count().Should().Be(1);
        }

        [Fact]
        public async Task UpdateTest()
        {
            _repository.FindAll().Count().Should().Be(0);            
            var usuario = await AddItemAsync();
            _repository.FindAll().Count().Should().Be(1);
            var expected = "TesteNovoUser";
            usuario.Nome = expected;
            await _repository.UpdateAsync(usuario.Id, usuario);
            var foundUser = await _repository.FindByIdAsync(usuario.Id);
            foundUser.Nome.Should().Be(expected);
            foundUser.Inativo.Should().BeFalse();
            _repository.FindAll().Count().Should().Be(1);

        }
        [Fact]
        public async Task RemoveTest()
        {
            _repository.FindAll().Count().Should().Be(0);
            var usuario = await AddItemAsync();
            _repository.FindAll().Count().Should().Be(1);
            await _repository.RemoveAsync(usuario);
            _repository.FindAll().Count().Should().Be(0);
        }

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
            return await _repository.AddAsync(usuario);
        }

        public void Dispose()
        {
            
            _dataContext.Database.ExecuteSqlCommand("delete Usuario;");

            _repository = null;
        }
    }
}
