using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControllRR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FiledPriceSugested : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceSugested",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Profit",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceSugested",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Profit",
                table: "Stocks");
        }
    }
}
