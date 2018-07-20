using System;
using System.Collections.Generic;

namespace Microservices.Connectors.Common
{
    public class Endpoints
    {
        private readonly IDictionary<string, string> _endpoints;

        public Endpoints(IDictionary<string, string> endpoints)
        {
            _endpoints = endpoints ?? throw new ArgumentNullException(nameof(endpoints));
        }

        public string GetEndpoint(string name) => _endpoints.ContainsKey(name) ? _endpoints[name] : throw new ArgumentOutOfRangeException(name);
    }
}
