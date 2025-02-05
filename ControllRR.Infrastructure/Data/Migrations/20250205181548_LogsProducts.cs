using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControllRR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class LogsProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaintenanceId",
                table: "StockManagements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockManagements_MaintenanceId",
                table: "StockManagements",
                column: "MaintenanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockManagements_Maintenances_MaintenanceId",
                table: "StockManagements",
                column: "MaintenanceId",
                principalTable: "Maintenances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockManagements_Maintenances_MaintenanceId",
                table: "StockManagements");

            migrationBuilder.DropIndex(
                name: "IX_StockManagements_MaintenanceId",
                table: "StockManagements");

            migrationBuilder.DropColumn(
                name: "MaintenanceId",
                table: "StockManagements");
        }
    }
}
