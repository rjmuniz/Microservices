using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservices.Entities
{
    public class Email : IEntityId, IEntityIndicaPrincipal
    {
        public int Id { get; set; }
        [EmailAddress]
        [MaxLength(100)]
        public string EnderecoEmail { get; set; }

        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public bool Principal { get; set; }
    }
}
