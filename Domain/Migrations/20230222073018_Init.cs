using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinanceActivityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceActivityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinanceActivityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeExpenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinanceOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceOperations_IncomeExpenseTypes_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "IncomeExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FinanceActivityTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Income" },
                    { 2, "Expense" }
                });

            migrationBuilder.InsertData(
                table: "IncomeExpenseTypes",
                columns: new[] { "Id", "FinanceActivityType", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Salary" },
                    { 2, 0, "Deposit" },
                    { 3, 1, "Public Utilities" },
                    { 4, 1, "Online Subscriptions" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinanceOperations_CategoryId",
                table: "FinanceOperations",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinanceActivityTypes");

            migrationBuilder.DropTable(
                name: "FinanceOperations");

            migrationBuilder.DropTable(
                name: "IncomeExpenseTypes");
        }
    }
}
