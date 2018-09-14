using Microsoft.EntityFrameworkCore.Migrations;

namespace VkBot.Manager.Data.Migrations
{
    public partial class Addedfirstnameandlastnametobotuserentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "BotUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "BotUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "BotUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "BotUsers");
        }
    }
}
