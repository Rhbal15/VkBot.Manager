using Microsoft.EntityFrameworkCore.Migrations;

namespace VkBot.Manager.Data.Migrations
{
    public partial class AddedPublicationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "StickerSets",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "StickerSets");
        }
    }
}
