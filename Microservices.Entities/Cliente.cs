using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservices.Entities
{
    public class Cliente : PessoaJuridica
    {
        public DateTime PrimeiraCompra { get; set; }
    }
}
