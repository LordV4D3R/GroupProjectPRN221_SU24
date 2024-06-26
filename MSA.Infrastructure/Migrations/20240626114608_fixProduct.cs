using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_quantity",
                schema: "msa",
                table: "product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "total_quantity",
                schema: "msa",
                table: "product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
