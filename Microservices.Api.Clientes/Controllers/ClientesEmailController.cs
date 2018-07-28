using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Api.Common;
using Microservices.Business.Clientes;
using Microservices.Business.Common;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Api.Clientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesEmailController : ControllerBusiness<ClienteEmail>
    {
        public ClientesEmailController(IBusinessBase<ClienteEmail> business) : base(business)
        {
        }
    }
}
