using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Entities
{
    public class Pessoa: IEntityId, IEntityInativo
    {
        public int Id { get; set; }
        public string Apelido { get; set; }
        public bool Inativo { get; set; }

        public ICollection<Email> Emails { get; set; }
    }
}
