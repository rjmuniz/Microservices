using Microservices.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Threading.Tasks;
using Xunit;

namespace Microservices.Data.Integration.Test
{
    public class ProdutoTest
    {
        private readonly DataContext _dataContext;
        public ProdutoTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase("DatabaseTest").Options;
            _dataContext = new DataContext(options);
        }
        [Fact]
        public async Task InsercaoTest()
        {
            Assert.Equal(0, await _dataContext.Produtos.CountAsync(),);
            Produto produto = new Produto()
            {
                Id = 123,
                Nome = "Produto1",
                Preco = 10.45M,
                Inativo = false,
                UsuarioCadastroId = Guid.NewGuid(),
                DataHoraCadastro = DateTime.Today
            };


            _dataContext.Add(produto);
            await _dataContext.SaveChangesAsync();
            Assert.Equal(1, await _dataContext.Produtos.CountAsync());

        }


        ~ProdutoTest()
        {
            Console.WriteLine("Dropando database");
            _dataContext.Dispose();
        }
    }
}
