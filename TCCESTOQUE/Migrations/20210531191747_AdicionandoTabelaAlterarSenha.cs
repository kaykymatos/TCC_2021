using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace TCCESTOQUE.Migrations
{
    public partial class AdicionandoTabelaAlterarSenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlterarSenha",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Codigo = table.Column<int>(nullable: false),
                    DataEmissão = table.Column<DateTime>(nullable: false),
                    Invalida = table.Column<bool>(nullable: false),
                    VendedorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlterarSenha", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlterarSenha_Vendedor_VendedorId",
                        column: x => x.VendedorId,
                        principalTable: "Vendedor",
                        principalColumn: "VendedorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlterarSenha_VendedorId",
                table: "AlterarSenha",
                column: "VendedorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlterarSenha");
        }
    }
}
