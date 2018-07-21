using Microservices.Connectors.Common;
using Microservices.Connectors.Produtos.ExternalInterface;
using Microservices.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microservices.Connectors.Produtos
{
    public class HttpProduct: IHttpProduto
    {
        private readonly HttpClient _client;
        public HttpProduct(HttpClient client, Endpoints endpoints)
        {
            _client = client;
            client.BaseAddress = new Uri(endpoints.GetEndpoint(Endpoints.EndpointKey.Produtos));

        }
        public async Task<Produto> GetProdutoAsync(int id)
        {
            var response = await _client.GetAsync($"api/produtos/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadObjectAsync<Produto>();
        }
    }
}
