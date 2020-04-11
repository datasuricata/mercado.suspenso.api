using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mercadosuspenso.orm.Migrations
{
    public partial class _202000001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doacao",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Cpf = table.Column<string>(nullable: true),
                    Detalhes = table.Column<string>(nullable: true),
                    VarejistaId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Cep = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Cidade = table.Column<string>(nullable: true),
                    Estado = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Codigo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Participante",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Rg = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    EnderecoId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participante_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoacaoProduto",
                columns: table => new
                {
                    DoacaoId = table.Column<string>(nullable: false),
                    ProdutoId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoacaoProduto", x => new { x.DoacaoId, x.ProdutoId });
                    table.ForeignKey(
                        name: "FK_DoacaoProduto_Doacao_DoacaoId",
                        column: x => x.DoacaoId,
                        principalTable: "Doacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoacaoProduto_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Distribuidor",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    RazaoSocial = table.Column<string>(nullable: true),
                    Representante = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    EnderecoId = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distribuidor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distribuidor_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Distribuidor_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Varejista",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    RazaoSocial = table.Column<string>(nullable: true),
                    Representante = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    Parceiro = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    EnderecoId = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Varejista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Varejista_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Varejista_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vistoria",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Hash = table.Column<string>(nullable: true),
                    Processamento = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    DoacaoId = table.Column<string>(nullable: true),
                    DistribudidorId = table.Column<string>(nullable: true),
                    DistribuidorId = table.Column<string>(nullable: true),
                    ParticipanteId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vistoria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vistoria_Distribuidor_DistribuidorId",
                        column: x => x.DistribuidorId,
                        principalTable: "Distribuidor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vistoria_Doacao_DoacaoId",
                        column: x => x.DoacaoId,
                        principalTable: "Doacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vistoria_Participante_ParticipanteId",
                        column: x => x.ParticipanteId,
                        principalTable: "Participante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Distribuidor_EnderecoId",
                table: "Distribuidor",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Distribuidor_UsuarioId",
                table: "Distribuidor",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DoacaoProduto_ProdutoId",
                table: "DoacaoProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Participante_EnderecoId",
                table: "Participante",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Varejista_EnderecoId",
                table: "Varejista",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Varejista_UsuarioId",
                table: "Varejista",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Vistoria_DistribuidorId",
                table: "Vistoria",
                column: "DistribuidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vistoria_DoacaoId",
                table: "Vistoria",
                column: "DoacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Vistoria_ParticipanteId",
                table: "Vistoria",
                column: "ParticipanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoacaoProduto");

            migrationBuilder.DropTable(
                name: "Varejista");

            migrationBuilder.DropTable(
                name: "Vistoria");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Distribuidor");

            migrationBuilder.DropTable(
                name: "Doacao");

            migrationBuilder.DropTable(
                name: "Participante");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Endereco");
        }
    }
}
