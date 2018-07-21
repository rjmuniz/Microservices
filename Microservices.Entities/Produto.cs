using Microservices.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservices.Entities
{
    public class Produto : IEntityId, IEntityInativo, ILogCadastro
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Nome { get; set; }

        [Required, Range(0.001, double.MaxValue)]
        public decimal Preco { get; set; }

        public bool Inativo { get; set; } 



        public Guid UsuarioCadastroId { get; set; }
        [ForeignKey("UsuarioCadastroId")]
        public Usuario UsuarioCadastro { get; set; }
        public DateTime DataHoraCadastro { get; set; }

    }
}
