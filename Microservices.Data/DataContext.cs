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


        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Email> Emails { get; set; }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>().ToTable("Pessoas");
            modelBuilder.Entity<PessoaJuridica>().ToTable("PessoasJuridicas");
            modelBuilder.Entity<Cliente>().ToTable("Clientes");

            modelBuilder.Entity<Email>().HasOne(p => p.Pessoa).WithMany(e=> e.Emails).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pedido>()
              .HasOne(p => p.Cliente).WithMany().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Pedido>()
              .HasOne(p => p.UsuarioCadastro).WithMany().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PedidoItem>()
                .HasOne(p => p.Produto)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict)
                ;
            modelBuilder.Entity<PedidoItem>()
                .HasOne(p => p.Pedido)
                .WithMany(p => p.PedidoItens)
                .OnDelete(DeleteBehavior.Cascade);
            

            modelBuilder.Entity<Produto>().Property(c => c.Preco).HasColumnType("decimal(14,4)");
            modelBuilder.Entity<PedidoItem>().Property(c => c.PrecoUnitario).HasColumnType("decimal(14,4)");
            modelBuilder.Entity<PedidoItem>().Property(c => c.Desconto).HasColumnType("decimal(4,3)");
            modelBuilder.Entity<Pedido>().Property(c => c.ValorTotal).HasColumnType("decimal(14,4)");


            Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = AdminUserId,
                Nome = "Admin",
                Inativo = false
            });
            modelBuilder.Entity<Cliente>().HasData(new Cliente
            {
                Id = 1,
                Apelido = "Microservices",
                CNPJ = "191000000000",
                PrimeiraCompra = DateTime.Now.Subtract(TimeSpan.FromDays(30)),
                RazaoSocial = "Microservices SA",
                Inativo = false
            });
            modelBuilder.Entity<Email>().HasData(
                new Email { Id = 1, EnderecoEmail = "teste1@teste.com", Principal = true, PessoaId = 1 },
                new Email { Id = 2, EnderecoEmail = "teste2@teste.com", Principal = false, PessoaId = 1 }
            );
            modelBuilder.Entity<Produto>().HasData(
                new Produto { Id = 1, Nome = "Mesa", Preco = 200, UsuarioCadastroId = AdminUserId });
            //    new Produto { Id = 2, Nome = "Mesa Modelo velho", Inativo = true, Preco = 70.5M },
            //    new Produto { Id = 4, Nome = "note", Inativo = false, Preco = 2500 });
        }
    }
}
