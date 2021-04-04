using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBridge_WEBAPI.Migrations
{
    public partial class CreateShopBridgeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    item_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    item_Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    item_Desc = table.Column<string>(nullable: true),
                    item_Quantity = table.Column<int>(nullable: false),
                    item_Image = table.Column<byte[]>(nullable: true),
                    itemAvailability = table.Column<bool>(nullable: false),
                    item_AddedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.item_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");
        }
    }
}
