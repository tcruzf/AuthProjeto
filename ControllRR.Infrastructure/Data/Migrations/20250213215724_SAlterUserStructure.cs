using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControllRR.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SAlterUserStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Devices_DeviceId",
                table: "Maintenances");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Devices_DeviceId",
                table: "Maintenances",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Devices_DeviceId",
                table: "Maintenances");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Maintenances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Devices_DeviceId",
                table: "Maintenances",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }
    }
}
