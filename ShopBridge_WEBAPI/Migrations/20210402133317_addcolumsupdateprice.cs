using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBridge_WEBAPI.Migrations
{
    public partial class addcolumsupdateprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "item_Price",
                table: "Inventories",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "item_UpdatedOn",
                table: "Inventories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "item_Price",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "item_UpdatedOn",
                table: "Inventories");
        }
    }
}
