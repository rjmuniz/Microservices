using JetBrains.Annotations;
using Microservices.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microservices.Data
{
    public class DataContext : DbContext
    {
        public Guid AdminUserId { get; } = new Guid("09C04ED3-607F-4C37-9714-30053FF97D77");


        public DataContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = AdminUserId,
                Nome = "Admin",
                Inativo = false
            });
            modelBuilder.Entity<Cliente>().HasData(new Cliente
            {
                Id = 1,
                Nome = "Microservices",
                Inativo = false
            });
            modelBuilder.Entity<Pedido>()
              .HasOne(p => p.Cliente).WithMany().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Pedido>()
              .HasOne(p => p.UsuarioCadastro).WithMany().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PedidoItem>()
                .HasOne(p => p.Produto)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PedidoItem>()
                .HasOne(p => p.Pedido)
                .WithMany(p => p.PedidoItens)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Mesa", Preco = 200, UsuarioCadastroId = AdminUserId });
            //    new Produto { Id = 2, Nome = "Mesa Modelo velho", Inativo = true, Preco = 70.5M },
            //    new Produto { Id = 4, Nome = "note", Inativo = false, Preco = 2500 });
        }
    }
}
