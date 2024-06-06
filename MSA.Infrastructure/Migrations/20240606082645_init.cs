using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "msa");

            migrationBuilder.CreateTable(
                name: "account",
                schema: "msa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    role = table.Column<int>(type: "int", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voucher",
                schema: "msa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    voucher_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    voucher_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valid_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expire_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    voucher_status = table.Column<int>(type: "int", nullable: false),
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_on = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updated_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voucher", x => x.id);
                    table.ForeignKey(
                        name: "FK_voucher_account_staff_id",
                        column: x => x.staff_id,
                        principalSchema: "msa",
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_voucher_staff_id",
                schema: "msa",
                table: "voucher",
                column: "staff_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "voucher",
                schema: "msa");

            migrationBuilder.DropTable(
                name: "account",
                schema: "msa");
        }
    }
}
