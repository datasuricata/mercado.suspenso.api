using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mercadosuspenso.orm.Migrations
{
    public partial class _2020_00005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vinculo",
                keyColumn: "Id",
                keyValue: "70e0dd3d866049d1b5173ace821cce5b");

            migrationBuilder.DeleteData(
                table: "Vinculo",
                keyColumn: "Id",
                keyValue: "ba34b154de8045af9dff79bc68b728dc");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UltimoResgate",
                table: "Participante",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UltimoResgate",
                table: "Distribuidor",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Distribuidor",
                keyColumn: "Id",
                keyValue: "189be3c445504dcf9d8559959579035e",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 414, DateTimeKind.Unspecified).AddTicks(665), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Distribuidor",
                keyColumn: "Id",
                keyValue: "cf936849e0b244a2a6a812c15d69390d",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 413, DateTimeKind.Unspecified).AddTicks(4158), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "010bafef4b7342bda843d22b1ec9d7c2",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 412, DateTimeKind.Unspecified).AddTicks(9849), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "39f3a4d569ea400f848dd71deed5eb82",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 413, DateTimeKind.Unspecified).AddTicks(5), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "5d2387e4b93f476c81dd1bb757b27f43",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 412, DateTimeKind.Unspecified).AddTicks(9678), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "eff3b1b9b3b742a6b5ba4fffdf99585e",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 412, DateTimeKind.Unspecified).AddTicks(3682), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Participante",
                keyColumn: "Id",
                keyValue: "02ee3c7582ea47a0a8105e9a8e9764bc",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 415, DateTimeKind.Unspecified).AddTicks(5951), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Participante",
                keyColumn: "Id",
                keyValue: "816e1eb79d7d4863a953f77e9d8a2347",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 415, DateTimeKind.Unspecified).AddTicks(5838), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Participante",
                keyColumn: "Id",
                keyValue: "e942c6f6c1364447a584a8f2fee583c2",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 415, DateTimeKind.Unspecified).AddTicks(680), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "2580868e52444603b4d75fdf92f7f916",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 410, DateTimeKind.Unspecified).AddTicks(3829), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "4bea229e5b5a4d66a75d1d248f7dc2d5",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 410, DateTimeKind.Unspecified).AddTicks(4408), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "7d5ea14a85ba4af7a4ca4c95545f3545",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 400, DateTimeKind.Unspecified).AddTicks(6657), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "c9f9cd17020c4bfaa83f7c53e5f9b278",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 400, DateTimeKind.Unspecified).AddTicks(5835), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "d87e4de503984cfd8b282616ba50b42f",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 400, DateTimeKind.Unspecified).AddTicks(3067), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "e0176f9f9b0445de83411b404e68a311",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 400, DateTimeKind.Unspecified).AddTicks(5769), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Varejista",
                keyColumn: "Id",
                keyValue: "eeb45a8bc09e422dbfd45b456a78f878",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 414, DateTimeKind.Unspecified).AddTicks(4087), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Vinculo",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "DistribuidorId", "UsuarioId", "VarejistaId" },
                values: new object[,]
                {
                    { "06950b118f944b64b5d736bc38b8481c", true, null, new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 415, DateTimeKind.Unspecified).AddTicks(9643), new TimeSpan(0, 0, 0, 0, 0)), "189be3c445504dcf9d8559959579035e", null, "eeb45a8bc09e422dbfd45b456a78f878" },
                    { "c2324df6ea3a47dab5bd38fce556d828", true, null, new DateTimeOffset(new DateTime(2020, 6, 4, 0, 13, 26, 415, DateTimeKind.Unspecified).AddTicks(8760), new TimeSpan(0, 0, 0, 0, 0)), "cf936849e0b244a2a6a812c15d69390d", null, "eeb45a8bc09e422dbfd45b456a78f878" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Vinculo",
                keyColumn: "Id",
                keyValue: "06950b118f944b64b5d736bc38b8481c");

            migrationBuilder.DeleteData(
                table: "Vinculo",
                keyColumn: "Id",
                keyValue: "c2324df6ea3a47dab5bd38fce556d828");

            migrationBuilder.DropColumn(
                name: "UltimoResgate",
                table: "Participante");

            migrationBuilder.DropColumn(
                name: "UltimoResgate",
                table: "Distribuidor");

            migrationBuilder.UpdateData(
                table: "Distribuidor",
                keyColumn: "Id",
                keyValue: "189be3c445504dcf9d8559959579035e",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 840, DateTimeKind.Unspecified).AddTicks(9435), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Distribuidor",
                keyColumn: "Id",
                keyValue: "cf936849e0b244a2a6a812c15d69390d",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 840, DateTimeKind.Unspecified).AddTicks(2677), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "010bafef4b7342bda843d22b1ec9d7c2",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 839, DateTimeKind.Unspecified).AddTicks(8552), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "39f3a4d569ea400f848dd71deed5eb82",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 839, DateTimeKind.Unspecified).AddTicks(8576), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "5d2387e4b93f476c81dd1bb757b27f43",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 839, DateTimeKind.Unspecified).AddTicks(8427), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Endereco",
                keyColumn: "Id",
                keyValue: "eff3b1b9b3b742a6b5ba4fffdf99585e",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 838, DateTimeKind.Unspecified).AddTicks(8983), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Participante",
                keyColumn: "Id",
                keyValue: "02ee3c7582ea47a0a8105e9a8e9764bc",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 842, DateTimeKind.Unspecified).AddTicks(8059), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Participante",
                keyColumn: "Id",
                keyValue: "816e1eb79d7d4863a953f77e9d8a2347",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 842, DateTimeKind.Unspecified).AddTicks(7920), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Participante",
                keyColumn: "Id",
                keyValue: "e942c6f6c1364447a584a8f2fee583c2",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 842, DateTimeKind.Unspecified).AddTicks(2214), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "2580868e52444603b4d75fdf92f7f916",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 837, DateTimeKind.Unspecified).AddTicks(2254), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "4bea229e5b5a4d66a75d1d248f7dc2d5",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 837, DateTimeKind.Unspecified).AddTicks(2760), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "7d5ea14a85ba4af7a4ca4c95545f3545",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 829, DateTimeKind.Unspecified).AddTicks(1110), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "c9f9cd17020c4bfaa83f7c53e5f9b278",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 829, DateTimeKind.Unspecified).AddTicks(272), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "d87e4de503984cfd8b282616ba50b42f",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 828, DateTimeKind.Unspecified).AddTicks(7799), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "e0176f9f9b0445de83411b404e68a311",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 829, DateTimeKind.Unspecified).AddTicks(209), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Varejista",
                keyColumn: "Id",
                keyValue: "eeb45a8bc09e422dbfd45b456a78f878",
                column: "CriadoEm",
                value: new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 841, DateTimeKind.Unspecified).AddTicks(2637), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Vinculo",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "DistribuidorId", "UsuarioId", "VarejistaId" },
                values: new object[,]
                {
                    { "ba34b154de8045af9dff79bc68b728dc", true, null, new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 843, DateTimeKind.Unspecified).AddTicks(1996), new TimeSpan(0, 0, 0, 0, 0)), "189be3c445504dcf9d8559959579035e", null, "eeb45a8bc09e422dbfd45b456a78f878" },
                    { "70e0dd3d866049d1b5173ace821cce5b", true, null, new DateTimeOffset(new DateTime(2020, 5, 14, 21, 49, 41, 843, DateTimeKind.Unspecified).AddTicks(971), new TimeSpan(0, 0, 0, 0, 0)), "cf936849e0b244a2a6a812c15d69390d", null, "eeb45a8bc09e422dbfd45b456a78f878" }
                });
        }
    }
}
