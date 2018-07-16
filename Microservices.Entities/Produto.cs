using Microservices.Entities.Common;
using System;

namespace Microservices.Entities
{
    public class Produto : IEntityId, IEntityInativo, ILogCadastro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        
        public decimal Preco { get; set; }

        public bool Inativo { get; set; }



        public Guid UsuarioCadastroId { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public DateTime DataHoraCadastro { get; set; }

    }
}
