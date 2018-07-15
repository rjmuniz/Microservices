using JetBrains.Annotations;
using Microservices.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microservices.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Mesa", Preco = 200 },
                new Produto { Id = 2, Nome = "Mesa Modelo velho", Inativo = true, Preco = 70.5M },
                new Produto { Id = 4, Nome = "note", Inativo = false, Preco = 2500 });
        }
    }
}
