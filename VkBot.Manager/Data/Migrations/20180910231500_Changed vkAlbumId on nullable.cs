using Microsoft.EntityFrameworkCore.Migrations;

namespace VkBot.Manager.Data.Migrations
{
    public partial class ChangedvkAlbumIdonnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "VkAlbumId",
                table: "StickerSets",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "StickerSetStatus",
                table: "StickerSets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StickerSetStatus",
                table: "StickerSets");

            migrationBuilder.AlterColumn<long>(
                name: "VkAlbumId",
                table: "StickerSets",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
