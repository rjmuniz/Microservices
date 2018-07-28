using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microservices.Data.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Apelido = table.Column<string>(nullable: true),
                    Inativo = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    RazaoSocial = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    PrimeiraCompra = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Inativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EnderecoEmail = table.Column<string>(maxLength: 100, nullable: true),
                    PessoaId = table.Column<int>(nullable: false),
                    Principal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emails_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClienteId = table.Column<int>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Vendedor = table.Column<string>(nullable: true),
                    ValorTotal = table.Column<decimal>(type: "decimal(14,4)", nullable: false),
                    UsuarioCadastroId = table.Column<Guid>(nullable: false),
                    DataHoraCadastro = table.Column<DateTime>(nullable: false),
                    UsuarioAlteracaoId = table.Column<Guid>(nullable: true),
                    DataHoraAlteracao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Pessoas_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuario_UsuarioAlteracaoId",
                        column: x => x.UsuarioAlteracaoId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuario_UsuarioCadastroId",
                        column: x => x.UsuarioCadastroId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(14,4)", nullable: false),
                    Inativo = table.Column<bool>(nullable: false),
                    UsuarioCadastroId = table.Column<Guid>(nullable: false),
                    DataHoraCadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Usuario_UsuarioCadastroId",
                        column: x => x.UsuarioCadastroId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItens",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProdutoId = table.Column<int>(nullable: false),
                    PedidoId = table.Column<Guid>(nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(14,4)", nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Desconto = table.Column<decimal>(type: "decimal(4,3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoItens_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoItens_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Apelido", "Discriminator", "Inativo", "CNPJ", "RazaoSocial", "PrimeiraCompra" },
                values: new object[] { 1, "Microservices", "Cliente", false, "191000000000", "Microservices SA", new DateTime(2018, 6, 28, 20, 44, 45, 577, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Inativo", "Nome" },
                values: new object[] { new Guid("09c04ed3-607f-4c37-9714-30053ff97d77"), false, "Admin" });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "EnderecoEmail", "PessoaId", "Principal" },
                values: new object[] { 1, "teste1@teste.com", 1, true });

            migrationBuilder.InsertData(
                table: "Emails",
                columns: new[] { "Id", "EnderecoEmail", "PessoaId", "Principal" },
                values: new object[] { 2, "teste2@teste.com", 1, false });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "DataHoraCadastro", "Inativo", "Nome", "Preco", "UsuarioCadastroId" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Mesa", 200m, new Guid("09c04ed3-607f-4c37-9714-30053ff97d77") });

            migrationBuilder.CreateIndex(
                name: "IX_Emails_PessoaId",
                table: "Emails",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_PedidoId",
                table: "PedidoItens",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItens_ProdutoId",
                table: "PedidoItens",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                table: "Pedidos",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioAlteracaoId",
                table: "Pedidos",
                column: "UsuarioAlteracaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_UsuarioCadastroId",
                table: "Pedidos",
                column: "UsuarioCadastroId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_UsuarioCadastroId",
                table: "Produtos",
                column: "UsuarioCadastroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "PedidoItens");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
