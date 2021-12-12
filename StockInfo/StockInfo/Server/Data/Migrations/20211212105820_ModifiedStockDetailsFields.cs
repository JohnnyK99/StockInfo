using Microsoft.EntityFrameworkCore.Migrations;

namespace StockInfo.Server.Data.Migrations
{
    public partial class ModifiedStockDetailsFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ceo",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Stocks");

            migrationBuilder.DropColumn(
               name: "Url",
               table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "Exchange",
                table: "Stocks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Stocks",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Stocks",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Exchange",
                table: "Stocks");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Stocks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ceo",
                table: "Stocks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Stocks",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
