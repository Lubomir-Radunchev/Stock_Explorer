using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Explorer.Migrations
{
    /// <inheritdoc />
    public partial class removedStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdjClose = table.Column<double>(type: "float", nullable: false),
                    Close = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Open = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }
    }
}
