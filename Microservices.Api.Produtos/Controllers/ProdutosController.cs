using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservices.Api.Common;
using Microservices.Business.Common;
using Microservices.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Api.Produtos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBusiness<Produto>
    {
        public ProdutosController(BusinessBase<Produto> business) : base(business)
        {
        }
    }
}
