using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControllRR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class BrazilianTaxesConfigurationS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_CFOPs_CFOPId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_CFOPId",
                table: "PurchaseOrders");

            migrationBuilder.AlterColumn<string>(
                name: "CFOPId",
                table: "PurchaseOrders",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CFOPId",
                table: "PurchaseOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CFOPId",
                table: "PurchaseOrders",
                column: "CFOPId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_CFOPs_CFOPId",
                table: "PurchaseOrders",
                column: "CFOPId",
                principalTable: "CFOPs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
