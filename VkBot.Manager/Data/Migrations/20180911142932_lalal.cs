using Microsoft.EntityFrameworkCore.Migrations;

namespace VkBot.Manager.Data.Migrations
{
    public partial class lalal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowedStickers_Stickers_StickerId",
                table: "ShowedStickers");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowedStickers_Stickers_StickerId",
                table: "ShowedStickers",
                column: "StickerId",
                principalTable: "Stickers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowedStickers_Stickers_StickerId",
                table: "ShowedStickers");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowedStickers_Stickers_StickerId",
                table: "ShowedStickers",
                column: "StickerId",
                principalTable: "Stickers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
