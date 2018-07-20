using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Connectors
{
    public static  class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadObjectAsync<T>(this HttpContent content)
        {
            return await content.ReadAsAsync<T>();
        }
    }
}
