using Microservices.Entities.Common;
using System;

namespace Microservices.Entities
{
    public class Usuario : IEntityIdGuid, IEntityInativo
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }
        public bool Inativo { get; set; }
    }
}
