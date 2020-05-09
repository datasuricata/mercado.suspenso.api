using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mercadosuspenso.orm.Migrations
{
    public partial class _202000003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vinculo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 32, nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(nullable: false),
                    AtualizadoEm = table.Column<DateTimeOffset>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    DistribuidorId = table.Column<string>(nullable: true),
                    VarejistaId = table.Column<string>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vinculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vinculo_Distribuidor_DistribuidorId",
                        column: x => x.DistribuidorId,
                        principalTable: "Distribuidor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vinculo_Varejista_VarejistaId",
                        column: x => x.VarejistaId,
                        principalTable: "Varejista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Endereco",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "Bairro", "Cep", "Cidade", "Complemento", "CriadoEm", "Estado", "Logradouro", "Numero" },
                values: new object[,]
                {
                    { "eff3b1b9b3b742a6b5ba4fffdf99585e", true, null, "Centro", "04747110", "São Paulo", "SALA 02", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 617, DateTimeKind.Unspecified).AddTicks(1268), new TimeSpan(0, 0, 0, 0, 0)), "SP", "Rua Santo Aristides", "23" },
                    { "5d2387e4b93f476c81dd1bb757b27f43", true, null, "Indianópolis", "04028003", "São Paulo", "Conj. 01", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 618, DateTimeKind.Unspecified).AddTicks(2476), new TimeSpan(0, 0, 0, 0, 0)), "SP", "Avenida Ibirapuera", "334" },
                    { "010bafef4b7342bda843d22b1ec9d7c2", true, null, "Sitio dos Vianas", "09171686", "São Paulo", "02", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 618, DateTimeKind.Unspecified).AddTicks(2672), new TimeSpan(0, 0, 0, 0, 0)), "SP", "Rua Santa Adélia", "35" },
                    { "39f3a4d569ea400f848dd71deed5eb82", true, null, "Centro", "80020040", "Curitiba", "32", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 618, DateTimeKind.Unspecified).AddTicks(2685), new TimeSpan(0, 0, 0, 0, 0)), "PR", "Muricy 73", "32" }
                });

            migrationBuilder.InsertData(
                table: "Participante",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "Cpf", "CriadoEm", "EnderecoId", "Nome", "Rg", "Status", "Telefone" },
                values: new object[,]
                {
                    { "816e1eb79d7d4863a953f77e9d8a2347", true, null, "09316558980", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 621, DateTimeKind.Unspecified).AddTicks(8395), new TimeSpan(0, 0, 0, 0, 0)), null, "Carlos Gabriel", "3243518", 0, "41595653717" },
                    { "02ee3c7582ea47a0a8105e9a8e9764bc", true, null, "09316573980", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 621, DateTimeKind.Unspecified).AddTicks(8508), new TimeSpan(0, 0, 0, 0, 0)), null, "Amanda Mendez", "1243518", 0, "41696663717" }
                });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "c9f9cd17020c4bfaa83f7c53e5f9b278",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 602, DateTimeKind.Unspecified).AddTicks(3777), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "d87e4de503984cfd8b282616ba50b42f",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 602, DateTimeKind.Unspecified).AddTicks(601), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "e0176f9f9b0445de83411b404e68a311",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 602, DateTimeKind.Unspecified).AddTicks(3700), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "Email", "Senha", "Tipo" },
                values: new object[,]
                {
                    { "7d5ea14a85ba4af7a4ca4c95545f3545", true, null, new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 602, DateTimeKind.Unspecified).AddTicks(4936), new TimeSpan(0, 0, 0, 0, 0)), "jeniffer.restaurante@restaurante.com", "E10ADC3949BA59ABBE56E057F20F883E", 2 },
                    { "2580868e52444603b4d75fdf92f7f916", true, null, new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 614, DateTimeKind.Unspecified).AddTicks(6502), new TimeSpan(0, 0, 0, 0, 0)), "carlos@pizzaria.com", "E10ADC3949BA59ABBE56E057F20F883E", 2 },
                    { "4bea229e5b5a4d66a75d1d248f7dc2d5", true, null, new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 614, DateTimeKind.Unspecified).AddTicks(7053), new TimeSpan(0, 0, 0, 0, 0)), "olivia@noturna.com.br", "E10ADC3949BA59ABBE56E057F20F883E", 1 }
                });

            migrationBuilder.InsertData(
                table: "Distribuidor",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "Cnpj", "CriadoEm", "EnderecoId", "RazaoSocial", "Representante", "Status", "Telefone", "UsuarioId" },
                values: new object[,]
                {
                    { "cf936849e0b244a2a6a812c15d69390d", true, null, "46992525000151", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 618, DateTimeKind.Unspecified).AddTicks(7986), new TimeSpan(0, 0, 0, 0, 0)), "eff3b1b9b3b742a6b5ba4fffdf99585e", "Luiza e Jennifer Restaurante Ltda", "Jennifer Rocha", 0, "1129551395", "7d5ea14a85ba4af7a4ca4c95545f3545" },
                    { "189be3c445504dcf9d8559959579035e", true, null, "99270944000146", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 619, DateTimeKind.Unspecified).AddTicks(8546), new TimeSpan(0, 0, 0, 0, 0)), "5d2387e4b93f476c81dd1bb757b27f43", "LAndré e Mateus Pizzaria Delivery Ltda", "Carlos Matues", 0, "1128810961", "2580868e52444603b4d75fdf92f7f916" }
                });

            migrationBuilder.InsertData(
                table: "Participante",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "Cpf", "CriadoEm", "EnderecoId", "Nome", "Rg", "Status", "Telefone" },
                values: new object[] { "e942c6f6c1364447a584a8f2fee583c2", true, null, "09316578980", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 621, DateTimeKind.Unspecified).AddTicks(1321), new TimeSpan(0, 0, 0, 0, 0)), "39f3a4d569ea400f848dd71deed5eb82", "Luiz Inacio", "5243518", 0, "41994643417" });

            migrationBuilder.InsertData(
                table: "Varejista",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "Cnpj", "CriadoEm", "EnderecoId", "Parceiro", "RazaoSocial", "Representante", "Status", "Telefone", "UsuarioId" },
                values: new object[] { "eeb45a8bc09e422dbfd45b456a78f878", true, null, "18904128000145", new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 620, DateTimeKind.Unspecified).AddTicks(2272), new TimeSpan(0, 0, 0, 0, 0)), "010bafef4b7342bda843d22b1ec9d7c2", false, "Olivia e Raimundo Casa Noturna ME", "Oliveira Moraes", 0, "1125262748", "4bea229e5b5a4d66a75d1d248f7dc2d5" });

            migrationBuilder.InsertData(
                table: "Vinculo",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "DistribuidorId", "UsuarioId", "VarejistaId" },
                values: new object[] { "c9dc874f89314d86be9ac7930254be10", true, null, new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 622, DateTimeKind.Unspecified).AddTicks(1629), new TimeSpan(0, 0, 0, 0, 0)), "cf936849e0b244a2a6a812c15d69390d", null, "eeb45a8bc09e422dbfd45b456a78f878" });

            migrationBuilder.InsertData(
                table: "Vinculo",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "DistribuidorId", "UsuarioId", "VarejistaId" },
                values: new object[] { "a577f4026f6d4e3cb4a711ff1fd9409a", true, null, new DateTimeOffset(new DateTime(2020, 5, 3, 21, 47, 46, 622, DateTimeKind.Unspecified).AddTicks(2927), new TimeSpan(0, 0, 0, 0, 0)), "189be3c445504dcf9d8559959579035e", null, "eeb45a8bc09e422dbfd45b456a78f878" });

            migrationBuilder.CreateIndex(
                name: "IX_Vinculo_DistribuidorId",
                table: "Vinculo",
                column: "DistribuidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Vinculo_VarejistaId",
                table: "Vinculo",
                column: "VarejistaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vinculo");

            migrationBuilder.DeleteData(
                table: "Distribuidor",
                keyColumn: "Id",
                keyValue: "189be3c445504dcf9d8559959579035e");

            migrationBuilder.DeleteData(
                table: "Distribuidor",
                keyColumn: "Id",
                keyValue: "cf936849e0b244a2a6a812c15d69390d");

            migrationBuilder.DeleteData(
                table: "Participante",
                keyColumn: "Id",
                keyValue: "02ee3c7582ea47a0a8105e9a8e9764bc");

            migrationBuilder.DeleteData(
                table: "Participante",
                keyColumn: "Id",
                keyValue: "816e1eb79d7d4863a953f77e9d8a2347");

            migrationBuilder.DeleteData(
                table: "Participante",
                keyColumn: "Id",
                keyValue: "e942c6f6c1364447a584a8f2fee583c2");

            migrationBuilder.DeleteData(
                table: "Varejista",
                keyColumn: "Id",
                keyValue: "eeb45a8bc09e422dbfd45b456a78f878");

            migrationBuilder.DeleteData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "010bafef4b7342bda843d22b1ec9d7c2");

            migrationBuilder.DeleteData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "39f3a4d569ea400f848dd71deed5eb82");

            migrationBuilder.DeleteData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "5d2387e4b93f476c81dd1bb757b27f43");

            migrationBuilder.DeleteData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "eff3b1b9b3b742a6b5ba4fffdf99585e");

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "2580868e52444603b4d75fdf92f7f916");

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "4bea229e5b5a4d66a75d1d248f7dc2d5");

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "7d5ea14a85ba4af7a4ca4c95545f3545");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "c9f9cd17020c4bfaa83f7c53e5f9b278",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 2, 22, 35, 26, 75, DateTimeKind.Unspecified).AddTicks(8365), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "d87e4de503984cfd8b282616ba50b42f",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 2, 22, 35, 26, 75, DateTimeKind.Unspecified).AddTicks(5064), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "e0176f9f9b0445de83411b404e68a311",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 2, 22, 35, 26, 75, DateTimeKind.Unspecified).AddTicks(8303), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
