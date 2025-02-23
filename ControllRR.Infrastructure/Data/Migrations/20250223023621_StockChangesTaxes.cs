using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControllRR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class StockChangesTaxes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Suppliers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "Stocks",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Stocks",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "FinancialRecords",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalTaxes",
                table: "FinancialRecords",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "CPF_CNPJ",
                table: "Customers",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CNPJ",
                table: "Suppliers",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductName",
                table: "Stocks",
                column: "ProductName");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_OrderDate",
                table: "PurchaseOrders",
                column: "OrderDate");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CPF_CNPJ",
                table: "Customers",
                column: "CPF_CNPJ",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suppliers_CNPJ",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductName",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_OrderDate",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CPF_CNPJ",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TotalTaxes",
                table: "FinancialRecords");

            migrationBuilder.AlterColumn<string>(
                name: "CNPJ",
                table: "Suppliers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "TaxRate",
                table: "Stocks",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PurchasePrice",
                table: "Stocks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Stocks",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "FinancialRecords",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "CPF_CNPJ",
                table: "Customers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
