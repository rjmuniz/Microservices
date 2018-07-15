using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microservices.Entities
{
    public class Pedido : IEntityIdGuid, ILogAlteracao
    {
        public Guid Id { get; set; }

        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }

        public DateTime Data { get; set; }

        public string Vendedor { get; set; }

        public decimal ValorTotal { get; set; }

        public ICollection<PedidoItem> PedidoItems { get; set; }
        public Guid? UsuarioAlteracaoId { get; set; }
        public IUsuario UsuarioAlteracao { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }
        public Guid UsuarioCadastroId { get; set; }
        public IUsuario UsuarioCadastro { get; set; }
        public DateTime DataHoraCadastro { get; set; }
    }
}
