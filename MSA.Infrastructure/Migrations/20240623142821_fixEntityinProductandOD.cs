using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixEntityinProductandOD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_detail_product_OrderDetail",
                schema: "msa",
                table: "order_detail");

            migrationBuilder.DropIndex(
                name: "IX_order_detail_OrderDetail",
                schema: "msa",
                table: "order_detail");

            migrationBuilder.DropColumn(
                name: "OrderDetail",
                schema: "msa",
                table: "order_detail");

            migrationBuilder.AddColumn<Guid>(
                name: "product_id",
                schema: "msa",
                table: "order_detail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_product_id",
                schema: "msa",
                table: "order_detail",
                column: "product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_order_detail_product_product_id",
                schema: "msa",
                table: "order_detail",
                column: "product_id",
                principalSchema: "msa",
                principalTable: "product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_detail_product_product_id",
                schema: "msa",
                table: "order_detail");

            migrationBuilder.DropIndex(
                name: "IX_order_detail_product_id",
                schema: "msa",
                table: "order_detail");

            migrationBuilder.DropColumn(
                name: "product_id",
                schema: "msa",
                table: "order_detail");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetail",
                schema: "msa",
                table: "order_detail",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_OrderDetail",
                schema: "msa",
                table: "order_detail",
                column: "OrderDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_order_detail_product_OrderDetail",
                schema: "msa",
                table: "order_detail",
                column: "OrderDetail",
                principalSchema: "msa",
                principalTable: "product",
                principalColumn: "id");
        }
    }
}
