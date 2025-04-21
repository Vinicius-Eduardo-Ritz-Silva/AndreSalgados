using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class TipoProdutoNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VR_PRODUTO_VR_PRODUTO_TIPO_ID_TIPO",
                table: "VR_PRODUTO");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID_TIPO",
                table: "VR_PRODUTO",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_VR_PRODUTO_VR_PRODUTO_TIPO_ID_TIPO",
                table: "VR_PRODUTO",
                column: "ID_TIPO",
                principalTable: "VR_PRODUTO_TIPO",
                principalColumn: "ID_PRODUTOTIPO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VR_PRODUTO_VR_PRODUTO_TIPO_ID_TIPO",
                table: "VR_PRODUTO");

            migrationBuilder.AlterColumn<Guid>(
                name: "ID_TIPO",
                table: "VR_PRODUTO",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VR_PRODUTO_VR_PRODUTO_TIPO_ID_TIPO",
                table: "VR_PRODUTO",
                column: "ID_TIPO",
                principalTable: "VR_PRODUTO_TIPO",
                principalColumn: "ID_PRODUTOTIPO",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
