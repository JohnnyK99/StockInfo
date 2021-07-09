using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInfo.Server.Data.Migrations
{
    public partial class AddedSavedStocksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedStocks",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Ticker = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedStocks", x => new { x.Username, x.Ticker });
                    table.ForeignKey(
                        name: "FK_SavedStocks_Stocks_Ticker",
                        column: x => x.Ticker,
                        principalTable: "Stocks",
                        principalColumn: "Ticker",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedStocks_Ticker",
                table: "SavedStocks",
                column: "Ticker");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedStocks");
        }
    }
}
