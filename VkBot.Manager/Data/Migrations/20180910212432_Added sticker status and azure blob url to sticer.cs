using Microsoft.EntityFrameworkCore.Migrations;

namespace VkBot.Manager.Data.Migrations
{
    public partial class Addedstickerstatusandazurebloburltosticer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AzureImageUrl",
                table: "Stickers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StickerStatus",
                table: "Stickers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AzureImageUrl",
                table: "Stickers");

            migrationBuilder.DropColumn(
                name: "StickerStatus",
                table: "Stickers");
        }
    }
}
