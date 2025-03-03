using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControllRR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SupplierChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivityArea",
                table: "Suppliers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Suppliers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CRT",
                table: "Suppliers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IssuerIE",
                table: "Suppliers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PersonType",
                table: "Suppliers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityArea",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "CRT",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "IssuerIE",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Suppliers");
        }
    }
}
