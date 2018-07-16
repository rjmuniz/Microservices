using FluentAssertions;
using Microservices.Entities;
using Microservices.Repository;
using Microservices.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microservices.Data.Integration.Test
{
    public class RepositoryTest:IDisposable
    {
        IRepository<Produto> _repository;
        DataContext _dataContext;
        public RepositoryTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("DatabaseTest").Options;

            _dataContext = new DataContext(options);

            _repository = new Repository<Produto>(_dataContext);
        }

        [Fact]
        public void InsereTest()
        {
            _repository.GetAll().Count().Should().Be(0);
            AddItemAsync().Wait();
            _repository.GetAll().Count().Should().Be(1);
        }

        [Fact]
        public void InsercaoDuasVezesTest()
        {

            _repository.GetAll().Count().Should().Be(0);
            AddItemAsync().Wait();
            AddItemAsync().Wait();
            _repository.GetAll().Count().Should().Be(2);
        }

        public async Task<Produto> AddItemAsync()
        {
            Produto produto = new Produto()
            {
                Nome = "Produto1",
                Preco = 10.45M,
                Inativo = false,
                UsuarioCadastroId = Guid.NewGuid(),
                DataHoraCadastro = DateTime.Today
            };
            return await _repository.InsertEntityAsync(produto);
        }

        public void Dispose()
        {
            _dataContext.Database.EnsureDeleted();
            
            _repository = null;
        }
    }
}
