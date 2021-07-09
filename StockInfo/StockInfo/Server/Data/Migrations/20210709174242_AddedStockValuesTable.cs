using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInfo.Server.Data.Migrations
{
    public partial class AddedStockValuesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockValues",
                columns: table => new
                {
                    Ticker = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    O = table.Column<double>(type: "float", nullable: false),
                    L = table.Column<double>(type: "float", nullable: false),
                    C = table.Column<double>(type: "float", nullable: false),
                    H = table.Column<double>(type: "float", nullable: false),
                    V = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockValues", x => new { x.Ticker, x.Date });
                    table.ForeignKey(
                        name: "FK_StockValues_Stocks_Ticker",
                        column: x => x.Ticker,
                        principalTable: "Stocks",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockValues");
        }
    }
}
