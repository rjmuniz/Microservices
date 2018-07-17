using FluentAssertions;
using Microservices.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Microservices.Data.Integration.Test
{
    public class DbContextTest : IDisposable
    {
        private readonly DataContext _dataContext;
        private readonly ITestOutputHelper log;

        public DbContextTest(ITestOutputHelper log)
        {
            this.log = log;
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("DatabaseTest").Options;

            _dataContext = new DataContext(options);

            _dataContext.Database.EnsureCreated();



        }
        [Fact]
        public void InsercaoTest()
        {

            _dataContext.Produtos.Count().Should().Be(0);
            var expected = AddItem();
            var actual = _dataContext.Produtos.First();
            actual.Should().Be(actual);
            Assert.Equal(expected, actual);

            _dataContext.Produtos.Count().Should().Be(1);
        }
        [Fact]
        public void InsercaoDuasVezesTest()
        {

            _dataContext.Produtos.Count().Should().Be(0);
            AddItem();
            AddItem();
            _dataContext.Produtos.Count().Should().Be(2);
        }

        [Fact]
        public void UpdateTest()
        {
            var produto = AddItem();
            var expectedText = "Produto XYZ";
            produto.Nome = expectedText;
            _dataContext.SaveChanges();
            var actual = _dataContext.Produtos.First();

            actual.Nome.Should().Be(expectedText);
        }

        [Fact]
        public void DeleteTest()
        {
            var produto = AddItem();
            _dataContext.Produtos.Count().Should().Be(1);
            _dataContext.Produtos.Remove(produto);
            _dataContext.SaveChangesAsync().Wait();
            _dataContext.Produtos.Count().Should().Be(0);

        }

        public Produto AddItem()
        {
            Produto produto = new Produto()
            {
                Nome = "Produto1",
                Preco = 10.45M,
                Inativo = false,
                UsuarioCadastroId = Guid.NewGuid(),
                DataHoraCadastro = DateTime.Today
            };


            _dataContext.Add(produto);

            _dataContext.SaveChangesAsync().Wait();
            return produto;
        }

        public void Dispose()
        {
            log.WriteLine("Drop Database");

            _dataContext.Database.EnsureDeleted();
            _dataContext.Dispose();
        }


    }
}
