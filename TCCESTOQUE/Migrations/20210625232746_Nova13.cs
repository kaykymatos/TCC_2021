using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCCESTOQUE.Migrations
{
    public partial class Nova13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlterarSenha_Vendedor_VendedorId",
                table: "AlterarSenha");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Vendedor");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendedorId",
                table: "AlterarSenha",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_AlterarSenha_Vendedor_VendedorId",
                table: "AlterarSenha",
                column: "VendedorId",
                principalTable: "Vendedor",
                principalColumn: "VendedorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlterarSenha_Vendedor_VendedorId",
                table: "AlterarSenha");

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Vendedor",
                type: "varchar(14) CHARACTER SET utf8mb4",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "VendedorId",
                table: "AlterarSenha",
                type: "char(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AlterarSenha_Vendedor_VendedorId",
                table: "AlterarSenha",
                column: "VendedorId",
                principalTable: "Vendedor",
                principalColumn: "VendedorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
