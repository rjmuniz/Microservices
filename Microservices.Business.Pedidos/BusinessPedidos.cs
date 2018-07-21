using Microservices.Business.Common;
using Microservices.Connectors.Produtos.ExternalInterface;
using Microservices.Entities;
using Microservices.Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Business.Pedidos
{
    public class BusinessPedidos : BusinessBase<Pedido>
    {
        private readonly IHttpProduto _httpProduto;

        public BusinessPedidos(IRepository<Pedido> repository, IHttpProduto httpProduto)
            : base(repository)
        {
            _httpProduto = httpProduto;
        }

        public override Task<IQueryable<Pedido>> FindAllAsync()
        {
            return Task.FromResult(_repository.FindAll("PedidoItens"));
        }

        public override async Task BeforeAddAsync(Pedido entity, bool insert)
        {
            entity.Data = DateTime.Now;
            entity.Vendedor = System.Security.Principal.GenericPrincipal.Current?.Identity?.Name;

            //get Product Value
            foreach (var i in entity.PedidoItens)
            {
                var produto = await _httpProduto.GetProdutoAsync(i.ProdutoId);
                i.PrecoUnitario = produto.Preco;
            }

            entity.ValorTotal = entity.PedidoItens.Sum(d => d.TotalParcial);

            //

        }
    }
}
