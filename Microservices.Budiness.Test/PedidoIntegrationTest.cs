using Microservices.Business.Pedidos;
using Microservices.Connectors.Produtos.ExternalInterface;
using Microservices.Data;
using Microservices.Entities;
using Microservices.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Microservices.Business.Test
{
    public class PedidoIntegrationTest : IDisposable
    {
        private readonly BusinessPedidos _business;
        private readonly DataContext _dataContext;
        public PedidoIntegrationTest()
        {
            Mock<IHttpProduto> httpProduto = new Mock<IHttpProduto>();
            httpProduto.Setup(m => m.GetProdutoAsync(It.IsAny<int>())).Returns<int>(async (id) => await Task.FromResult(new Produto { Id = id, Nome = $"test{id}", Preco = Convert.ToDecimal(id * 2.5), Inativo = false }));


            DbContextOptions dbContextOptions = new DbContextOptionsBuilder<DataContext>().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ef_core_test;Integrated Security=True;").Options;
            _dataContext = new DataContext(dbContextOptions);
            _business = new BusinessPedidos(new Repository<Pedido>(_dataContext), httpProduto.Object);

        }
        public void Dispose()
        {
            _dataContext.Database.ExecuteSqlCommand("Delete PedidoItens; Delete Pedidos");
        }



        [Fact]
        public async Task AddPedidoTest()
        {
            var total = await _business.CountAsync();

            var p = await AddPedido();
            Assert.Equal(total + 1, await _business.CountAsync());
            var p2 = await _business.FindByIdAsync(p.Id);
            Assert.Equal(3, p2.PedidoItens.Count);
        }
        [Fact]
        public async Task FindPedidoTest()
        {
            var total = await _business.CountAsync();

            var p = await AddPedido();
            Assert.Equal(total + 1, await _business.CountAsync());
            var p2 = await _business.FindByIdAsync(p.Id);
            Assert.Equal(3, p2.PedidoItens.Count);
        }


        [Fact]
        public async Task LimparTest()
        {
            _dataContext.Database.ExecuteSqlCommand("Delete PedidoItens; Delete Pedidos");
            Assert.Equal(0, await _business.CountAsync());
        }
        private async Task<Pedido> AddPedido()
        {
            var p = new Pedido()
            {
                ClienteId = 1,
                PedidoItens = new List<PedidoItem>()
                {
                     new PedidoItem//12,5
                     {
                          Desconto=10,
                          ProdutoId = 5,
                          Quantidade = 1
                     },
                     new PedidoItem //35
                     {
                           ProdutoId = 7,
                           Quantidade = 2
                     },
                     new PedidoItem//90
                     {
                           ProdutoId = 8,
                            Quantidade = 5
                     }
                },
                UsuarioCadastroId = new Guid("6FD8D788-7955-4A24-AD72-C2F189923699")
            };
            return await _business.AddAsync(p);
        }
    }
}
