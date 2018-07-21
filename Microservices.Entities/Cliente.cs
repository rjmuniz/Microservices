using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Microservices.Entities
{
    public class Cliente : IEntityId, IEntityInativo
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Inativo { get; set; }
    }
}
