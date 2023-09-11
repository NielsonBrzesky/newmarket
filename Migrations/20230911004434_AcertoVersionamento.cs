using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newmarket.Migrations
{
    /// <inheritdoc />
    public partial class AcertoVersionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "Estoques",
                newName: "ProdutoID");

            migrationBuilder.RenameIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                newName: "IX_Estoques_ProdutoID");

            migrationBuilder.AddColumn<float>(
                name: "Total",
                table: "Vendas",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Quantidade",
                table: "Saidas",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "vendaId",
                table: "Saidas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Promocoes",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Saidas_vendaId",
                table: "Saidas",
                column: "vendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoID",
                table: "Estoques",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Saidas_Vendas_vendaId",
                table: "Saidas",
                column: "vendaId",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoID",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_Saidas_Vendas_vendaId",
                table: "Saidas");

            migrationBuilder.DropIndex(
                name: "IX_Saidas_vendaId",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Vendas");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "vendaId",
                table: "Saidas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Promocoes");

            migrationBuilder.RenameColumn(
                name: "ProdutoID",
                table: "Estoques",
                newName: "ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_Estoques_ProdutoID",
                table: "Estoques",
                newName: "IX_Estoques_ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
