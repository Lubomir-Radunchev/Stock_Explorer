using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Explorer.Migrations
{
    /// <inheritdoc />
    public partial class AddColumNamedDataInStockRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "StockRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "StockRecords");
        }
    }
}
