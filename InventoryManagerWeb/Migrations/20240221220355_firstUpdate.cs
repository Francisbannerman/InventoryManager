using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagerWeb.Migrations
{
    public partial class firstUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Notifications");

            migrationBuilder.AddColumn<bool>(
                name: "IsTakenInPackages",
                table: "OutTakes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTakenInPieces",
                table: "OutTakes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityPerPackage",
                table: "InventoryItems",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "InventoryItems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReceivedInPackages",
                table: "Intakes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReceivedInPieces",
                table: "Intakes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTakenInPackages",
                table: "OutTakes");

            migrationBuilder.DropColumn(
                name: "IsTakenInPieces",
                table: "OutTakes");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "InventoryItems");

            migrationBuilder.DropColumn(
                name: "IsReceivedInPackages",
                table: "Intakes");

            migrationBuilder.DropColumn(
                name: "IsReceivedInPieces",
                table: "Intakes");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Notifications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "QuantityPerPackage",
                table: "InventoryItems",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
