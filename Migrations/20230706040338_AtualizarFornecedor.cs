using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace newmarket.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarFornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Fornecedores",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Fornecedores");
        }
    }
}
