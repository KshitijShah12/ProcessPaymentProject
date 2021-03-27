using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessPaymentApi.Migrations
{
    public partial class ProcessPaymentmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessPayment_tbl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardHolder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessPayment_tbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "processStatuses",
                columns: table => new
                {
                    ProcessStatusId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_processStatuses", x => x.ProcessStatusId);
                    table.ForeignKey(
                        name: "FK_processStatuses_ProcessPayment_tbl_CardId",
                        column: x => x.CardId,
                        principalTable: "ProcessPayment_tbl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_processStatuses_CardId",
                table: "processStatuses",
                column: "CardId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "processStatuses");

            migrationBuilder.DropTable(
                name: "ProcessPayment_tbl");
        }
    }
}
