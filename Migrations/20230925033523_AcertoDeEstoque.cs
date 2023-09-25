using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newmarket.Migrations
{
    /// <inheritdoc />
    public partial class AcertoDeEstoque : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoID",
                table: "Estoques",
                column: "ProdutoID",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoID",
                table: "Estoques");

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
