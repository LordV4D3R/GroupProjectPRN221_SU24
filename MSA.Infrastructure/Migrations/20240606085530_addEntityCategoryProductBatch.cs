using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addEntityCategoryProductBatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetail",
                schema: "msa",
                table: "order_detail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "category",
                schema: "msa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    category_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "msa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    img_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    total_quantity = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_category_category_id",
                        column: x => x.category_id,
                        principalSchema: "msa",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "batch",
                schema: "msa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    expired_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_batch", x => x.id);
                    table.ForeignKey(
                        name: "FK_batch_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "msa",
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_OrderDetail",
                schema: "msa",
                table: "order_detail",
                column: "OrderDetail");

            migrationBuilder.CreateIndex(
                name: "IX_batch_product_id",
                schema: "msa",
                table: "batch",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                schema: "msa",
                table: "product",
                column: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_detail_product_OrderDetail",
                schema: "msa",
                table: "order_detail",
                column: "OrderDetail",
                principalSchema: "msa",
                principalTable: "product",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_detail_product_OrderDetail",
                schema: "msa",
                table: "order_detail");

            migrationBuilder.DropTable(
                name: "batch",
                schema: "msa");

            migrationBuilder.DropTable(
                name: "product",
                schema: "msa");

            migrationBuilder.DropTable(
                name: "category",
                schema: "msa");

            migrationBuilder.DropIndex(
                name: "IX_order_detail_OrderDetail",
                schema: "msa",
                table: "order_detail");

            migrationBuilder.DropColumn(
                name: "OrderDetail",
                schema: "msa",
                table: "order_detail");
        }
    }
}
