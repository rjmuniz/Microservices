using Microservices.Entities;
using System;
using System.Threading.Tasks;

namespace Microservices.Connectors.Produtos.ExternalInterface
{
    public interface IHttpProduto
    {
        Task<Produto> GetProdutoAsync(int id);
    }
}
