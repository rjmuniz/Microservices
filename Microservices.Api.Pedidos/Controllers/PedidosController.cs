using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Api.Common;
using Microservices.Business.Common;
using Microservices.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Api.Pedidos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBusiness<Pedido>
    {
        public PedidosController(IBusinessBase<Pedido> business) : base(business)
        {
        }
    }
}
