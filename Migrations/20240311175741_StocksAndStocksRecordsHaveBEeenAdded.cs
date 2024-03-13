using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Explorer.Migrations
{
    /// <inheritdoc />
    public partial class StocksAndStocksRecordsHaveBEeenAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockRecords",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Open = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    High = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Low = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Close = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adj = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockRecords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockRecords");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
