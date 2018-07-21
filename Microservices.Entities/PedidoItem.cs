using Microservices.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Microservices.Entities
{
    public class PedidoItem : IEntityId
    {
        public int Id { get; set; }

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }


        public Guid PedidoId { get; set; }
        [ForeignKey("PedidoId")]
        public Pedido Pedido { get; set; }

        public decimal PrecoUnitario { get; set; }

        public int Quantidade { get; set; }
        [Range(0, 100)]
        public decimal? Desconto { get; set; }

        [NotMapped]
        public decimal TotalParcial => PrecoUnitario * Quantidade * (Desconto.HasValue ? (Desconto.Value / 100) : 1);

    }
}
