using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class EnumPedidoStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FL_PEDIPAGO",
                table: "VR_PEDIDO",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "FL_PEDIPAGO",
                table: "VR_PEDIDO",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
