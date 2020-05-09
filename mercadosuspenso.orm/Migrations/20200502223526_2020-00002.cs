using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mercadosuspenso.orm.Migrations
{
    public partial class _202000002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "Email", "Senha", "Tipo" },
                values: new object[] { "d87e4de503984cfd8b282616ba50b42f", true, null, new DateTimeOffset(new DateTime(2020, 5, 2, 22, 35, 26, 75, DateTimeKind.Unspecified).AddTicks(5064), new TimeSpan(0, 0, 0, 0, 0)), "lucas.moraes@datasuricata.com.br", "C3F21A70DFB5072C152691AF019CC68A", 99 });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "Email", "Senha", "Tipo" },
                values: new object[] { "e0176f9f9b0445de83411b404e68a311", true, null, new DateTimeOffset(new DateTime(2020, 5, 2, 22, 35, 26, 75, DateTimeKind.Unspecified).AddTicks(8303), new TimeSpan(0, 0, 0, 0, 0)), "equipegkio@gmail.com", "2D27FE16A2DE63872DDEC0BAFFB473BC", 99 });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Ativo", "AtualizadoEm", "CriadoEm", "Email", "Senha", "Tipo" },
                values: new object[] { "c9f9cd17020c4bfaa83f7c53e5f9b278", true, null, new DateTimeOffset(new DateTime(2020, 5, 2, 22, 35, 26, 75, DateTimeKind.Unspecified).AddTicks(8365), new TimeSpan(0, 0, 0, 0, 0)), "web.admin@mailinator.com", "6C5F89C867E08F99BE95D3CBA4B88594", 99 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "c9f9cd17020c4bfaa83f7c53e5f9b278");

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "d87e4de503984cfd8b282616ba50b42f");

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: "e0176f9f9b0445de83411b404e68a311");
        }
    }
}
