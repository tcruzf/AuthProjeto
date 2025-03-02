using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControllRR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjusteSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CFOPId",
                table: "Stocks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<decimal>(
                name: "CofinsAmount",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CofinsBase",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IcmsAmount",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "IcmsBase",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PisAmount",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PisBase",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxAmount",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CFOPId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "CofinsAmount",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "CofinsBase",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "IcmsAmount",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "IcmsBase",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "PisAmount",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "PisBase",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Stocks");
        }
    }
}
