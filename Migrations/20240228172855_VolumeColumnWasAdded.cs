using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stock_Explorer.Migrations
{
    /// <inheritdoc />
    public partial class VolumeColumnWasAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Volume",
                table: "Stocks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Volume",
                table: "Stocks");
        }
    }
}
