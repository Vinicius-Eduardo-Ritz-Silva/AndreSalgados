using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class PostGre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VR_CLIENTE",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NM_NOMECLIE = table.Column<string>(type: "text", nullable: true),
                    NR_NUMECLIE = table.Column<string>(type: "text", nullable: true),
                    GN_ENDECLIE = table.Column<string>(type: "text", nullable: true),
                    DT_INCLCLIENTE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ALTECLIENTE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IE_CODICLIENTE = table.Column<int>(type: "integer", nullable: true),
                    FL_ATIVCLIENTE = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VR_CLIENTE", x => x.ID_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "VR_PRODUTO_TIPO",
                columns: table => new
                {
                    ID_PRODUTOTIPO = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NM_TIPO = table.Column<string>(type: "text", nullable: true),
                    DT_INCLPRODUTOTIPO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ALTEPRODUTOTIPO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IE_CODIPRODUTOTIPO = table.Column<int>(type: "integer", nullable: true),
                    FL_ATIVPRODUTOTIPO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VR_PRODUTO_TIPO", x => x.ID_PRODUTOTIPO);
                });

            migrationBuilder.CreateTable(
                name: "VR_COBRANCA",
                columns: table => new
                {
                    ID_COBRANCA = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    DT_DATACOBR = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NR_VALOCOBR = table.Column<decimal>(type: "numeric", nullable: false),
                    NR_VALODESC = table.Column<decimal>(type: "numeric", nullable: true),
                    FL_DESCPORC = table.Column<bool>(type: "boolean", nullable: false),
                    FL_COBRPERD = table.Column<bool>(type: "boolean", nullable: false),
                    ID_CLIE = table.Column<Guid>(type: "uuid", nullable: false),
                    DT_INCLCOBRANCA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ALTECOBRANCA = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IE_CODICOBRANCA = table.Column<int>(type: "integer", nullable: true),
                    FL_ATIVCOBRANCA = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VR_COBRANCA", x => x.ID_COBRANCA);
                    table.ForeignKey(
                        name: "FK_VR_COBRANCA_VR_CLIENTE_ID_CLIE",
                        column: x => x.ID_CLIE,
                        principalTable: "VR_CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VR_VENDAS_RELATORIO",
                columns: table => new
                {
                    ID_VENDASRELATORIO = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NR_VALOVEND = table.Column<decimal>(type: "numeric", nullable: false),
                    FL_VENDGANH = table.Column<bool>(type: "boolean", nullable: false),
                    ID_CLIE = table.Column<Guid>(type: "uuid", nullable: false),
                    DT_INCLVENDASRELATORIO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ALTEVENDASRELATORIO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IE_CODIVENDASRELATORIO = table.Column<int>(type: "integer", nullable: true),
                    FL_ATIVVENDASRELATORIO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VR_VENDAS_RELATORIO", x => x.ID_VENDASRELATORIO);
                    table.ForeignKey(
                        name: "FK_VR_VENDAS_RELATORIO_VR_CLIENTE_ID_CLIE",
                        column: x => x.ID_CLIE,
                        principalTable: "VR_CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VR_PRODUTO",
                columns: table => new
                {
                    ID_PRODUTO = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NM_NOMEPROD = table.Column<string>(type: "text", nullable: true),
                    GN_DESCPROD = table.Column<string>(type: "text", nullable: true),
                    NR_PRECPROD = table.Column<decimal>(type: "numeric", nullable: false),
                    NR_QUANPROD = table.Column<int>(type: "integer", nullable: false),
                    ID_TIPO = table.Column<Guid>(type: "uuid", nullable: true),
                    DT_INCLPRODUTO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ALTEPRODUTO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IE_CODIPRODUTO = table.Column<int>(type: "integer", nullable: true),
                    FL_ATIVPRODUTO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VR_PRODUTO", x => x.ID_PRODUTO);
                    table.ForeignKey(
                        name: "FK_VR_PRODUTO_VR_PRODUTO_TIPO_ID_TIPO",
                        column: x => x.ID_TIPO,
                        principalTable: "VR_PRODUTO_TIPO",
                        principalColumn: "ID_PRODUTOTIPO");
                });

            migrationBuilder.CreateTable(
                name: "VR_PEDIDO",
                columns: table => new
                {
                    ID_PEDIDO = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NR_QUANPEDI = table.Column<int>(type: "integer", nullable: false),
                    NR_VALOPEDI = table.Column<decimal>(type: "numeric", nullable: false),
                    FL_PEDIPAGO = table.Column<int>(type: "integer", nullable: false),
                    EN_STATCOBR = table.Column<int>(type: "integer", nullable: true),
                    FL_EDITPEDI = table.Column<bool>(type: "boolean", nullable: false),
                    ID_CLIE = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_COBR = table.Column<Guid>(type: "uuid", nullable: true),
                    DT_INCLPEDIDO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ALTEPEDIDO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IE_CODIPEDIDO = table.Column<int>(type: "integer", nullable: true),
                    FL_ATIVPEDIDO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VR_PEDIDO", x => x.ID_PEDIDO);
                    table.ForeignKey(
                        name: "FK_VR_PEDIDO_VR_CLIENTE_ID_CLIE",
                        column: x => x.ID_CLIE,
                        principalTable: "VR_CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VR_PEDIDO_VR_COBRANCA_ID_COBR",
                        column: x => x.ID_COBR,
                        principalTable: "VR_COBRANCA",
                        principalColumn: "ID_COBRANCA");
                });

            migrationBuilder.CreateTable(
                name: "VR_PRODUTO_PEDIDO",
                columns: table => new
                {
                    ID_PRODUTOPEDIDO = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NR_QUANPEDIPROD = table.Column<int>(type: "integer", nullable: false),
                    NR_VALOPEDIPROD = table.Column<decimal>(type: "numeric", nullable: false),
                    ID_PEDI = table.Column<Guid>(type: "uuid", nullable: false),
                    ID_PROD = table.Column<Guid>(type: "uuid", nullable: false),
                    DT_INCLPRODUTOPEDIDO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DT_ALTEPRODUTOPEDIDO = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IE_CODIPRODUTOPEDIDO = table.Column<int>(type: "integer", nullable: true),
                    FL_ATIVPRODUTOPEDIDO = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VR_PRODUTO_PEDIDO", x => x.ID_PRODUTOPEDIDO);
                    table.ForeignKey(
                        name: "FK_VR_PRODUTO_PEDIDO_VR_PEDIDO_ID_PEDI",
                        column: x => x.ID_PEDI,
                        principalTable: "VR_PEDIDO",
                        principalColumn: "ID_PEDIDO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VR_PRODUTO_PEDIDO_VR_PRODUTO_ID_PROD",
                        column: x => x.ID_PROD,
                        principalTable: "VR_PRODUTO",
                        principalColumn: "ID_PRODUTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VR_COBRANCA_ID_CLIE",
                table: "VR_COBRANCA",
                column: "ID_CLIE");

            migrationBuilder.CreateIndex(
                name: "IX_VR_PEDIDO_ID_CLIE",
                table: "VR_PEDIDO",
                column: "ID_CLIE");

            migrationBuilder.CreateIndex(
                name: "IX_VR_PEDIDO_ID_COBR",
                table: "VR_PEDIDO",
                column: "ID_COBR");

            migrationBuilder.CreateIndex(
                name: "IX_VR_PRODUTO_ID_TIPO",
                table: "VR_PRODUTO",
                column: "ID_TIPO");

            migrationBuilder.CreateIndex(
                name: "IX_VR_PRODUTO_PEDIDO_ID_PEDI",
                table: "VR_PRODUTO_PEDIDO",
                column: "ID_PEDI");

            migrationBuilder.CreateIndex(
                name: "IX_VR_PRODUTO_PEDIDO_ID_PROD",
                table: "VR_PRODUTO_PEDIDO",
                column: "ID_PROD");

            migrationBuilder.CreateIndex(
                name: "IX_VR_VENDAS_RELATORIO_ID_CLIE",
                table: "VR_VENDAS_RELATORIO",
                column: "ID_CLIE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VR_PRODUTO_PEDIDO");

            migrationBuilder.DropTable(
                name: "VR_VENDAS_RELATORIO");

            migrationBuilder.DropTable(
                name: "VR_PEDIDO");

            migrationBuilder.DropTable(
                name: "VR_PRODUTO");

            migrationBuilder.DropTable(
                name: "VR_COBRANCA");

            migrationBuilder.DropTable(
                name: "VR_PRODUTO_TIPO");

            migrationBuilder.DropTable(
                name: "VR_CLIENTE");
        }
    }
}
