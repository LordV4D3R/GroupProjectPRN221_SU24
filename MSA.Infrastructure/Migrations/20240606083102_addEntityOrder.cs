using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEntityOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order",
                schema: "msa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_price = table.Column<double>(type: "float", nullable: false),
                    total_quantity = table.Column<int>(type: "int", nullable: false),
                    order_status = table.Column<int>(type: "int", nullable: false),
                    OrderRefundStatus = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_account_customer_id",
                        column: x => x.customer_id,
                        principalSchema: "msa",
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_customer_id",
                schema: "msa",
                table: "order",
                column: "customer_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order",
                schema: "msa");
        }
    }
}
