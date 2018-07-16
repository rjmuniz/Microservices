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
    public class ProdutoTest:IDisposable
    {
        private readonly DataContext _dataContext;
        private readonly ITestOutputHelper log;

        public ProdutoTest(ITestOutputHelper log)
        {
            this.log = log;
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("DatabaseTest").Options;

            _dataContext = new DataContext(options);

            _dataContext.Database.EnsureCreated();
            
            
            
        }
        [Fact]
        public async Task InsercaoTest()
        {

            _dataContext.Produtos.Count().Should().Be(0);
            var expected = await AddItemAsync();
            var actual = await _dataContext.Produtos.FirstAsync();
            actual.Should().Be(actual);
            Assert.Equal(expected, actual);

            _dataContext.Produtos.Count().Should().Be(1);
        }
        [Fact]
        public async Task InsercaoDuasVezesTest()
        {

            _dataContext.Produtos.Count().Should().Be(0);
            await AddItemAsync();
            await AddItemAsync();
            _dataContext.Produtos.Count().Should().Be(2);
        }

        [Fact]
        public async Task UpdateTest()
        {
            var produto = await AddItemAsync();
            var expectedText = "Produto XYZ";
            produto.Nome = expectedText;
            _dataContext.SaveChanges();
            var actual = await _dataContext.Produtos.FirstAsync();

            actual.Nome.Should().Be(expectedText);           
        }

        [Fact]
        public async Task DeleteTest()
        {
            var produto = await AddItemAsync();
            _dataContext.Produtos.Count().Should().Be(1);
            _dataContext.Produtos.Remove(produto);
            _dataContext.SaveChanges();
            _dataContext.Produtos.Count().Should().Be(0);
            
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


            _dataContext.Add(produto);

            await _dataContext.SaveChangesAsync();
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
