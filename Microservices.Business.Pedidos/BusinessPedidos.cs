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

        public override async Task BeforeAddAsync(Pedido entity, bool insert)
        {
            entity.Data = DateTime.Now;
            entity.Vendedor = System.Security.Principal.GenericPrincipal.Current?.Identity?.Name;

            //get Product Value
            foreach (var i in entity.PedidoItems)
            {
                i.Produto = await _httpProduto.GetProdutoAsync(i.ProdutoId);
                i.PrecoUnitario = i.Produto.Preco;
            }

            entity.ValorTotal = entity.PedidoItems.Sum(d => d.TotalParcial);

            //

        }
    }
}
