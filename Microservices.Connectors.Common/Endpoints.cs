using System;
using System.Collections.Generic;

namespace Microservices.Connectors.Common
{
    public class Endpoints
    {
        public enum EndpointKey
        {
            Produtos
        }


        private readonly IDictionary<EndpointKey, string> _endpoints;

        public Endpoints(IDictionary<EndpointKey, string> endpoints)
        {
            _endpoints = endpoints ?? throw new ArgumentNullException(nameof(endpoints));
        }

        public string GetEndpoint(EndpointKey key) => _endpoints.ContainsKey(key) ? _endpoints[key] : throw new ArgumentOutOfRangeException(key.ToString());
    }
}
