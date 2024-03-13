using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Explorer.Migrations
{
    /// <inheritdoc />
    public partial class StockRecordForeignKeyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "StockRecords",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "StockId",
                table: "StockRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockRecords_StockId",
                table: "StockRecords",
                column: "StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockRecords_Stocks_StockId",
                table: "StockRecords",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockRecords_Stocks_StockId",
                table: "StockRecords");

            migrationBuilder.DropIndex(
                name: "IX_StockRecords_StockId",
                table: "StockRecords");

            migrationBuilder.DropColumn(
                name: "StockId",
                table: "StockRecords");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "StockRecords",
                newName: "id");
        }
    }
}
