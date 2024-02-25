using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagerWeb.Migrations
{
    public partial class extrafirstUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InventoryItemId",
                table: "OutTakes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPackages",
                table: "OutTakes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPieces",
                table: "OutTakes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPackages",
                table: "Intakes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPieces",
                table: "Intakes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutTakes_InventoryItemId",
                table: "OutTakes",
                column: "InventoryItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OutTakes_InventoryItems_InventoryItemId",
                table: "OutTakes",
                column: "InventoryItemId",
                principalTable: "InventoryItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutTakes_InventoryItems_InventoryItemId",
                table: "OutTakes");

            migrationBuilder.DropIndex(
                name: "IX_OutTakes_InventoryItemId",
                table: "OutTakes");

            migrationBuilder.DropColumn(
                name: "InventoryItemId",
                table: "OutTakes");

            migrationBuilder.DropColumn(
                name: "NumberOfPackages",
                table: "OutTakes");

            migrationBuilder.DropColumn(
                name: "NumberOfPieces",
                table: "OutTakes");

            migrationBuilder.DropColumn(
                name: "NumberOfPackages",
                table: "Intakes");

            migrationBuilder.DropColumn(
                name: "NumberOfPieces",
                table: "Intakes");
        }
    }
}
